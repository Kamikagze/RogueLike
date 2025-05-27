using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ElectricOrbital", menuName = "ScriptableObjects/ElectricOrbital")]

public class ElectricOrbitalScriptableObject : BaseScriptableObject
{
    public string electricOrbitalName;
    public float electricOrbitalDamage = 3;
    public int electricOrbitalCount;

    public float electricOrbitalRadius;
    public float electricOrbitalSpeed;


    public static event Action ElectricOrbitalUpgrade;

    public void OnElectricOrbitalUpgrade()
    {
        ElectricOrbitalUpgrade?.Invoke();
        Debug.Log("IO");
    }
}
