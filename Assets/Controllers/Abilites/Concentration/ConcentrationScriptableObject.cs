using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Concentration", menuName = "ScriptableObjects/Concentration")]
public class ConcentrationScriptableObject : BaseScriptableObject
{
    [SerializeField] public string concentrationName;
    [SerializeField] public float concentrationCooldown;
    [SerializeField] public float concentrationDuration;
    [SerializeField] public float concentrationPercent;

   

    public static event Action concentrationUpgradeEvent;


    public void OnConcentrationUpgrade()
    {
        concentrationUpgradeEvent?.Invoke();
    }
}
