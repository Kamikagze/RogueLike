using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cataclysm", menuName = "ScriptableObjects/Cataclysm")]
public class CataclysmScriptableObject : BaseScriptableObject
{
    [SerializeField] public string cataclysmName;
    [SerializeField] public float cataclysmCooldown;
    
    
   
    public static event Action CataclysmUpgradeEvent;
    

    public void OnCataclysmUpgrade()
    {
        CataclysmUpgradeEvent?.Invoke();
    }
}
