using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterOfSP : MonoBehaviour
{
    [SerializeField] private Transform player;

    public StonePickePool stonePickePool; // Ссылка на пул пуль


    // Задержка между выстрелами


    private void Start()
    {

    }
    private void OnEnable()
    {

    }


    public void ShootMethod()
    {

        StonePicke stonePicke = stonePickePool.GetStonePicke();

        if (stonePicke != null)
        {
            stonePicke.Initialize(GetRandomPositionInsideCircle());
        }
          

        
        
    }

  

   private Vector2 GetRandomPositionInsideCircle()
    {
       
            // Генерация случайного угла от 0 до 2π
            float angle = Random.Range(0f, 2f * Mathf.PI);

            // Генерация случайного радиуса
            float randomRadius = Mathf.Sqrt(Random.Range(0f, 1f)) * stonePickePool.RadiusOfSpawn;

            // Вычисление координат x и y
            float x = randomRadius * Mathf.Cos(angle);
            float y = randomRadius * Mathf.Sin(angle);

            return new Vector2(player.position.x + x, player.position.y + y);
        
    }

    
    private void OnDisable()
    {
        
    }

}
