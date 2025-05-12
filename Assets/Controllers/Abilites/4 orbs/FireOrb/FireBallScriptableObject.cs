using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "FireBall", menuName = "ScriptableObjects/FireBall")]

public class FireBallScriptableObject : BaseScriptableObject
{
    public string fireBallName;
    public float fireBallDamage = 10;

    public float fireBallFireRate = 3;
    public float fireBallRadius = 1;

  
  
    public static event Action FireBallUpgrade;

    public void OnFireBallUpgrade()
    {
        FireBallUpgrade?.Invoke();
        Debug.Log("IO");
    }
}
