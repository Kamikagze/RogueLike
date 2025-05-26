using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterOfFB : MonoBehaviour
{

    public FireBallPool fBPool; // Ссылка на пул пуль
    [SerializeField] private Transform player;




    private void Start()
    {
       
    }


    public void ShootMethod()
    {
        // Получаем случайную позицию на окружности
        Vector2 targetPosition = GetRandomPositionOnCircle(1f);

        // Находим направление к этой целевой позиции, вычитая позицию игрока
        Vector2 direction = targetPosition - (Vector2)transform.position;

        // Получаем пулю из пула
        FireBall fireball = fBPool.GetFireBall();

        if (fireball != null)
        {
            // Устанавливаем позицию пули
            fireball.transform.position = transform.position;

            // Поворачиваем огненный шар в сторону направления
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Преобразуем в радианы в градусы
            fireball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Инициализируем огненный шар
            fireball.Initialize(direction);
        }
        else
        {
            Debug.LogWarning("Не удалось получить пулю из пула");
        }
    }


    private Vector2 GetRandomPositionOnCircle(float radius)
    {
        // Генерируем случайный угол в радианах
        float angle = Random.Range(0f, 2 * Mathf.PI);

        // Вычисляем координаты точки на окружности относительно центра объекта
        float x = player.position.x + radius * Mathf.Cos(angle);
        float y = player.position.y + radius * Mathf.Sin(angle);

        // Возвращаем случайную точку на окружности как Vector2
        return new Vector2(x, y);
    }



    private void OnDisable()
    {
        
    }
}
