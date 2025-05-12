using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : AbstractAbill
{
    public ShooterOfFB shooter;
    public GameObject fireBallPrefab; // ������ ����
    public int poolSize = 10; // ������ ����
    [SerializeField] private Transform parentPoolObject;

    public StatsHolder globalStats;

    private Queue<FireBall> fireBallPool; // ���������� ������� ��� ����� ������� ����������
    private FireBall[] fireBallsArray;
    private bool isReInitializing = false;
    public int reint;

    public float fireRate = 3f;
    public float FireBallDamage { get; private set; }

    public static event Action FireBallActionEvent;
    private bool poolInitialized = false;
    private void Awake()
    {
        if (!poolInitialized)
        {
            InitializePool();
            CopyQueueToArray();
            poolInitialized = true;
            gameObject.SetActive(false);

        }


    }

    private void Start()
    {
        FullFillButtons.UpgradeFireBall += ReInitialize;
        StatsHolder.CooldownReductionIncreased += NewShootererCharacteristicsWithGlobalStats;
        StatsHolder.DamageImproverIncreased += NewShootererCharacteristicsWithGlobalStats;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        NewShootererCharacteristicsWithGlobalStats();
        Activate();
    }


    protected override void ActionOfAbill()
    {
        shooter.ShootMethod();
    }
    private void InitializePool()
    {
        fireBallPool = new Queue<FireBall>();

        for (int i = 0; i < poolSize; i++)
        {
            FireBall fireBall = Instantiate(fireBallPrefab, parentPoolObject).GetComponent<FireBall>();
            fireBall.SetPool(this); // ��������� ���� � �����

            fireBall.gameObject.SetActive(false); // ������������ ����
            fireBallPool.Enqueue(fireBall); // ��������� � �������
        }
    }
    private void CopyQueueToArray()
    {
        // ������� ������ ������� �������
        fireBallsArray = new FireBall[fireBallPool.Count];

        // �������� �������� �� ������� � ������
        int index = 0;
        foreach (FireBall iceArrow in fireBallPool)
        {
            fireBallsArray[index] = iceArrow;
            index++;
        }
    }

    public FireBall GetFireBall()
    {
        if (fireBallPool.Count > 0)
        {
            FireBall fireBall = fireBallPool.Dequeue(); // ������ ���� �� �������
            fireBall.gameObject.SetActive(true); // ���������� ����

            return fireBall;
        }

        // ���� ��� ��������, ����� ������� ����� ������
        FireBall newFireBall = Instantiate(fireBallPrefab, parentPoolObject).GetComponent<FireBall>();
        newFireBall.SetPool(this);
        newFireBall.gameObject.SetActive(true);
        Debug.Log("New fireBall created");
        return newFireBall;
    }

    public void ReturnObject(FireBall fireBall)
    {
        // ���������, ��� ���� ������������
        // ��������, ����� �������� ��������
        if (fireBall != null)
        {
            fireBall.gameObject.SetActive(false); // ������������ ������
            fireBallPool.Enqueue(fireBall); // ���������� ���� � �������
        }
        else
        {
            Debug.LogError("Trying to return a null fireBall to pool.");
        }
    }
    private void ReInitialize(int level)
    {

        if (isReInitializing) return; // ��������� ����
        isReInitializing = true; // ������������� ���� ��� �������������� ���������� ������

        abilityLevel = level;


        for (int i = 0; i <fireBallPool.Count; i++) 
        {
            fireBallsArray[i].LevelUp(level); // �������� ������ ������ FireBall
          
            Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }
        RadiusUpgrade();

        isReInitializing = false; // ���������� ���� ����� ���������� ���������
        NewShootererCharacteristicsWithGlobalStats();
    }
    private void NewShootererCharacteristicsWithGlobalStats()
    {
        FireBall fireBall = Peeker();
        CooldownReduction();
        
        DamageUpgrage();
    }
    public override void CooldownReduction()
    {
        FireBall fireBall = Peeker();
       
        fireRate = fireBall.levelsIseFireBall[abilityLevel].fireBallFireRate
                * globalStats.CooldownReduction * cooldownMultiplicator;// �������� ��������� ����������������
        ChangeCooldown(fireRate);
       
    }

    protected override void DamageUpgrage()
    {
        FireBall fireBall = fireBallPool.Peek();
        FireBallDamage = fireBall.levelsIseFireBall[abilityLevel].fireBallDamage * bonusDamage;
        FireBallActionEvent?.Invoke();
    }
    private FireBall Peeker()
    {
            FireBall fireBall = fireBallPool.Peek();
            return fireBall;
           
    }

    protected override void RadiusUpgrade()
    {
        
        for (int i = 0; i < fireBallPool.Count; i++)
        {
            fireBallsArray[i].transform.localScale = new Vector2(fireBallsArray[i].levelsIseFireBall[fireBallsArray[i].fireBallLevel].fireBallRadius * globalStats.Radius * bonusRadius,
            fireBallsArray[i].levelsIseFireBall[fireBallsArray[i].fireBallLevel].fireBallRadius * globalStats.Radius * bonusRadius);
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        FullFillButtons.UpgradeFireBall -= ReInitialize;

        StatsHolder.DamageImproverIncreased -= NewShootererCharacteristicsWithGlobalStats;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
    }

  

    

    protected override void DurationUpgrade()
    {

    }

    protected override void CountUpgrade()
    {

    }
}
