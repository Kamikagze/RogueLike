using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireBreath", menuName = "ScriptableObjects/FireBreath")]
public class FireBreathScriptableObject : BaseScriptableObject
{
    public string fireBreathName;
    public float fireBreathDamage;
    public float fireBreathRadius;
    public float fireBreathCooldown;
    public float fireBreathDuration;

    public static event Action FireBreathUpgrade;

    public void OnFireBreathUpgrade()
    {
        FireBreathUpgrade?.Invoke();
    }
}
