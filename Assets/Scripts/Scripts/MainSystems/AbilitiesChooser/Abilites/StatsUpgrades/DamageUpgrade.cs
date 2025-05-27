using System;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "DamageStatsImprove", menuName = "ScriptableObjects/DamageStat")]
public class DamageUpgrade : BaseScriptableObject
{    
    public string damageName;
    public float damageImprove;
   


    public static event Action<float> DamageUpgradeEvent;

    public void OnDamageUpgrade()
    {
        DamageUpgradeEvent?.Invoke(damageImprove);
    }
}
