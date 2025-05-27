using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "WindSlash", menuName = "ScriptableObjects/WindSlash")]
public class WindSlashScriptableObject : BaseScriptableObject
{
    [SerializeField] public string windSlashName;
    [SerializeField] public float windSlashCooldown;
    [SerializeField] public int windSlashNumber;
    [SerializeField] public float windSlashDamage;
    [SerializeField] public float windSlashRadius;


    public static event Action WindSlashUpgradeEvent;


    public void OnWindSlashUpgrade()
    {
        WindSlashUpgradeEvent?.Invoke();
    }
}
