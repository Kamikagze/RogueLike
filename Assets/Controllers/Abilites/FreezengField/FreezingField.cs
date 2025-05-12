using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreezingField : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] private CircleCollider2D freezingFieldCollider;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform freezingFieldRadius;
    [SerializeField] private Transform player;
    [SerializeField] private FreezingFieldScriptableObject[] freezingFieldScriptableObjects;
    [SerializeField] private SpriteRenderer freezingFieldSpriteRenderer;


    
    public float freezingFieldCooldown;

    protected override float Duration { get => 1f; }

    public float FreezingFieldDamage { get; private set; } // Надо подумать на счет свойств или избавления от этого метода

    public static event Action FreezingFieldActionEvent;
    private void Start()
    {
       
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        FreezingFieldScriptableObject.FreezingFieldUpgradeEvent += Reinitialize;
       
        Activate();
    }


  
    protected override void ActionOfAbill()
    {
        transform.position = player.position;
        anim.SetTrigger("Start");
        
    }
    protected override void Offers()
    {
        anim.ResetTrigger("Start");
    }
  
  
    public override void CooldownReduction()
    {
    
    
        freezingFieldCooldown = freezingFieldScriptableObjects[abilityLevel].freezingFieldCooldown
            * statsHolder.CooldownReduction * cooldownMultiplicator;
        ChangeCooldown(freezingFieldCooldown);
    }
    protected override void RadiusUpgrade()
    {

        freezingFieldRadius.localScale = new Vector2(freezingFieldScriptableObjects[abilityLevel].freezingFieldRadius
            * statsHolder.Radius * bonusRadius, freezingFieldScriptableObjects[abilityLevel].freezingFieldRadius
            * statsHolder.Radius * bonusRadius);
        
    }


    protected override void DamageUpgrage()
    {
        FreezingFieldDamage = freezingFieldScriptableObjects[abilityLevel].freezingFieldDamage * bonusDamage;
        FreezingFieldActionEvent?.Invoke();
    }
   
    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        FreezingFieldScriptableObject.FreezingFieldUpgradeEvent -= Reinitialize;
    }


    protected override void DurationUpgrade()
    {
   
    }

    protected override void CountUpgrade()
    {

    }
}
