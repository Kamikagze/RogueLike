using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MagicShield", menuName = "ScriptableObjects/MagicShield")]
public class MagicShieldScriptableObject : BaseScriptableObject
{
    public string shieldName;
    public float reloadTime;
    public int numberOfShields;


    public static event Action ShieldUpgrade;

    public void OnShieldUpgrade()
    {
        ShieldUpgrade?.Invoke();
    }
}
