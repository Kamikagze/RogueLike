using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireBreath : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] private FireBreathScriptableObject[] fireBreathScriptableObjects;
    [SerializeField] private Transform[] fireBreathScales;
    [SerializeField] HelperFireBreathAnim[] helpers;
    private float timerForNextExplosion; // Таймер до следующего взрыва
    private int nextExplosionIndex; // Индекс следующего взрыва
    public int fireBreathLevel = 0;
    public float waitTime;  
    public float FireBreathDamage { get; private set; }

    public static event Action FireBreathDamageIncrease;
    private void Start()
    {
        TurnerOn();
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        FireBreathScriptableObject.FireBreathUpgrade += Reinitialize;
        Activate();
        
    }

    private void TurnerOn()
    {        
       
            for (int i = 0; i < helpers.Length; ++i)
            {
                helpers[i].gameObject.SetActive(true);
            }
        
    }

    protected override void ActionOfAbill()
    {
        ChecerSize();
        nextExplosionIndex = 0;
        timerForNextExplosion = 0;
    }

    protected override void Offers()
    {
        Offer();
    }

    protected override void DurationPartOfAbill(float deltaTime)
    {
        timerForNextExplosion += deltaTime;
        if (timerForNextExplosion >= 0.1f)
        {
            ActivateNextExplosion();
            timerForNextExplosion = 0f;
        }
    }

    private void ActivateNextExplosion()
    {
        if (nextExplosionIndex < helpers.Length)
        {
            // Активируем взрыв по текущему индексу
            helpers[nextExplosionIndex].StarterEffect();
        }
        else
        {
            // Если достигли конца массива, запускаем цикл
            nextExplosionIndex = 0;
            helpers[nextExplosionIndex].StarterEffect(); // Активируем первый элемент
        }

        // Увеличиваем индекс для следующего взрыва
        nextExplosionIndex++;
    }


  

    private void Offer()
    {        
            for (int i = 0; i < helpers.Length; i++)
            {
                helpers[i].Offer();
                helpers[i].ReturnParent();
            }       
    }
   
    
    
    
    protected override void DamageUpgrage()
    {
        FireBreathDamage = fireBreathScriptableObjects[fireBreathLevel].fireBreathDamage;
        FireBreathDamageIncrease?.Invoke();
    }
    public override void CooldownReduction()
    {
        Duration = fireBreathScriptableObjects[fireBreathLevel].fireBreathDuration;

        waitTime = fireBreathScriptableObjects[fireBreathLevel].fireBreathCooldown * statsHolder.CooldownReduction * cooldownMultiplicator;
        ChangeCooldown(waitTime);
    }
    protected override void RadiusUpgrade()
    {
        if (fireBreathScales != null)
        {
            for (int i = 0; i < fireBreathScales.Length; i++)
            {
                fireBreathScales[i].localScale = new Vector2(fireBreathScriptableObjects[fireBreathLevel].fireBreathRadius * statsHolder.Radius,
                   fireBreathScriptableObjects[fireBreathLevel].fireBreathRadius * statsHolder.Radius);
            }
        }
        
    }
    public void ChecerSize()
    {
        if (fireBreathScales[0].localScale != fireBreathScales[1].localScale || 
            fireBreathScales[0].localScale != fireBreathScales[2].localScale ||
                fireBreathScales[1].localScale != fireBreathScales[2].localScale)
        {
            RadiusUpgrade();
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        FireBreathScriptableObject.FireBreathUpgrade -= Reinitialize;
    }

    

    

    protected override void DurationUpgrade()
    {
        Duration = fireBreathScriptableObjects[fireBreathLevel].fireBreathDuration * bonusDuration;
    }

    protected override void CountUpgrade()
    {

    }
}
   


