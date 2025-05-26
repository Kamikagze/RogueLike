using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : AbstractAbill
{
    public ShooterOfFB shooter;
    public GameObject fireBallPrefab; // Префаб пули
    public int poolSize = 10; // Размер пула
    [SerializeField] private Transform parentPoolObject;

    public StatsHolder globalStats;

    private Queue<FireBall> fireBallPool; // Используем очередь для более легкого управления
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
            fireBall.SetPool(this); // Связываем пулю с пулом

            fireBall.gameObject.SetActive(false); // Деактивируем пулю
            fireBallPool.Enqueue(fireBall); // Добавляем в очередь
        }
    }
    private void CopyQueueToArray()
    {
        // Создаем массив нужного размера
        fireBallsArray = new FireBall[fireBallPool.Count];

        // Копируем элементы из очереди в массив
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
            FireBall fireBall = fireBallPool.Dequeue(); // Достаём пулю из очереди
            fireBall.gameObject.SetActive(true); // Активируем пулю

            return fireBall;
        }

        // Если пул исчерпан, можно создать новый объект
        FireBall newFireBall = Instantiate(fireBallPrefab, parentPoolObject).GetComponent<FireBall>();
        newFireBall.SetPool(this);
        newFireBall.gameObject.SetActive(true);
        Debug.Log("New fireBall created");
        return newFireBall;
    }

    public void ReturnObject(FireBall fireBall)
    {
        // Проверяем, что пуля возвращается
        // Возможно, нужно добавить проверку
        if (fireBall != null)
        {
            fireBall.gameObject.SetActive(false); // Деактивируем объект
            fireBallPool.Enqueue(fireBall); // Возвращаем пулю в очередь
        }
        else
        {
            Debug.LogError("Trying to return a null fireBall to pool.");
        }
    }
    private void ReInitialize(int level)
    {

        if (isReInitializing) return; // Проверяем флаг
        isReInitializing = true; // Устанавливаем флаг для предотвращения повторного вызова

        abilityLevel = level;


        for (int i = 0; i <fireBallPool.Count; i++) 
        {
            fireBallsArray[i].LevelUp(level); // Улучшаем каждый объект FireBall
          
            Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }
        RadiusUpgrade();

        isReInitializing = false; // Сбрасываем флаг после завершения обработки
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
                * globalStats.CooldownReduction * cooldownMultiplicator;// проверка изменения скорострельности
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
