using System;
using System.Collections;
using UnityEngine;

public class LightningOrb : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] Lightnings[] lights;
    [SerializeField] private LightningOrbScriptableObjects[] lightningOrbScriptableObjects;
   
    private int activeLightnings;
    

    public float lightningOrbCooldown;


    private System.Random random = new System.Random(); // Ёкземпл€р System.Random дл€ случаев использовани€
    public float LightningOrbDamage { get; private set; } // Ќадо подумать на счет свойств или избавлени€ от этого метода

    public static event Action LightningOrbActionEvent;
   
    private void Start()
    {

        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        Duration = 0.5f;
        LightningOrbScriptableObjects.LightningOrbUpgrade += Reinitialize;
        
        Activate();
    }

    protected override void ActionOfAbill()
    {
        for (int i = 0; i < activeLightnings; i++)
        {
            lights[i].Activator(random.Next(0, 360));
              
        }
    }
    protected override void Offers()
    {
        for (int i = 0; i < activeLightnings ; i++)
        {
            lights[i].Offer();

        }
    }
   
    public override void CooldownReduction()
    {
        lightningOrbCooldown = lightningOrbScriptableObjects[abilityLevel].lightningOrbCooldown
            * statsHolder.CooldownReduction;
        ChangeCooldown(lightningOrbCooldown);
    }
   
   
    protected override void DamageUpgrage()
    {
        LightningOrbDamage = lightningOrbScriptableObjects[abilityLevel].lightningOrbDamage;
        LightningOrbActionEvent?.Invoke();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;

        LightningOrbScriptableObjects.LightningOrbUpgrade -= Reinitialize;
    }

    

    protected override void RadiusUpgrade()
    {
       
    }

    protected override void DurationUpgrade()
    {
       
    }

    protected override void CountUpgrade()
    {
        activeLightnings = lightningOrbScriptableObjects[abilityLevel].lightningOrbCount + bonusNumberOfCount;

    }
}
