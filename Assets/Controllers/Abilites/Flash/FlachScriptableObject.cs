using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FlashAbill", menuName = "ScriptableObjects/Abill/Flash")]
public class FlashScriptableObject : BaseScriptableObject
{
    [SerializeField] public string flashName;
    [SerializeField] public float flashDamage;
    [SerializeField] public float flashRadius;
    [SerializeField] public float flashCooldown;
 
    public static event Action FlashUpgradeEvent;

    public void OnFlashUpgrade()
    {
        FlashUpgradeEvent?.Invoke();
    }

}
