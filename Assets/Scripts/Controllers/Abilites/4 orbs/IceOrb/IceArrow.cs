using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrow : MonoBehaviour
{
    
    public IceArrowPool pool;

    public IceArrowScriptableObject[] levelsIseArrow;
    public int arrowLevel = 0;
    private float lifetime = 0.8f;
    private int countOfDamages = 2;

    //EpicUpgrage
    [SerializeField] SpriteRenderer spriteRenderer;
    
    private bool isFrostHell;

    private void Start()
    {
        FullFillButtons.UpgradeIceArrow += LevelUp;
    }

    public void Initialize(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * levelsIseArrow[arrowLevel].iceArrowSpeed;    
        StartCoroutine(StartLifetimeCoroutine());
       
        
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            countOfDamages--;
            if (countOfDamages <= 0) {ReturnToPool(); }
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

    public void SetPool(IceArrowPool iceArrowPool)
    {
        pool = iceArrowPool; // Устанавливаем пул для пули

    }
    

    
    public void LevelUp(int level)
    {
        if (arrowLevel < levelsIseArrow.Length - 1) // Проверяем, не в максимальном ли уровне
        {
            arrowLevel = level;
            Checker();
            Debug.Log($"{levelsIseArrow[arrowLevel].iceArrowName} улучшена до уровня {arrowLevel + 1}");
        }
        
        else
        {
            Debug.LogWarning("Максимальный уровень пули достигнут!");
        }
    }
    private IEnumerator StartLifetimeCoroutine()
    {
        if (!isFrostHell)
        {
            countOfDamages = 2;
        }
        else if (isFrostHell)
        {
            countOfDamages = 4;
        }
        // Ждём заданное время жизни пули
        yield return new WaitForSeconds(lifetime);

        // Проверяем, активна ли пуля, и если да, возвращаем её в пул
        if (gameObject.activeSelf)
        {
            ReturnToPool();
        }
    }
    public void EpicUpgrade()
    {
        isFrostHell = true;
        
        spriteRenderer.color = Color.red;
       
        
    }
    private void Checker()
    {
        if (pool.iceHell == true || arrowLevel == 8)
        {
            EpicUpgrade();
        }
    }
    private void OnDisable()
    {
        FullFillButtons.UpgradeIceArrow -= LevelUp;
    }
}
