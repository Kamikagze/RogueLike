using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SpeedStatsImprove", menuName = "ScriptableObjects/SpeedStat")]


public class SpeedUpgrade : BaseScriptableObject
{
    public string speedName;
    public float speedImprove;


    public static event Action<float> SpeedUpgradeEvent;

    public void OnSpeedUpgrade()
    {
        SpeedUpgradeEvent?.Invoke(speedImprove);
    }

}
