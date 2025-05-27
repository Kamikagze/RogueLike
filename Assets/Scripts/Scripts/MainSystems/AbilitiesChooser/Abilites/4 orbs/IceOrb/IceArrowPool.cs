using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrowPool : MonoBehaviour
{
    public Shooter[] shooter;
    public GameObject iceArrowPrefab; // Префаб пули
    public int poolSize = 30; // Размер пула
    [SerializeField] private Transform parentPoolObject;
    public StatsHolder globalStats;

    private Queue<IceArrow> iceArrowPool; // Используем очередь для более легкого управления
    private IceArrow[] iceArrowArray; // массив для улучшений, из него никогда ничего не пропадет
    private bool isReInitializing;
    public int reint;
    private int numbersOfShooters = 1;
    public int bonusShooter = 0;

    public bool iceHell = false;
    public float IceArrowDamage { get; private set; }

  

    public static event Action IceArrowActionEvent;
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
        FullFillButtons.UpgradeIceArrow += ReInitialize;
        FullFillButtons.IceHell += IceHell;
       
        StatsHolder.DamageImproverIncreased += NewShootererCharacteristicsWithGlobalStats;
    }



    private void InitializePool()
    {
        iceArrowPool = new Queue<IceArrow>();

        for (int i = 0; i < poolSize; i++)
        {
            IceArrow iceArrow = Instantiate(iceArrowPrefab, parentPoolObject).GetComponent<IceArrow>();
            iceArrow.SetPool(this); // Связываем пулю с пулом
            
            iceArrow.gameObject.SetActive(false); // Деактивируем пулю
            iceArrowPool.Enqueue(iceArrow); // Добавляем в очередь
        }
    }
    private void CopyQueueToArray()
    {
        // Создаем массив нужного размера
        iceArrowArray = new IceArrow[iceArrowPool.Count];

        // Копируем элементы из очереди в массив
        int index = 0;
        foreach (IceArrow iceArrow in iceArrowPool)
        {
            iceArrowArray[index] = iceArrow;
            index++;
        }
    }
    public IceArrow GetIceArrow()
    {
        if (iceArrowPool.Count > 0)
        {
            IceArrow iceArrow = iceArrowPool.Dequeue(); // Достаём пулю из очереди
            iceArrow.gameObject.SetActive(true); // Активируем пулю

            return iceArrow;
        }

        // Если пул исчерпан, можно создать новый объект
        IceArrow newIceArrow = Instantiate(iceArrowPrefab, parentPoolObject).GetComponent<IceArrow>();
        newIceArrow.SetPool(this);
        newIceArrow.gameObject.SetActive(true);
        Debug.Log("New iceArrow created");
        return newIceArrow;
    }

    public void ReturnObject(IceArrow iceArrow)
    {
        // Проверяем, что пуля возвращается
        // Возможно, нужно добавить проверку
        if (iceArrow != null)
        {
            iceArrow.gameObject.SetActive(false); // Деактивируем объект
            iceArrowPool.Enqueue(iceArrow); // Возвращаем пулю в очередь
        }
        else
        {
            Debug.LogError("Trying to return a null iceArrow to pool.");
        }
    }
    private void ReInitialize(int level)
    {

        if (isReInitializing) return; // Проверяем флаг
        isReInitializing = true; // Устанавливаем флаг для предотвращения повторного вызова




        for (int i = 0; i < iceArrowArray.Length; i++) 
        {

            iceArrowArray[i].LevelUp(level); // Улучшаем каждый объект IceArrow
            Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }


        isReInitializing = false; // Сбрасываем флаг после завершения обработки
        NewShootererCharacteristicsWithGlobalStats();

        
    }
    private void NewShootererCharacteristicsWithGlobalStats()
    {
        IceArrow iceArrow = Peeker();
        NewShootererCharacteristicsFireRate();
        IsNeedToOnTheNextShooter();
        DamageFromIceArrowIncrease(iceArrow);
    }
    public void NewShootererCharacteristicsFireRate()
    {
        IceArrow iceArrow = iceArrowPool.Peek();
        for (int i = 0; i < shooter.Length; i++)
        {
            shooter[i].fireRate = iceArrow.levelsIseArrow[iceArrow.arrowLevel].iceArrowFireRate
                * globalStats.CooldownReduction;// проверка изменения скорострельности
            shooter[i].CooldownHelper(shooter[i].fireRate);
        }
    }
    public void IsNeedToOnTheNextShooter()
    {
        IceArrow iceArrow = iceArrowPool.Peek();
        if (iceArrow.levelsIseArrow[iceArrow.arrowLevel].iceArrowNumberOfShooters + bonusShooter != numbersOfShooters)
        {
            numbersOfShooters = iceArrow.levelsIseArrow[iceArrow.arrowLevel].iceArrowNumberOfShooters + bonusShooter;
            shooter[0].TurnerOnShooters(numbersOfShooters - 2);
        }
    }
    public void DamageFromIceArrowIncrease(IceArrow iceArrow)
    {
       
        IceArrowDamage = iceArrow.levelsIseArrow[iceArrow.arrowLevel].iceArrowDamage;
        IceArrowActionEvent?.Invoke();
    }
    private IceArrow Peeker()
    {
        IceArrow iceArrow = iceArrowPool.Peek();
        return iceArrow;
    }

    private void IceHell()
    {
        iceHell = true;

        

        shooter[0].OffAbill();

        shooter[1].OffAbill();
        shooter[3].OffAbill();
        
    }
    private void OnDisable()
    {
        FullFillButtons.UpgradeIceArrow -= ReInitialize;
        FullFillButtons.IceHell -= IceHell;
        StatsHolder.DamageImproverIncreased -= NewShootererCharacteristicsWithGlobalStats;
    }


}
