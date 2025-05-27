using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicBoltPool : AbstractAbill
{
    public ShooterOfMB shooter;
    public GameObject magicBoltPrefab; // ������ ����
    public int poolSize = 20; // ������ ����
    [SerializeField] private Transform parentPoolObject;
    public StatsHolder globalStats;

    private Queue<MagicBolt> magicBoltPool; // ���������� ������� ��� ����� ������� ����������
    private bool isReInitializing;
    public int reint;
    private float fireRate;

    public float MagicBoltDamage { get; private set; }

    public static event Action MagicBoltActionEvent;
    private bool poolInitialized = false;
    private void Awake()
    {
        if (!poolInitialized)
        {
            InitializePool();
            
            poolInitialized = true;
            gameObject.SetActive(false);

        }


    }
    private void Start()
    {
        FullFillButtons.UpgradeMagicBolt += ReInitialize;
        FullFillButtons.StarFall += Starfalling;
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        
        Activate();
    }


    private void Starfalling()
    {
        foreach (MagicBolt magicBolt in magicBoltPool)
        {
            Destroy(magicBolt);
        }
        gameObject.SetActive(false);
    }
    private void InitializePool()
    {
        magicBoltPool = new Queue<MagicBolt>();

        for (int i = 0; i < poolSize; i++)
        {
            MagicBolt magicBolt = Instantiate(magicBoltPrefab, parentPoolObject).GetComponent<MagicBolt>();
            magicBolt.SetPool(this); // ��������� ������

            magicBolt.gameObject.SetActive(false); // ������������ ������
            magicBoltPool.Enqueue(magicBolt); // ��������� � �������
        }
    }

    public MagicBolt GetMagicBolt()
    {
        if (magicBoltPool.Count > 0)
        {
            MagicBolt magicBolt = magicBoltPool.Dequeue(); // ������ ���� �� �������
            magicBolt.gameObject.SetActive(true); // ���������� ����

            return magicBolt;
        }

        // ���� ��� ��������, ����� ������� ����� ������
        MagicBolt newMagicBolt = Instantiate(magicBoltPrefab, parentPoolObject).GetComponent<MagicBolt>();
        newMagicBolt.SetPool(this);
        newMagicBolt.gameObject.SetActive(true);
        Debug.Log("New magicBolt created");
        return newMagicBolt;
    }

    public void ReturnObject(MagicBolt magicBolt)
    {
        // ���������, ��� ���� ������������
        // ��������, ����� �������� ��������
        if (magicBolt != null)
        {
            magicBolt.gameObject.SetActive(false); // ������������ ������
            magicBoltPool.Enqueue(magicBolt); // ���������� ���� � �������
        }
        else
        {
            Debug.LogError("Trying to return a null magicBolt to pool.");
        }
    }
    protected override void ActionOfAbill()
    {
        shooter.TryToShoot();
    }
    private void ReInitialize(int level)
    {

        if (isReInitializing) return; // ��������� ����
        isReInitializing = true; // ������������� ���� ��� �������������� ���������� ������
        abilityLevel = level;



        foreach (MagicBolt magicBolt in magicBoltPool)
        {
            magicBolt.LevelUp(level); // �������� ������ ������ MagicBolt
            Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }


        isReInitializing = false; // ���������� ���� ����� ���������� ���������
        Initialize();
    }
    
    protected override void CountUpgrade()
    {
        MagicBolt magicBolt = magicBoltPool.Peek();
        shooter.numberOfMagicBolt = magicBolt.levelsMagicBolt[abilityLevel].magicBoltNumberOfLightnings + bonusNumberOfCount;

    }
    public override void CooldownReduction()
    {
        MagicBolt magicBolt = magicBoltPool.Peek();
        if (magicBolt != null)
        {
            fireRate = magicBolt.levelsMagicBolt[abilityLevel].magicBoltFireRate
                * globalStats.CooldownReduction * cooldownMultiplicator;
            ChangeCooldown(fireRate);
        }
        else return;

    }
    protected override void DamageUpgrage()
    {
        MagicBolt magicBolt = magicBoltPool.Peek();
        MagicBoltDamage = magicBolt.levelsMagicBolt[abilityLevel].magicBoltDamage * bonusDamage;
        MagicBoltActionEvent?.Invoke();
    }
    public MagicBolt Peeker()
    {
        MagicBolt magicBolt = magicBoltPool.Peek();
        return magicBolt;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        FullFillButtons.UpgradeMagicBolt -= ReInitialize;

        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
    }

   
    protected override void RadiusUpgrade()
    {

    }

    protected override void DurationUpgrade()
    {

    }

}