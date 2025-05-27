using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StonePickePool : AbstractAbill
{
    public ShooterOfSP shooter;
    public GameObject stonePickePrefab; // ������ ����
    public int poolSize = 20; // ������ ����
    [SerializeField] private Transform parentPoolObject;
    public StatsHolder globalStats;
    [SerializeField] private GameObject eruption;
    private Queue<StonePicke> stonePickePool; // ���������� ������� ��� ����� ������� ����������
    private StonePicke[] stonePickeArray;
    private bool isReInitializing;
    public int reint;

    private float fireRate = 0;
    private float currentTime;
    
    public float RadiusOfSpawn { get; private set; }

    private StonePicke stonePicke;
    public float StonePickeDamage { get; private set; }
    public float StonePickeCooldown { get; private set; }

    public static event Action StonePickeActionEvent;


    private bool poolInitialized = false;
    private void Awake()
    {
        if (!poolInitialized)
        {
            InitializePool();
            Peeker();
            CopyQueueToArray();
            poolInitialized = true;
            gameObject.SetActive(false);

        }


    }

    private void Start()
    {
        FullFillButtons.UpgradeStonePicke += ReInitialize;
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        FullFillButtons.Eruption += EruptionStart;
        ReInitialize(0);
        Activate();
    }
    private void CopyQueueToArray()
    {
        // ������� ������ ������� �������
        stonePickeArray = new StonePicke[stonePickePool.Count];

        // �������� �������� �� ������� � ������
        int index = 0;
        foreach (StonePicke stonePicke in stonePickePool)
        {
            stonePickeArray[index] = stonePicke;
            index++;
        }
    }
    protected override void ActionOfAbill()
    {
        
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime >= fireRate)
        {
            shooter.ShootMethod();
            currentTime = 0;
        }

    }

    private void EruptionStart()
    {
        foreach (StonePicke stonePicke in stonePickePool)
        {
           Destroy(stonePicke);
        }
        OffAbill();
        eruption.SetActive(true);

    }

    private void InitializePool()
    {
        stonePickePool = new Queue<StonePicke>();

        for (int i = 0; i < poolSize; i++)
        {
            StonePicke stonePicke = Instantiate(stonePickePrefab, parentPoolObject).GetComponent<StonePicke>();
            stonePicke.SetPool(this); // ��������� ���� � �����

            stonePicke.gameObject.SetActive(false); // ������������ ����
            stonePickePool.Enqueue(stonePicke); // ��������� � �������
        }
    }

    public StonePicke GetStonePicke()
    {
        if (stonePickePool.Count > 0)
        {
            StonePicke stonePicke = stonePickePool.Dequeue(); // ������ ���� �� �������
            stonePicke.gameObject.SetActive(true); // ���������� ����

            return stonePicke;
        }

        // ���� ��� ��������, ����� ������� ����� ������
        StonePicke newStonePicke = Instantiate(stonePickePrefab, parentPoolObject).GetComponent<StonePicke>();
        newStonePicke.SetPool(this);
        newStonePicke.gameObject.SetActive(true);
        Debug.Log("New stonePicke created");
        return newStonePicke;
    }

    public void ReturnObject(StonePicke stonePicke)
    {
        // ���������, ��� ���� ������������
        // ��������, ����� �������� ��������
        if (stonePicke != null)
        {
            stonePicke.gameObject.SetActive(false); // ������������ ������
            stonePickePool.Enqueue(stonePicke); // ���������� ���� � �������
        }
        else
        {
            Debug.LogError("Trying to return a null stonePicke to pool.");
        }
    }
    private void ReInitialize(int level)
    {

        if (isReInitializing) return; // ��������� ����
        isReInitializing = true; // ������������� ���� ��� �������������� ���������� ������
        abilityLevel = level;



        for (int i = 0; i < stonePickeArray.Length; i++)
        {

            stonePickeArray[i].LevelUp(level); // �������� ������ ������ StonePicke
            Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }


        isReInitializing = false; // ���������� ���� ����� ���������� ���������
        Peeker();
        Initialize();
    }
   
    protected override void DurationUpgrade()
    {
       
        Duration = stonePicke.levelsIseStonePicke[abilityLevel].stonePickeDuration * bonusDuration ;
    }

    protected override void DamageUpgrage()
    {

        StonePickeDamage = stonePicke.levelsIseStonePicke[abilityLevel].stonePickeDamage * bonusDamage;
        StonePickeActionEvent?.Invoke();
    }
    protected override void RadiusUpgrade()
    {
       
        RadiusOfSpawn = stonePicke.levelsIseStonePicke[abilityLevel].stonePickeRadius *
            globalStats.Radius;
    }
    private void Peeker()
    {
        stonePicke = stonePickePool.Peek();
        
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        FullFillButtons.UpgradeStonePicke -= ReInitialize;
       
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
    }

    public override void CooldownReduction()
    {
        
        StonePickeCooldown = stonePicke.levelsIseStonePicke[abilityLevel].stonePikeCooldown *
            globalStats.CooldownReduction * bonusCooldown;
        ChangeCooldown(StonePickeCooldown);
        fireRate = stonePicke.levelsIseStonePicke[abilityLevel].stonePickeFireRate
                * globalStats.CooldownReduction;// �������� ��������� ����������������
    }

 


    

    protected override void CountUpgrade()
    {
        
    }
}
