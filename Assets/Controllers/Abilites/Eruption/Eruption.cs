
using UnityEngine;

public class Eruption : AbstractAbill
{
    [SerializeField] private StatsHolder statsHolder; // ������ ���������
    [SerializeField] private GameObject[] eruptions; // ������ �������� �������
    [SerializeField] private float duration = 5f; // ������������ �������� �����������
    [SerializeField] private float cooldown = 10f; // ����� �����������

    protected override float Duration => duration;

    //��� ������� ��� ���� ��� ����������� ������������
    private float timerForNextExplosion; // ������ �� ���������� ������
    private int nextExplosionIndex; // ������ ���������� ������

    

    private void Start()
    {
        cooldownTime = cooldown; // ������������� ����� �����������
        
        Activate(); // ���������� ����������� ����� ��� ������
    }

    protected override void ActionOfAbill()
    {
        Starting();
    }


    private void Starting()
    {
        if (isInWork) return; // ���������, ���� ����������� ��� �������

        // ������������ ������ ����� ����������
        Shuffle(eruptions);

        // ��������� ������ 7 �������
        for (int i = 0; i < 7 && i < eruptions.Length; i++)
        {
            eruptions[i].SetActive(true);
        }

        
       
        timerForNextExplosion = 0; // ���������� ������ ��� ���������� ������
        nextExplosionIndex = 7; // ������������� ������ ���������� ������ (8-� �������)
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
                             
        timerForNextExplosion += deltaTime;

        // ���� ����� ��� ���������� ������ ������
        if (timerForNextExplosion >= 0.07f)
        {
            ActivateNextExplosion(); // ���������� ��������� �����
            timerForNextExplosion = 0f; // ���������� ������
        }
    }
    

    private void ActivateNextExplosion()
    {
        if (nextExplosionIndex < eruptions.Length)
        {
            // ���������� ����� �� �������� �������
            eruptions[nextExplosionIndex].SetActive(true);
        }
        else
        {
            // ���� �������� ����� �������, ��������� ����
            nextExplosionIndex = 0;
            eruptions[nextExplosionIndex].SetActive(true); // ���������� ������ �������
        }

        // ����������� ������ ��� ���������� ������
        nextExplosionIndex++;
    }


    public override void CooldownReduction()
    {
        cooldown *= statsHolder.CooldownReduction * cooldownMultiplicator * bonusCooldown; // ��������� ����� �����������
        if (IsActive)
        {
            ChangeCooldown(cooldown); // �������� �����������, ���� ����������� �������
        }
    }
    
    private void Shuffle(GameObject[] array)
    {
        int n = array.Length;
        System.Random rng = new System.Random();
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    protected override void DamageUpgrage()
    {
       
    }

    protected override void RadiusUpgrade()
    {
        
    }

    protected override void DurationUpgrade()
    {
        Duration = duration * bonusDuration;
    }

    protected override void CountUpgrade()
    {
       
    }
}