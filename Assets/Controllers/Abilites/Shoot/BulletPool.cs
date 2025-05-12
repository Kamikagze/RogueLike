using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : AbstractAbill
{
    public Shoot shoot;
    public GameObject bulletPrefab; // Префаб пули
    public int poolSize = 10; // Размер пула
    [SerializeField] private Transform parentPoolObject;
    public StatsHolder globalStats;

    private Queue<Bullet> bulletPool; // Используем очередь для более легкого управления
    private Bullet[] bulletArray;
    private bool isReInitializing;
    public int reint;

    private float currentTime;
    public bool doubleShot = false;
    public bool tripleShot = false;
    public float fireRate; // Задержка между выстрелами

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
        // Создаем массив нужного размера
        bulletArray = new Bullet[bulletPool.Count];

        // Копируем элементы из очереди в массив
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
            bullet.SetPool(this); // Связываем пулю с пулом
            bullet.SetGlobalStats(globalStats); // связываю с глобальными усилениями
            bullet.gameObject.SetActive(false); // Деактивируем пулю
            bulletPool.Enqueue(bullet); // Добавляем в очередь
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
            Bullet bullet = bulletPool.Dequeue(); // Достаём пулю из очереди
            bullet.gameObject.SetActive(true); // Активируем пулю
            
            return bullet;
        }

        // Если пул исчерпан, можно создать новый объект
        Bullet newBullet = Instantiate(bulletPrefab, parentPoolObject).GetComponent<Bullet>();
        newBullet.SetPool(this);
        newBullet.gameObject.SetActive(true);
        //Debug.Log("New bullet created");
        return newBullet;
    }

    public void ReturnObject(Bullet bullet)
    {
        // Проверяем, что пуля возвращается
                                              // Возможно, нужно добавить проверку
        if (bullet != null)
        {
            bullet.gameObject.SetActive(false); // Деактивируем объект
            bulletPool.Enqueue(bullet); // Возвращаем пулю в очередь
        }
        else
        {
            Debug.LogError("Trying to return a null bullet to pool.");
        }
    }
    private void ReInitialize(int level)
    {
       
        if (isReInitializing) return; // Проверяем флаг
        isReInitializing = true; // Устанавливаем флаг для предотвращения повторного вызова
        abilityLevel = level;

        

        for (int i = 0; i < bulletArray.Length; i++) 
        {
            bulletArray[i].LevelUp(level); // Улучшаем каждый объект Bullet
            //Debug.Log("+++++++++++++++++++++++++++++++++++++++");
        }

        isReInitializing = false; // Сбрасываем флаг после завершения обработки
        NewShooterCharacteristicsWithGlobalStats();
        DamageUpgrage();
        CooldownReduction();
    }
    private void NewShooterCharacteristicsWithGlobalStats()
    {
        
       
        Bullet bullet = bulletPool.Peek();
        if (bullet.levelsOfBullet[bullet.bulletLevel].doubleShot) // проверка изменения двойного выстрела
        {
           doubleShot = true;
            Duration = 0.17f;
        }
        if (bullet.levelsOfBullet[bullet.bulletLevel].tripleShot) // проверка изменения тройного выстрела
        {
           tripleShot = true;
            Duration = 0.27f;
        }
    }
    public override void CooldownReduction()
    {
        Bullet bullet = bulletPool.Peek();
        fireRate = bullet.levelsOfBullet[bullet.bulletLevel].bulletFireRate
            * globalStats.CooldownReduction* bonusCooldown;// проверка изменения скорострельности
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