using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "LavaField", menuName = "ScriptableObjects/Abill/LavaField")]
public class LavaFieldScriptableObject : BaseScriptableObject
{
    [SerializeField] public string lavaFieldName;
    [SerializeField] public float lavaFieldDamage;
    [SerializeField] public float lavaFieldRadius;
    [SerializeField] public float lavaFieldCooldown;
    [SerializeField] public float lavaFieldDuration;
    [SerializeField] public int lavaFieldCount;


    public static event Action LavaFieldUpgradeEvent;

    public void OnLavaFieldUpgrade()
    {
        LavaFieldUpgradeEvent?.Invoke();
    }
}
