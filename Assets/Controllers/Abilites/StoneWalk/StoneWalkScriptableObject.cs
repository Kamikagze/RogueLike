using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "StoneWalk", menuName = "ScriptableObjects/Abill/StoneWalk")]
public class StoneWalkScriptableObject : BaseScriptableObject
{

    [SerializeField] public string stoneWalkName;
    [SerializeField] public float stoneWalkDamage;
    [SerializeField] public float stoneWalkRadius;
    [SerializeField] public float stoneWalkCooldown;
    [SerializeField] public int stoneWalkCount;
   
    public static event Action StoneWalkUpgradeEvent;

    public void OnStoneWalkUpgrade()
    {
        StoneWalkUpgradeEvent?.Invoke();
    }
}
