using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "EXPStatsImprove", menuName = "ScriptableObjects/EXPStat")]
public class EXPUpgrade : BaseScriptableObject
{
    public string expName;
    public float expImprove;
  


    public static event Action<float> EXPUpgradeEvent;

    public void OnEXPUpgrade()
    {
        EXPUpgradeEvent?.Invoke(expImprove);
    }
}
