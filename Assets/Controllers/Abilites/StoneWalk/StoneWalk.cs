using System;
using UnityEngine;

public class StoneWalk : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] private Transform stoneWalkCollider;
    [SerializeField] private StoneWalkScriptableObject[] stoneWalkScriptableObjects;
  
    [SerializeField] private Animator animator;


    public int stoneWalkCount = 2;
    public float stoneWalkCooldown;
    private float stoneWalkCooldownBetweenActions = 1f;

    public float StoneWalkDamage { get; private set; } // Надо подумать на счет свойств или избавления от этого метода

    private float currentTime;

    public static event Action StoneWalkActionEvent;
    private void Start()
    {
        
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        StoneWalkScriptableObject.StoneWalkUpgradeEvent += Reinitialize;
       
        Activate();
        FullFillButtons.Eruption += EpicUpgrade;
    }


   
    protected override void ActionOfAbill()
    {
        currentTime = 0;
        animator.ResetTrigger("Action");
        animator.SetTrigger("Action");
       
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime >= 1f)
        {
            animator.ResetTrigger("Action");
            animator.SetTrigger("Action");
            currentTime = 0;
        }
    }
   
    public override void CooldownReduction()
    {
        stoneWalkCooldown = stoneWalkScriptableObjects[abilityLevel].stoneWalkCooldown
            * statsHolder.CooldownReduction * cooldownMultiplicator;
        ChangeCooldown(stoneWalkCooldown);

    }

    protected override void RadiusUpgrade()
    {
        stoneWalkCollider.transform.localScale = new Vector2( stoneWalkScriptableObjects[abilityLevel].stoneWalkRadius
            * statsHolder.Radius * bonusRadius, stoneWalkScriptableObjects[abilityLevel].stoneWalkRadius
            * statsHolder.Radius * bonusRadius) ;
    }
    protected override void CountUpgrade()
    {
        stoneWalkCount = stoneWalkScriptableObjects[abilityLevel].stoneWalkCount + bonusNumberOfCount;
        Duration = stoneWalkCooldownBetweenActions * (stoneWalkCount - 1) + 0.1f;
    }

    protected override void DamageUpgrage()
    {
        StoneWalkDamage = stoneWalkScriptableObjects[abilityLevel].stoneWalkDamage * bonusDamage; 
        StoneWalkActionEvent?.Invoke();
    }

    private void EpicUpgrade()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        StoneWalkScriptableObject.StoneWalkUpgradeEvent -= Reinitialize;
        FullFillButtons.Eruption -= EpicUpgrade;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        StoneWalkScriptableObject.StoneWalkUpgradeEvent -= Reinitialize;        
        FullFillButtons.Eruption -= StopAllCoroutines;
        FullFillButtons.Eruption -= EpicUpgrade;
    }



    protected override void DurationUpgrade()
    {

    }

   
}
