using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FounderOfEnemies : MonoBehaviour
{
 
    
    [SerializeField] private Transform player; // игрок для определения ближайшего врага

    private HashSet<Transform> detectedEnemies = new HashSet<Transform>();

    private int counterOfUpdates;

    private void OnEnable()
    {
        AbstractEnemy.Remove += GetOutOfCollection;
    }
    private void OnDisable()
    {
        AbstractEnemy.Remove -= GetOutOfCollection;
    }
    private void FixedUpdate()
    {
        UpdateEnemiesPosition();
    }
    private void UpdateEnemiesPosition()
    {
        foreach (var enemyTransform in detectedEnemies)
        {
            if (enemyTransform != null)
            {
                Vector3 enemyPosition = enemyTransform.position;
                if (counterOfUpdates >= 300)
                {
                    DeleteFurthestEnemies(enemyTransform);
                    
                }
            }
        }
        if (counterOfUpdates >= 300)
        {
            counterOfUpdates = 0;
        }
        else { counterOfUpdates++; }
    }

    // Возвращает список обнаруженных врагов
    public HashSet<Transform> GetDetectedEnemies()
    {
        return detectedEnemies; // Возвращаем существующий набор врагов
    }

    // Нахождение ближайшего врага к заданной позиции
    public Transform FindNearestEnemy(Transform referencePosition)
    {
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var enemy in detectedEnemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(referencePosition.position, enemy.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }

    // Перегрузка метода для поиска ближайшего врага от игрока
    public Transform FindNearestEnemy()
    {
        return FindNearestEnemy(player);
    }
    public void GetNewEnemyInCollection(Transform enemy)
    {
        detectedEnemies.Add(enemy);
    }
    public void GetOutOfCollection(Transform enemy)
    {
        detectedEnemies.Remove(enemy);
    }
    public void DeleteFurthestEnemies(Transform enemy)
    {
        float distanse = Vector2.Distance(player.position, enemy.position);
        if (distanse >= 10f)
        {
           Destroy(enemy.gameObject);
        }
    }
}

