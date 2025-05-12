using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "IceArrow", menuName = "ScriptableObjects/IceArrow")]
public class IceArrowScriptableObject : BaseScriptableObject
{
    public string iceArrowName;
    public float iceArrowDamage = 10;
    public float iceArrowSpeed = 10f;
    public float iceArrowFireRate = 1;
    public int iceArrowNumberOfShooters;
    public Color iceArrowColor;



    public static event Action IceArrowUpgrade;

    public void OnIceArrowUpgrade()
    {
        IceArrowUpgrade?.Invoke();
        Debug.Log("IO");
    }
}


