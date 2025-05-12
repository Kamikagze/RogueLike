using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Meteor", menuName = "ScriptableObjects/Abill/Meteor")]
public class MeteorScriptableObject : BaseScriptableObject
{
    [SerializeField] public string meteorName;
    [SerializeField] public float meteorDamage;
    [SerializeField] public float meteorRadius;
    [SerializeField] public float meteorCooldown;
    [SerializeField] public int meteorCount;

    public static event Action MeteorUpgradeEvent;

    public void OnMeteorUpgrade()
    {
        MeteorUpgradeEvent?.Invoke();
    }
}
