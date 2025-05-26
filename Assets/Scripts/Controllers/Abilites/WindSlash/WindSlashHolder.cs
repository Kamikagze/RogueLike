using System;
using System.Collections;
using UnityEngine;

public class WindSlashHolder : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    
    [SerializeField] private WindSlashScriptableObject[] windSlashScriptableObjects;
    [SerializeField] private GameObject[] windSlashes;
    
    private int currentNumberOfWindSlashes;
    



    public float cooldown = 0;
   

    public float WindSlashDamage { get; private set; }

    public static event Action WindSlashDamageIncrease;
    private void Start()
    {
        
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        
        WindSlashScriptableObject.WindSlashUpgradeEvent += Reinitialize;
        
        Activate();


    }
   
  

    protected override void ActionOfAbill()
    {
        
        if (!isInWork)
        {
            for (int i = 0; i < currentNumberOfWindSlashes; i++)
            {
                windSlashes[i].gameObject.SetActive(true);

            }
        }  
        
    }


   
  

    protected override void CountUpgrade()
    {
        currentNumberOfWindSlashes = windSlashScriptableObjects[abilityLevel].windSlashNumber + bonusNumberOfCount;
    }
    protected override void DamageUpgrage()
    {
       
        WindSlashDamage = windSlashScriptableObjects[abilityLevel].windSlashDamage;
        WindSlashDamageIncrease?.Invoke();
    }
    public override void CooldownReduction() 
    {
       cooldown = windSlashScriptableObjects[abilityLevel].windSlashCooldown * statsHolder.CooldownReduction * cooldownMultiplicator * bonusCooldown;
        ChangeCooldown(cooldown);
    }

    protected override void RadiusUpgrade()
    {
        for (int i = 0; i < windSlashes.Length; i++)
        {
            windSlashes[i].transform.localScale = new Vector2(windSlashScriptableObjects[abilityLevel].windSlashRadius * statsHolder.Radius * bonusRadius,
           windSlashScriptableObjects[abilityLevel].windSlashRadius * statsHolder.Radius * bonusRadius);
        }
    
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.RadiusIncreased -= RadiusUpgrade;        
        WindSlashScriptableObject.WindSlashUpgradeEvent -= Reinitialize;
    }

   

    protected override void DurationUpgrade()
    {

    }

    
}
   


