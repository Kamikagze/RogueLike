using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Laser", menuName = "ScriptableObjects/Laser")]

public class LaserScriptableObject : BaseScriptableObject
{
    public string laserName;
    public float laserDamage = 15;
    public float laserDuration;
    public float laserCooldown = 3;
    public int laserCount;

    public static event Action LaserUpgrade;

    public void OnLaserUpgrade()
    {
        LaserUpgrade?.Invoke();
        Debug.Log("IO");
    }
}
