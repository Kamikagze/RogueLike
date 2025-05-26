using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "ArmorStatsImprove", menuName = "ScriptableObjects/ArmorStat")]
public class ArmorUpgrade : BaseScriptableObject
{ 
    
    public string armorName;
    public float armorImprove;
   


    public static event Action<float> ArmorUpgradeEvent;

    public void OnArmorUpgrade()
    {
        ArmorUpgradeEvent?.Invoke(armorImprove);
    }

}
