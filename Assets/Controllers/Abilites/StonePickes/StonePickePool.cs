using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StonePickePool : AbstractAbill
{
    public ShooterOfSP shooter;
    public GameObject stonePickePrefab; // Префаб пули
    public int poolSize = 20; // Размер пула
    [SerializeField] private Transform parentPoolObject;
    public StatsHolder globalStats;
    [SerializeField] private GameObject eruption;
    private Queue<StonePicke> stonePickePool; // Используем очередь для более легкого управления
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
        // Создаем массив нужного размера
        stonePickeArray = new StonePicke[stonePickePool.Count];

        // Копируем элементы из очереди в массив
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
            stonePicke.SetPool(this); // Связываем пулю с пулом

            stonePicke.gameObject.SetActive(false); // Деактивируем пулю
            stonePickePool.Enqueue(stonePicke); // Добавляем в очередь
        }
    }

    public StonePicke GetStonePicke()
    {
        if (stonePickePool.Count > 0)
        {
            StonePicke stonePicke = stonePickePool.Dequeue(); // Достаём пулю из очереди
            stonePicke.gameObject.SetActive(true); // Активируем пулю

            return stonePicke;
        }

        // Если пул исчерпан, можно создать новый объект
        StonePicke newStonePicke = Instantiate(stonePickePrefab, parentPoolObject).GetComponent<StonePicke>();
        newStonePicke.SetPool(this);
        newStonePicke.gameObject.SetActive(true);
        Debug.Log("New stonePicke created");
        return newStonePicke;
    }

    public void ReturnObject(StonePicke stonePicke)
    {
        // Проверяем, что пуля возвращается
        // Возможно, нужно добавить проверку
        if (stonePicke != null)
        {
            stonePicke.gameObject.SetActive(false); // Деактивируем объект
            stonePickePool.Enqueue(stonePicke); // Возвращаем пулю в очередь
        }
        else
        {
            Debug.LogError("Trying to return a null stonePicke to pool.");
        }
    }
    private void ReInitialize(int level)
    {

        if (isReInitializing) return; // Проверяем флаг
        isReInitializing = true; // Устанавливаем флаг для предотвращения повторного вызова
        abilityLevel = level;



        for (int i = 0; i < stonePickeArray.Length; i++)
        {

            stonePickeArray[i].LevelUp(level); // Улучшаем каждый объект StonePicke
            Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }


        isReInitializing = false; // Сбрасываем флаг после завершения обработки
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
                * globalStats.CooldownReduction;// проверка изменения скорострельности
    }

 


    

    protected override void CountUpgrade()
    {
        
    }
}
