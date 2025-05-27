using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy")]

public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] public float speed;
    [SerializeField] public float speedMultiplier;
    [SerializeField] public float damage;
    public float damageMultiplier;
    [SerializeField] public float Health;
    [SerializeField] public float HealthMultiplier;
    [SerializeField] public float howManyEXPHold;



   
    private void Upgrade()
        {
            Debug.Log(" улучшился!");
            Health += HealthMultiplier; // Увеличиваем здоровье
            damage += 5; // Увеличиваем урон
        }
}
