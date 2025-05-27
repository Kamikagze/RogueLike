using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : AbstractAbill
{
    [SerializeField] private Transform shooter;

    public IceArrowPool iceArrowPool; // ������ �� ��� ����
    public FounderOfEnemies enemies; // ���� ��� �������, ���� ��������
    [SerializeField] private Shooter[] shooters;
    public float fireRate = 1f; // �������� ����� ����������

    // epic
    private float currentTime = 0f;
    private int countOfShoot = 0;
    private int maxShootCounter = 50;
    private bool isIceHell = false;
    private Vector2[] positions;
    

    private void Start()
    {
        FullFillButtons.IceHell += IceHell;
        CooldownReduction();
        Activate();
    }
   

    protected override void ActionOfAbill()
    {
        if (!isIceHell) ShootMethod();
        else if (isIceHell)
        {
            currentTime = 0;
            Shuffle(positions);
            countOfShoot = 0;
        }
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
        if (isIceHell)
        {
            currentTime += deltaTime;
            if (currentTime >= fireRate && countOfShoot <= maxShootCounter - 1)
            {
                IceHellShootMethod(countOfShoot);
                countOfShoot++;
                currentTime = 0;
            }
            else if (countOfShoot >= maxShootCounter) timer = 0;
        }
    }
   
    private void ShootMethod()
    {
        if (enemies != null)
        {
            var nearestEnemy = enemies.FindNearestEnemy(shooter);

            // ���������, ���������� �� ��������� ����
            if (nearestEnemy != null)
            {
                // ������� ����������� � ���������� �����
                Vector2 direction = (nearestEnemy.position - transform.position).normalized;

                // �������� ���� �� ����
                IceArrow iceArrow = iceArrowPool.GetIceArrow();

                if (iceArrow != null)
                {
                    // ������������� ������� ���� � �������������� �
                    iceArrow.transform.position = transform.position;
                    iceArrow.Initialize(direction);

                    // ������������� ���� �������� ������ ���, ����� ��� �������� � ������� �����
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // �������� ���� � ��������
                    iceArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // ������������ ������
                }
                else
                {
                    Debug.LogWarning("�� ������� �������� ���� �� ����");
                }
            }
            else
            {
                Debug.LogWarning("��� ��������� ������ ��� ��������");
            }
        }
    }

   
    protected override void Offers()
    {
        if (isIceHell) { cooldownTime = fireRate * 70f; }
    }
    Vector2[] GenerateCirclePoints(int count, float radius)
    {
        Vector2[] points = new Vector2[count];

        float angleStep = 360f / count; // ���� ����� �������

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad; // ����������� ���� � �������

            // ��������� ���������� ����� �� ����������
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y); // ������� ������ � 2D ������������
        }

        return points;
    }
    private void SetPositions()
    {
        positions = GenerateCirclePoints(maxShootCounter, 4f);
    }
    private void Shuffle(Vector2[] array)
    {
        int n = array.Length;
        System.Random rng = new System.Random();
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            Vector2 temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
   

    public void TurnerOnShooters(int index)
    {
        if (shooters != null)
        {
            for (int i = 0; i < shooters.Length; i++)
            {
                if (index == i)
                {
                    shooters[i].gameObject.SetActive(true);
                    // �������������� �������� �������� ��� ��������
                    shooters[i].fireRate = this.fireRate; // ������, �������������� �������� ��������
                    shooters[i].enemies = this.enemies;   // ������ �� ������

                }

                
            }
        }
    }


    private void IceHellShootMethod(int index)
    {
            Vector2 playerPos = transform.position;
            Vector2 targetPosition = positions[index] + playerPos;
            IceArrow iceArrow = iceArrowPool.GetIceArrow();

            if (iceArrow != null)
            {
                iceArrow.transform.position = transform.position;

                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                iceArrow.Initialize(direction);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                iceArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else
            {
                Debug.LogWarning("�� ������� �������� ���� �� ����");
            }
        
    }

    private void IceHell()
    {
        StopAllCoroutines();
        cooldownTime = 2;
        SetPositions();
        Duration = 6f;
        isIceHell = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        
    }
    private new void OnDisable()
    {
        FullFillButtons.IceHell -= IceHell;
    }

    public override void CooldownReduction()
    {
        iceArrowPool.NewShootererCharacteristicsFireRate();
    }
    public void CooldownHelper(float cooldown)
    {
        cooldown = cooldown * cooldownMultiplicator * bonusCooldown;
        ChangeCooldown(cooldown);
    }

    protected override void DamageUpgrage()
    {

    }

    protected override void RadiusUpgrade()
    {

    }

    protected override void DurationUpgrade()
    {

    }

    protected override void CountUpgrade()
    {
        bonusNumberOfCount = iceArrowPool.bonusShooter;
        iceArrowPool.IsNeedToOnTheNextShooter();
    }
}
        
