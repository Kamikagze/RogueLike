using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "WanderingFlash", menuName = "ScriptableObjects/Abill/WanderingFlash")]
public class WanderingFlashScriptableObject : BaseScriptableObject 
{

    [SerializeField] public string wanderingFlashName;
    [SerializeField] public float wanderingFlashDamage;
  
    [SerializeField] public float wanderingFlashCooldown;
    [SerializeField] public int wanderingFlashCount;
  
    public static event Action WanderingFlashUpgradeEvent;

    public void OnWanderingFlashUpgrade()
    {
        WanderingFlashUpgradeEvent?.Invoke();
    }
}