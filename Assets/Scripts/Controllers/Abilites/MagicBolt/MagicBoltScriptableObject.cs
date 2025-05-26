using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "MagicBolt", menuName = "ScriptableObjects/MagicBolt")]
public class MagicBoltScriptableObject : BaseScriptableObject
{
    public string magicBoltName;
    public float magicBoltDamage = 10;
   
    public float magicBoltFireRate = 7;
    public int magicBoltNumberOfLightnings;
    public static event Action MagicBoltUpgrade;

    public void OnMagicBoltUpgrade()
    {
        MagicBoltUpgrade?.Invoke();
        Debug.Log("IO");
    }
}
