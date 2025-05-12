using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Concentration : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;

    [SerializeField] private ConcentrationScriptableObject[] concentrationScriptableObjects;


    private float percentOFReduction;

    public float waitTime;  // Время ожидания перед следующим перемещением

    [SerializeField] private Animator mainAnimator; // вращение

    public static event Action<float> ConcentrationActionEvent;
    private void Start()
    {

        ConcentrationScriptableObject.concentrationUpgradeEvent += Reinitialize;

        Activate();
    }


    protected override void ActionOfAbill()
    {
        ConcentrationActionEvent?.Invoke(percentOFReduction);
        mainAnimator.SetBool("Work", true);
        Debug.Log("Concent");
    }
   

    protected override void Offers()
    {
        ConcentrationActionEvent?.Invoke(0f);
        mainAnimator.SetBool("Work", false);
    }

    protected override void Initialize()
    {
        base.Initialize();
        PercentReduction();
    }

    public override void CooldownReduction()
    {
        waitTime = concentrationScriptableObjects[abilityLevel].concentrationCooldown * statsHolder.CooldownReduction * cooldownMultiplicator;
        ChangeCooldown(waitTime);
    }

    private void PercentReduction()
    {
        percentOFReduction = concentrationScriptableObjects[abilityLevel].concentrationPercent;
    }
    
  
    protected override void OnDisable()
    {
        base.OnDisable();

        ConcentrationScriptableObject.concentrationUpgradeEvent -= Reinitialize;
    }

    protected override void DamageUpgrage()
    {
  
    }

    protected override void RadiusUpgrade()
    {

    }

    protected override void DurationUpgrade()
    {
        Duration = concentrationScriptableObjects[abilityLevel].concentrationDuration * bonusDuration ; // добавляю время прогрева лазера

    }

    protected override void CountUpgrade()
    {
      
    }
}
