using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "FreezingField", menuName = "ScriptableObjects/Abill/FreezingField")]
public class FreezingFieldScriptableObject : BaseScriptableObject
{

    [SerializeField] public string freezingFieldName;
    [SerializeField] public float freezingFieldDamage;
    [SerializeField] public float freezingFieldRadius;
    [SerializeField] public float freezingFieldCooldown;
    public static event Action FreezingFieldUpgradeEvent;

    public void OnFreezingFieldUpgrade()
    {
        FreezingFieldUpgradeEvent?.Invoke();
    }
}
