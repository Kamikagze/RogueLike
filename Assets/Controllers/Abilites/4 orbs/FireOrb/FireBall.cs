using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public FireBallPool pool;

    public FireBallScriptableObject[] levelsIseFireBall;
    public int fireBallLevel = 0;
    private float lifetime = 2f;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    



    public void Initialize(Vector2 direction)
    {

        Vector2 normalizedDirection = direction.normalized;

        // Установка скорости пули
        rb.velocity = normalizedDirection * 2F;

        // Запуск корутины для контроля времени жизни пули
        StartCoroutine(StartLifetimeCoroutine());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Explode");
            rb.velocity = Vector2.zero;
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

    public void SetPool(FireBallPool fireBallPool)
    {
        pool = fireBallPool; // Устанавливаем пул для пули

    }



    public void LevelUp(int level)
    {
        if (fireBallLevel < levelsIseFireBall.Length - 1) // Проверяем, не в максимальном ли уровне
        {
            fireBallLevel = level;
            

            Debug.Log($"{levelsIseFireBall[fireBallLevel].fireBallName} улучшена до уровня {fireBallLevel + 1}");
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
            animator.SetTrigger("Explode");
        }
    }
}
