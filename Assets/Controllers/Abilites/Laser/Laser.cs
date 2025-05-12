using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Laser : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    
    [SerializeField] private GameObject[] lasers;
   
    [SerializeField] private LaserScriptableObject[] laserScriptableObjects;

    [SerializeField] private Animator[] anim;
    private int activeLaser;
 

    private float currentTime = 0;
    
    public float waitTime;  // Время ожидания перед следующим перемещением

    [SerializeField] private Animator mainAnimator; // вращение

   

    private bool isRotating; // флаг вращения
    private float prewarmTime = 1.5F;
    public float LaserDamage { get; private set; }

    public static event Action LaserActionEvent;
    private void Start()
    {
        
        
       
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        LaserScriptableObject.LaserUpgrade += Reinitialize;
       
       
        Activate();
    }

   
    protected override void ActionOfAbill()
    {
        
        for (int i = 0; i < activeLaser; i++)
        {
            if (lasers[i].activeInHierarchy == false) lasers[i].SetActive(true);
        
        
            float initialAngle = GetRandomDirection();
            lasers[i].transform.rotation = Quaternion.Euler(0, 0, initialAngle);
            
            anim[i].SetBool("IsWork", true);

            currentTime = 0;
        }

    }
   
   
    private void FirstPhase() // прогрев лазера
    {
        if (currentTime >1.5F && !isRotating)
        {
            currentTime = 0;
            isRotating = true;
            mainAnimator.SetTrigger("StartRotation");
           
        }
    }
   

    
    protected override void DurationPartOfAbill(float deltaTime) 
    {
        currentTime += deltaTime;
        FirstPhase();
       
        
    }

    protected override void Offers()
    {
        isRotating = false;
        for (int i = 0; i < activeLaser; i++)
        {
            anim[i].SetBool("IsWork", false);
        }
        mainAnimator.ResetTrigger("StartRotation");
    }

    private float GetRandomDirection()
    {
        // Генерируем случайное направление (0 - 360 градусов)
        return UnityEngine.Random.Range(0f, 360f);
    }

    



    
    public override void CooldownReduction()
    {
        waitTime = laserScriptableObjects[abilityLevel].laserCooldown * statsHolder.CooldownReduction * cooldownMultiplicator;
        ChangeCooldown(waitTime);
    }

    protected override void DamageUpgrage()
    {
        LaserDamage = laserScriptableObjects[abilityLevel].laserDamage * bonusDamage;
        LaserActionEvent?.Invoke();
    }
    protected override void DurationUpgrade()
    {
        Duration = laserScriptableObjects[abilityLevel].laserDuration + prewarmTime; // добавляю время прогрева лазера
        
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
        LaserScriptableObject.LaserUpgrade -= Reinitialize;
    }

    

    protected override void RadiusUpgrade()
    {

    }

    

    protected override void CountUpgrade()
    {
        if (activeLaser != laserScriptableObjects[abilityLevel].laserCount + bonusNumberOfCount)
        {
            activeLaser = laserScriptableObjects[abilityLevel].laserCount + bonusNumberOfCount;

        }
    }
}

