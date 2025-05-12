using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "CooldownStatsImprove", menuName = "ScriptableObjects/CooldownStat")]
public class CooldownUpgrade : BaseScriptableObject
{
    public string cooldownName;
    public float cooldownImprove;



    public static event Action<float> CooldownUpgradeEvent;

    public void OnCooldownUpgrade()
    {
        CooldownUpgradeEvent?.Invoke(cooldownImprove);
    }
}
