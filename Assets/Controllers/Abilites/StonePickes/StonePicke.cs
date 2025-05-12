using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePicke : MonoBehaviour
{
    public StonePickePool pool;

    public StonePickeScriptableObject[] levelsIseStonePicke;
    public int stonePickeLevel = 0;
    private float lifetime = 0.3f;

    [Header("EarthShake")] 
    [SerializeField] private GameObject krater;
    [SerializeField] private Animator kraterAnim;
    




    public void Initialize(Vector2 direction)
    {
        
        transform.position = direction;
        // Запуск корутины для контроля времени жизни шипа
        StartCoroutine(StartLifetimeCoroutine());
       
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

    public void SetPool(StonePickePool stonePickePool)
    {
        pool = stonePickePool; // Устанавливаем пул для пули

    }



    public void LevelUp(int level)
    {
        if (stonePickeLevel < levelsIseStonePicke.Length - 1) // Проверяем, не в максимальном ли уровне
        {
            stonePickeLevel = level;
          
            Debug.Log($"{levelsIseStonePicke[stonePickeLevel].stonePickeName} улучшена до уровня {stonePickeLevel + 1}");
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
