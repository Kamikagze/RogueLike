using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ElectricOrbital : AbstractAbill
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] private GameObject[] orbitals;
    [SerializeField] private ElectricOrbitalScriptableObject[] electricOrbitalScriptableObjects;
    [SerializeField] private Animator animator;
    private int numberOfActiveorbitals;
   
    [SerializeField] private float radius = 2f;
    private float speedRotation = 1;
    public static event Action ElectricOrbitalActionEvent;

    public float ElectricOrbitalDamage {  get; private set; }
    private void Start()
    {
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        ElectricOrbitalScriptableObject.ElectricOrbitalUpgrade += Reinitialize;
        Activate();
    }
    
    protected override void Initialize()
    {

        RadiusUpgrade();
        IsNeedToActive();
        IsNeedToChangeSpeedRotation();
        DamageUpgrage();
    }
   
    protected override void ActionOfAbill()
    {
       
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
       
    }
    protected override void RadiusUpgrade()
    {
        for (int i = 0; i < orbitals.Length; i++)
        {
            orbitals[i].transform.localScale = new Vector2
                (electricOrbitalScriptableObjects[abilityLevel].electricOrbitalRadius * statsHolder.Radius,
                electricOrbitalScriptableObjects[abilityLevel].electricOrbitalRadius * statsHolder.Radius);
        }
       

    }
    private void IsNeedToChangeSpeedRotation()
    {
        cooldownTime = math.INFINITY;
        if(speedRotation != electricOrbitalScriptableObjects[abilityLevel].electricOrbitalSpeed)
        {
            speedRotation = electricOrbitalScriptableObjects[abilityLevel].electricOrbitalSpeed;
            animator.SetFloat("Speed", speedRotation);
        }
        
    }
    private void IsNeedToActive()
    {
        if (numberOfActiveorbitals != electricOrbitalScriptableObjects[abilityLevel].electricOrbitalCount)
        {
            ReSetPosition();
        }
    }
    protected override void CountUpgrade()
    {
        numberOfActiveorbitals = electricOrbitalScriptableObjects[abilityLevel].electricOrbitalCount + bonusNumberOfCount;
        if (numberOfActiveorbitals > 7) { numberOfActiveorbitals = 7; }
        for (int i = 0; i <numberOfActiveorbitals; i++)
        {
            if (orbitals[i].activeInHierarchy == false) orbitals[i].SetActive(true);
        }
    }
    private void ReSetPosition()
    {
        CountUpgrade();
        
        for (int i = 0; i < numberOfActiveorbitals; i++)
        {
            // Вычисляем угол в радианах
            float angle = i * Mathf.PI * 2 / numberOfActiveorbitals;

            // Вычисляем позиции X и Y
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // Создаем объект на вычисленной позиции
            Vector2 position = new Vector2(playerPosition.position.x + x, playerPosition.position.y + y);
            orbitals[i].transform.position = position;
        }
    }


    protected override void DamageUpgrage()
    {
        ElectricOrbitalDamage = electricOrbitalScriptableObjects[abilityLevel].electricOrbitalDamage;
        ElectricOrbitalActionEvent?.Invoke();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        ElectricOrbitalScriptableObject.ElectricOrbitalUpgrade -= Reinitialize;
    }

    public override void CooldownReduction()
    {
       
    }



   

    protected override void DurationUpgrade()
    {
        
    }

    
}
