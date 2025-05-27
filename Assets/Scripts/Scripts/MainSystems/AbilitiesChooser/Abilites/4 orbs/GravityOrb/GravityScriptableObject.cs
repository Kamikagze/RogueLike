using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GravityOrb", menuName = "ScriptableObjects/GravityOrb")]
public class GravityOrbScriptableObjects : BaseScriptableObject
{
    public string gravityOrbName;
    public float gravityOrbDamage = 10;
    public int gravityOrbCount = 0;
    public float gravityOrbCooldown = 3;
    public float gravityOrbRadius;
   
    public static event Action GravityOrbUpgrade;

    public void OnGravityOrbUpgrade()
    {
        GravityOrbUpgrade?.Invoke();
        Debug.Log("IO");
    }
}
