using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;


public class Meteor : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] Transform playerPos;
    [SerializeField] private Transform[] meteorColliders;
    [SerializeField] private MeteorScriptableObject[] meteorScriptableObjects;
    [SerializeField] private GameObject[] meteors;
    private float currentTime;
    private int meteorNumber; // номер запускаемого метеорита
    private float meteorCooldownBetweenActions = 0.15F;

    public int meteorCount = 1;
    public float meteorCooldown;
    private float lifeTime = 0.5f;
    private int meteorBonusCount = 0; //предметы усиливающие способности
    public float MeteorDamage { get; private set; } // Надо подумать на счет свойств или избавления от этого метода



    // Для глобального усиления (молния + метеор = звездопад)
    private bool isStarFall; // флаг соcтояния эпического улучшения
    private float starFallDuration = 6f;


    public static event Action MeteorActionEvent;
    private void Start()
    {
       
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        MeteorScriptableObject.MeteorUpgradeEvent += Reinitialize;
        FullFillButtons.StarFall += StarFallStarter;
    
        Activate();
        

    }

    protected override void ActionOfAbill()
    {
        currentTime = 0;
        meteorNumber = 0;
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
        currentTime += deltaTime;

        if (currentTime >= meteorCooldownBetweenActions)
        {
            SetPositionOfCrater(meteorNumber);
            meteorNumber++;
            if (meteorNumber >= meteorCount) { meteorNumber = 0; }
            currentTime = 0;
        }
            
        
    }
   
//глобальное улучшение
    private void StarFallStarter ()
    {
        StopAllCoroutines();
        isStarFall = true;
        Duration = starFallDuration;
        meteorCooldownBetweenActions = 0.25f;
    }
   

    private IEnumerator LifeTime(int i)
    {
        yield return new WaitForSeconds(lifeTime);
        meteors[i].SetActive(false);
    }
    public Vector2 GetRandomPosition()
    {
        // Определяем размеры прямоугольника
        float width = 3f; // Ширина прямоугольника
        float height = 4f; // Высота прямоугольника

        // Генерируем случайные координаты X и Y в пределах прямоугольника
        float randomX = UnityEngine.Random.Range(playerPos.position.x - width / 2, playerPos.position.x + width / 2);
        float randomY = UnityEngine.Random.Range(playerPos.position.y - height / 2, playerPos.position.y + height / 2);

        // Возвращаем новую позицию в виде вектора 2D
        return new Vector2(randomX, randomY); // Возвращаем 2D координаты
    }
    private void SetPositionOfCrater(int index)
    {
        meteors[index].transform.position = GetRandomPosition();
        meteors[index].gameObject.SetActive(true);
        StartCoroutine(LifeTime(index));
    }
    
   
   
    public override void CooldownReduction()
    {
        meteorCooldown = meteorScriptableObjects[abilityLevel].meteorCooldown
            * statsHolder.CooldownReduction * cooldownMultiplicator;
        ChangeCooldown(meteorCooldown);
    }

    protected override void RadiusUpgrade()
    {
        for (int i = 0; i < meteorColliders.Length; i++)
        {
            meteorColliders[i].localScale = new Vector2((meteorScriptableObjects[abilityLevel].meteorRadius * statsHolder.Radius *  bonusRadius), (meteorScriptableObjects[abilityLevel].meteorRadius
                * statsHolder.Radius * bonusRadius));
        }
            
    }
    protected override void CountUpgrade()
    {
        meteorCount = meteorScriptableObjects[abilityLevel].meteorCount + meteorBonusCount;
       
    }

    protected override void DamageUpgrage()
    {
        MeteorDamage = meteorScriptableObjects[abilityLevel].meteorDamage;
        MeteorActionEvent?.Invoke();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        MeteorScriptableObject.MeteorUpgradeEvent -= Reinitialize;
        FullFillButtons.StarFall -= StarFallStarter;
    }

    
 

    protected override void DurationUpgrade()
    {
        if (!isStarFall) { Duration = meteorCooldownBetweenActions * meteorCount * bonusDuration + 0.1f; }
    }


}

