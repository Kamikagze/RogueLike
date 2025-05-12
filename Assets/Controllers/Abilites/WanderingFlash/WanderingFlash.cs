using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WanderingFlash : AbstractAbill
{
    [SerializeField] FounderOfEnemies Hash;
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] private CircleCollider2D wanderingFlashCollider;
    [SerializeField] private WanderingFlashScriptableObject[] wanderingFlashScriptableObjects;
    [SerializeField] private GameObject particles;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public int wanderingFlashCount = 1;
    public float wanderingFlashCooldown;


    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isMooving = false;
    private float currentTime = 0;
    private float duration;

    [Header("SecondWanderingFlash")]
    [SerializeField] private WanderingFlash secondFlash;

    //Epic RoamingLight
    [SerializeField] private TrailRenderer trailRenderer;
    private bool isRoamingLightning;

    // private int wanderingFlashBonusCount = 0; //предметы усиливающие способности
    public float WanderingFlashDamage { get; private set; } // Надо подумать на счет свойств или избавления от этого метода

    public static event Action WanderingFlashActionEvent;
    private void Start()
    {
        particles.gameObject.SetActive(true);
        DurationSetter(0.3f);
        StatsHolder.DamageImproverIncreased += DamageFromWanderingFlashIncrease;
        WanderingFlashScriptableObject.WanderingFlashUpgradeEvent += ReInitialize;
        Initialization();
        Activate();

        FullFillButtons.RoamingLight += RoamingLight;
    }


    private Transform GetHashOfEnemies(int index)
    {
        if (Hash.GetDetectedEnemies() != null && index >= 0 && index < Hash.GetDetectedEnemies().Count)
        {
            var arrayOfEnemies = new Transform[Hash.GetDetectedEnemies().Count];

            Hash.GetDetectedEnemies().CopyTo(arrayOfEnemies);

            return arrayOfEnemies[index];
        }
        return null; // Вернуть null, если Hash.GetDetectedEnemies() null или индекс вне диапазона
    }

    private Transform GetPositionWhereFlashShouldFly()
    {
        if (Hash.GetDetectedEnemies() != null && Hash.GetDetectedEnemies().Count > 0)
        {
            int randomEnemy = UnityEngine.Random.Range(0, Hash.GetDetectedEnemies().Count);
            return GetHashOfEnemies(randomEnemy);
        }
        return null; // Возвращаем null, так как врагов нет
    }

    protected override void ActionOfAbill()
    {
        if (Hash.GetDetectedEnemies() == null || Hash.GetDetectedEnemies().Count == 0) return;
        else if (!isRoamingLightning)
        {
            ActionOfWanderingFlash();
        }
        else if (isRoamingLightning)
        {
            ActionOfRoamingLight();
        }
    }
    private void ActionOfWanderingFlash()
    {
        Transform target = GetPositionWhereFlashShouldFly();
        if (target != null)
        {
            startPosition = transform.position;
            targetPosition = target.position;
            transform.position = targetPosition;
            wanderingFlashCollider.enabled = true;
            isMooving = true;
        }
    }
    private void ActionOfRoamingLight()
    {
        Transform target = GetPositionWhereFlashShouldFly();
        if (target != null)
        {
            startPosition = transform.position;
            targetPosition = target.position;
            isMooving = true;
        }
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
        if (isMooving)
        {
            if (!isRoamingLightning)
            {
                currentTime += deltaTime;

                if (currentTime < Duration)
                {
                    particles.transform.position = Vector2.Lerp(startPosition, targetPosition, currentTime / Duration);
                }
                else
                {
                    particles.transform.position = targetPosition; // Убедитесь, что particles находятся в конечной позиции

                }
            }
            else if (isRoamingLightning)
            {
                currentTime += deltaTime;

                if (currentTime < Duration)
                {
                    transform.position = Vector2.Lerp(startPosition, targetPosition, currentTime / Duration);
                }
                else
                {
                    transform.position = targetPosition; 

                }
            }

        }
    }
    protected override void Offers()
    {
        if (!isRoamingLightning)
        {
            wanderingFlashCollider.enabled = false;
            isMooving = false;
            currentTime = 0f;
            particles.transform.position = targetPosition;

        }
       else if (isRoamingLightning)
        {
            isMooving = false;
            currentTime = 0f;
            transform.position = targetPosition;
        }
        
    }
  
    private void DurationSetter(float value)
    {
        duration = value;
        Duration = duration + 0.15f;
    }
    private void Initialization()
    {
        CountOfWanderingFlashes();

        CooldownReduction();
        DamageFromWanderingFlashIncrease();
    }
    private void ReInitialize()
    {
        abilityLevel += 1;
        Initialization();
    }
    public override void CooldownReduction()
    
    {
        wanderingFlashCooldown = wanderingFlashScriptableObjects[abilityLevel].wanderingFlashCooldown
            * statsHolder.CooldownReduction * bonusCooldown;
        ChangeCooldown(wanderingFlashCooldown);
    }

    private void CountOfWanderingFlashes()
    {
        wanderingFlashCount = wanderingFlashScriptableObjects[abilityLevel].wanderingFlashCount;
        if (wanderingFlashCount == 2 && secondFlash != null && secondFlash.gameObject.activeInHierarchy == false)
        {
            secondFlash.gameObject.SetActive(true);
            InitializeSecondFlash();
        }
    }

    private void InitializeSecondFlash()
    {
        secondFlash.abilityLevel = this.abilityLevel;
        secondFlash.wanderingFlashCount = this.wanderingFlashCount;
        secondFlash.wanderingFlashCooldown = this.wanderingFlashCooldown;
        secondFlash.WanderingFlashDamage = this.WanderingFlashDamage;

        // Дополнительно, если у вас есть какие-либо скриптовые объекты или другие параметры, которые нужно инициализировать
        secondFlash.wanderingFlashScriptableObjects = this.wanderingFlashScriptableObjects;

        secondFlash.Initialization(); // Вызываем инициализацию, если в нем есть необходимость перезапуска
    }

    public void DamageFromWanderingFlashIncrease()
    {
        WanderingFlashDamage = wanderingFlashScriptableObjects[abilityLevel].wanderingFlashDamage;
        WanderingFlashActionEvent?.Invoke();
    }

    private void RoamingLight()
    {
        Debug.Log("RoamingLight");
        particles.SetActive(false);
        isRoamingLightning = true;
        wanderingFlashCollider.enabled = true;
        DurationSetter(0.7f);
        ChangeColorAndSize();


    }
    private void ChangeColorAndSize()
    {
        spriteRenderer.color = new Color(70 / 255f, 95 / 255f, 250 / 255f);
        transform.localScale = new Vector2 (transform.localScale.x * 2, transform.localScale.y * 2);
        trailRenderer.enabled = true;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageFromWanderingFlashIncrease;
        WanderingFlashScriptableObject.WanderingFlashUpgradeEvent -= ReInitialize;
        FullFillButtons.RoamingLight -= RoamingLight;
    }

    protected override void DamageUpgrage()
    {

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


