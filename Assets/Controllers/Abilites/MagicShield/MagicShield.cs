using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MagicShield : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] HealthSystem healthSystem;
    
    [SerializeField] private MagicShieldScriptableObject[] magicShieldScriptableObjects;
    
    [SerializeField] SpriteRenderer shieldColor;

    private int maxNumberOfShields;
    private float reloadTime;
    private float alphaValue = 0.3f;

    public int ActiveShields { get; private set; }




    private void Start()
    {

        MagicShieldScriptableObject.ShieldUpgrade += Reinitialize;
        
        Activate();
    }
       

    

    protected override void ActionOfAbill()
    {
        if (ActiveShields < maxNumberOfShields)
        {
            Duration = 0;
            ActiveShields += 1;
            ShieldColor();
        }
        else Duration = 3;
    }
    public void ShieldBreaker()
    {
        ActiveShields -= 1;
        ShieldColor();
    }
    
    private void ShieldColor()
    {
        switch(ActiveShields)
        {
            case 0: shieldColor.enabled = false; break;
            case 1: shieldColor.enabled = true; shieldColor.color = new Color(0,1,0,alphaValue); 
                break;
            case 2:
                shieldColor.enabled = true;
                shieldColor.color = new Color(1, 1, 0, alphaValue);
                break;
            case 3:
                shieldColor.enabled = true; 
                shieldColor.color = new Color(0, 0, 1, alphaValue);
                break;

        }    
    }
    protected override void Initialize()
    
    {
        maxNumberOfShields = magicShieldScriptableObjects[abilityLevel].numberOfShields;
        
        ShieldColor();
        CooldownReduction();
    }

    public override void CooldownReduction()
    {
        reloadTime = magicShieldScriptableObjects[abilityLevel].reloadTime * statsHolder.CooldownReduction 
            * cooldownMultiplicator * bonusCooldown;
        ChangeCooldown(reloadTime);
    }
   

    protected override void OnDisable()
    {
        base.OnDisable();
        MagicShieldScriptableObject.ShieldUpgrade -= Reinitialize;
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

