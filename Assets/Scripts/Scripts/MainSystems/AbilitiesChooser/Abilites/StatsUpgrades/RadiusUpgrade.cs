using System;

using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "RadiusStatsImprove", menuName = "ScriptableObjects/RadiusStat")]
 public class RadiusUpgrade : BaseScriptableObject
{
    public string radiusName;
    public float radiusImprove;

    public static event Action<float> RadiusUpgradeEvent;

    public void OnRadiusUpgrade()
    {
        RadiusUpgradeEvent?.Invoke(radiusImprove);
    }
}
