using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrowPool : MonoBehaviour
{
    public Shooter[] shooter;
    public GameObject iceArrowPrefab; // ������ ����
    public int poolSize = 30; // ������ ����
    [SerializeField] private Transform parentPoolObject;
    public StatsHolder globalStats;

    private Queue<IceArrow> iceArrowPool; // ���������� ������� ��� ����� ������� ����������
    private IceArrow[] iceArrowArray; // ������ ��� ���������, �� ���� ������� ������ �� ��������
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
            iceArrow.SetPool(this); // ��������� ���� � �����
            
            iceArrow.gameObject.SetActive(false); // ������������ ����
            iceArrowPool.Enqueue(iceArrow); // ��������� � �������
        }
    }
    private void CopyQueueToArray()
    {
        // ������� ������ ������� �������
        iceArrowArray = new IceArrow[iceArrowPool.Count];

        // �������� �������� �� ������� � ������
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
            IceArrow iceArrow = iceArrowPool.Dequeue(); // ������ ���� �� �������
            iceArrow.gameObject.SetActive(true); // ���������� ����

            return iceArrow;
        }

        // ���� ��� ��������, ����� ������� ����� ������
        IceArrow newIceArrow = Instantiate(iceArrowPrefab, parentPoolObject).GetComponent<IceArrow>();
        newIceArrow.SetPool(this);
        newIceArrow.gameObject.SetActive(true);
        Debug.Log("New iceArrow created");
        return newIceArrow;
    }

    public void ReturnObject(IceArrow iceArrow)
    {
        // ���������, ��� ���� ������������
        // ��������, ����� �������� ��������
        if (iceArrow != null)
        {
            iceArrow.gameObject.SetActive(false); // ������������ ������
            iceArrowPool.Enqueue(iceArrow); // ���������� ���� � �������
        }
        else
        {
            Debug.LogError("Trying to return a null iceArrow to pool.");
        }
    }
    private void ReInitialize(int level)
    {

        if (isReInitializing) return; // ��������� ����
        isReInitializing = true; // ������������� ���� ��� �������������� ���������� ������




        for (int i = 0; i < iceArrowArray.Length; i++) 
        {

            iceArrowArray[i].LevelUp(level); // �������� ������ ������ IceArrow
            Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }


        isReInitializing = false; // ���������� ���� ����� ���������� ���������
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
                * globalStats.CooldownReduction;// �������� ��������� ����������������
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
