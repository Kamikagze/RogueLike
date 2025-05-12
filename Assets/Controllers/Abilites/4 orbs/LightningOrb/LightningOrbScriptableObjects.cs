using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[CreateAssetMenu(fileName = "LightningOrb", menuName = "ScriptableObjects/LightningOrb")]
public class LightningOrbScriptableObjects : BaseScriptableObject
{
    public string lightningOrbName;
    public float lightningOrbDamage = 10;
    public int lightningOrbCount = 0;
    public float lightningOrbCooldown = 3;
    


    public static event Action LightningOrbUpgrade;

    public void OnLightningOrbUpgrade()
    {
        LightningOrbUpgrade?.Invoke();
        Debug.Log("IO");
    }
}
