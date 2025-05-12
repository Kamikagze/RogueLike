using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCrafter : MonoBehaviour, ITimeController
{
    [SerializeField] private GameObject[] Chests;
    [SerializeField] private Transform player;
    private float minRadius = 9f;
    private float maxRadius = 20f;

    private float timeSpawn = 120f;
    private float currentTime = 0;


    public void Timerred(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime / timeSpawn >= 1)
        {
            CraftChest();
            currentTime = 0;
        }
    }

    

    private void CraftChest()
    {
        float distance = UnityEngine.Random.Range(minRadius, maxRadius);
        Vector2 position = GetPositionOnCircle(distance);
        Instantiate(Chests[0], position, Quaternion.identity);

     

    }
    private Vector2 GetPositionOnCircle(float distance)
    {
        // Генерируем случайный угол в радианах от 0 до 2π
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        // Вычисляем координаты на окружности
        float x = player.position.x + Mathf.Cos(angle) * distance; // x-координата
        float y = player.position.y + Mathf.Sin(angle) * distance; // y-координата
        return new Vector2(x, y); // Возвращаем позицию 
    }
}
