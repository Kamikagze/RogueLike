using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Cataclysm : AbstractAbill
{
    [SerializeField] Animator cataclysmAnim;
    [SerializeField] CataclysmScriptableObject[] cataclysms;
    [SerializeField] GameObject specialEffects;
    [SerializeField] StatsHolder statsHolder;
    private float cataclysmCooldown;
    public int cataclysmLevel = -1;

    private void Start()
    {
        CataclysmScriptableObject.CataclysmUpgradeEvent += Reinitialize;

  
        Activate();
    }
    

   
    protected override void ActionOfAbill()
    {
        Offer();
        if (!isInWork)
        {
            cataclysmAnim.SetTrigger("Action");
            specialEffects.SetActive(true);
        }
       
    }
    
       

    private void Offer()
    {
        cataclysmAnim.ResetTrigger("Action");
        
    }

    public override void CooldownReduction()
    {
        cataclysmCooldown = cataclysms[cataclysmLevel].cataclysmCooldown * statsHolder.CooldownReduction * cooldownMultiplicator * bonusCooldown;
        ChangeCooldown(cataclysmCooldown);
    }

    protected override void Initialize()
    
    {
        CooldownReduction();
    }
    protected override void Reinitialize()
   
    {
        cataclysmLevel++;
        Initialize();
    }



    protected override void OnDisable()
    {
        base.OnDisable();
        CataclysmScriptableObject.CataclysmUpgradeEvent -= Reinitialize;
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

