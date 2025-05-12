using System;
using System.Collections;

using UnityEngine;

public class Shoot : MonoBehaviour
{

        public BulletPool bulletPool; // Ссылка на пул пуль
        public FounderOfEnemies enemies; // Враг или позиция, куда стреляем
       



    private void Start()
    {
        FullFillButtons.IceHell += DestroyThis;

    }
   

    public void ShootMethod()
    {
        if (enemies != null)
        {
            var nearestEnemy = enemies.FindNearestEnemy();

            // Проверяем, существует ли ближайший враг
            if (nearestEnemy != null)
            {
                // Находим направление к ближайшему врагу
                Vector2 direction = (nearestEnemy.position - transform.position).normalized;

                // Получаем пулю из пула
                Bullet bullet = bulletPool.GetBullet();

                if (bullet != null)
                {
                    // Устанавливаем позицию пули и инициализируем её
                    bullet.transform.position = transform.position;
                    bullet.Initialize(direction);
                }
                else
                {
                    Debug.LogWarning("Не удалось получить пулю из пула");
                }
            }
            else
            {
               // Debug.LogWarning("Нет доступных врагов для стрельбы");
            }
        }
    }


   

    private void DestroyThis()
    {
        StopAllCoroutines();
       gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        FullFillButtons.IceHell -= DestroyThis;
    }
}
