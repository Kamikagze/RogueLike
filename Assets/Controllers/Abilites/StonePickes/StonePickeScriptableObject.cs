using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "StonePicke", menuName = "ScriptableObjects/StonePicke")]
public class StonePickeScriptableObject : BaseScriptableObject
{
   
        public string stonePickeName;
        public float stonePickeDamage = 10;
        public float stonePickeDuration = 10f;
        public float stonePickeFireRate = 1;
        public float stonePickeRadius;
        public float stonePikeCooldown;
        public static event Action StonePickeUpgrade;

        public void OnStonePickeUpgrade()
        {
            StonePickeUpgrade?.Invoke();
            Debug.Log("IO");
        }
    }


