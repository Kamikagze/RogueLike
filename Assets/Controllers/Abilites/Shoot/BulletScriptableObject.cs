using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Bullet")]

public class BulletScriptableObject : BaseScriptableObject
{
    
    public string bulletName; 
    public float bulletDamage = 10;
    public float bulletSpeed = 10f;
    public float bulletFireRate = 1;
    public bool doubleShot;
    public bool tripleShot;
    public Color  bulletColor ;
 


    public static event Action BulletUpgrade;

    public void OnBulletUpgrade()
    {
        BulletUpgrade?.Invoke();
        Debug.Log("IO");
    }

}

