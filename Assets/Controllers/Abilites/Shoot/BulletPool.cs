using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : AbstractAbill
{
    public Shoot shoot;
    public GameObject bulletPrefab; // ������ ����
    public int poolSize = 10; // ������ ����
    [SerializeField] private Transform parentPoolObject;
    public StatsHolder globalStats;

    private Queue<Bullet> bulletPool; // ���������� ������� ��� ����� ������� ����������
    private Bullet[] bulletArray;
    private bool isReInitializing;
    public int reint;

    private float currentTime;
    public bool doubleShot = false;
    public bool tripleShot = false;
    public float fireRate; // �������� ����� ����������

    public float BulletDamage {  get; private set; }
    public static event Action BulletDamageActionEvent;
    private void Awake()
    {
        InitializePool();
        CopyQueueToArray();
        ReInitialize(0);
        DamageUpgrage();
        Activate();
        
    }

    private void Start()
    {
       FullFillButtons.UpgradeBullet += ReInitialize;
        
    }


    private void CopyQueueToArray()
    {
        // ������� ������ ������� �������
        bulletArray = new Bullet[bulletPool.Count];

        // �������� �������� �� ������� � ������
        int index = 0;
        foreach (Bullet bullet in bulletPool)
        {
            bulletArray[index] = bullet;
            index++;
        }
    }
    private void InitializePool()
    {
        bulletPool = new Queue<Bullet>();

        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab,parentPoolObject).GetComponent<Bullet>();
            bullet.SetPool(this); // ��������� ���� � �����
            bullet.SetGlobalStats(globalStats); // �������� � ����������� ����������
            bullet.gameObject.SetActive(false); // ������������ ����
            bulletPool.Enqueue(bullet); // ��������� � �������
        }
    }


    protected override void ActionOfAbill()
    {
        currentTime = 0;
        shoot.ShootMethod();
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime >= 0.1f)
        {
            shoot.ShootMethod();
            currentTime = 0;
        }
    }
    public Bullet GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            Bullet bullet = bulletPool.Dequeue(); // ������ ���� �� �������
            bullet.gameObject.SetActive(true); // ���������� ����
            
            return bullet;
        }

        // ���� ��� ��������, ����� ������� ����� ������
        Bullet newBullet = Instantiate(bulletPrefab, parentPoolObject).GetComponent<Bullet>();
        newBullet.SetPool(this);
        newBullet.gameObject.SetActive(true);
        //Debug.Log("New bullet created");
        return newBullet;
    }

    public void ReturnObject(Bullet bullet)
    {
        // ���������, ��� ���� ������������
                                              // ��������, ����� �������� ��������
        if (bullet != null)
        {
            bullet.gameObject.SetActive(false); // ������������ ������
            bulletPool.Enqueue(bullet); // ���������� ���� � �������
        }
        else
        {
            Debug.LogError("Trying to return a null bullet to pool.");
        }
    }
    private void ReInitialize(int level)
    {
       
        if (isReInitializing) return; // ��������� ����
        isReInitializing = true; // ������������� ���� ��� �������������� ���������� ������
        abilityLevel = level;

        

        for (int i = 0; i < bulletArray.Length; i++) 
        {
            bulletArray[i].LevelUp(level); // �������� ������ ������ Bullet
            //Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }

        isReInitializing = false; // ���������� ���� ����� ���������� ���������
        NewShooterCharacteristicsWithGlobalStats();
        DamageUpgrage();
        CooldownReduction();
    }
    private void NewShooterCharacteristicsWithGlobalStats()
    {
        
       
        Bullet bullet = bulletPool.Peek();
        if (bullet.levelsOfBullet[bullet.bulletLevel].doubleShot) // �������� ��������� �������� ��������
        {
           doubleShot = true;
            Duration = 0.17f;
        }
        if (bullet.levelsOfBullet[bullet.bulletLevel].tripleShot) // �������� ��������� �������� ��������
        {
           tripleShot = true;
            Duration = 0.27f;
        }
    }
    public override void CooldownReduction()
    {
        Bullet bullet = bulletPool.Peek();
        fireRate = bullet.levelsOfBullet[bullet.bulletLevel].bulletFireRate
            * globalStats.CooldownReduction* bonusCooldown;// �������� ��������� ����������������
        ChangeCooldown(fireRate);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        FullFillButtons.UpgradeBullet -= ReInitialize;
        
    }

    protected override void DamageUpgrage()
    {
        Bullet bullet = bulletPool.Peek();
        BulletDamage = bullet.levelsOfBullet[abilityLevel].bulletDamage * bonusDamage;
        BulletDamageActionEvent?.Invoke();
    }

    protected override void RadiusUpgrade()
    {
     
    }

    protected override void DurationUpgrade()
    {
     
    }

    protected override void CountUpgrade()
    {
   
    }
}