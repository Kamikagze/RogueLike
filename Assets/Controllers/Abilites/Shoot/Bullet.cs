using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public StatsHolder globalStats;
    public BulletPool pool;

    public BulletScriptableObject[] levelsOfBullet;
    public int bulletLevel = 0;
    private float lifetime = 4;


    

    public void Initialize(Vector2 direction)
    {
       
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * levelsOfBullet[bulletLevel].bulletSpeed;

        // Запуск корутины для контроля времени жизни пули
        StartCoroutine(StartLifetimeCoroutine());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
         // Проверяем вызов метода
        if (pool == null)
        {
            Debug.LogError("Bullet pool is null! Make sure SetPool is called.");
        }
        gameObject.SetActive(false); // Деактивируем объект
        pool.ReturnObject(this); // Возвращаем объект в пул
    }

    public void SetPool(BulletPool bulletPool)
    {
        pool = bulletPool; // Устанавливаем пул для пули

    }
    public void SetGlobalStats(StatsHolder _globalStats)
    {
        globalStats = _globalStats; // Устанавливаем stats

    }

   
    public void LevelUp(int level)
    {
        if (bulletLevel < levelsOfBullet.Length - 1) // Проверяем, не в максимальном ли уровне
        {
            bulletLevel = level;

           // Debug.Log($"{levelsOfBullet[bulletLevel].bulletName} улучшена до уровня {bulletLevel + 1}");
        }
        else
        {
            Debug.LogWarning("Максимальный уровень пули достигнут!");
        }
    }
    private IEnumerator StartLifetimeCoroutine()
    {
        // Ждём заданное время жизни пули
        yield return new WaitForSeconds(lifetime);

        // Проверяем, активна ли пуля, и если да, возвращаем её в пул
        if (gameObject.activeSelf)
        {
            ReturnToPool();
        }
    }
}
