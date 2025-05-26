using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBolt : MonoBehaviour
{

    public MagicBoltPool pool;

    public MagicBoltScriptableObject[] levelsMagicBolt;
    public int magicBoltLevel = 0;
    [SerializeField]private Animator animator;
    
    




    public void Initialize(Transform enemyPos)
    {
        UnityEngine.Debug.Log("Initializing MagicBolt at position: " + enemyPos.position);
        transform.position = enemyPos.position;
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

    public void SetPool(MagicBoltPool magicBoltPool)
    {
        pool = magicBoltPool; // Устанавливаем пул для пули

    }



    public void LevelUp(int level)
    {
        if (magicBoltLevel < levelsMagicBolt.Length - 1) // Проверяем, не в максимальном ли уровне
        {
            magicBoltLevel = level;

            Debug.Log($"{levelsMagicBolt[magicBoltLevel].magicBoltName} улучшена до уровня {magicBoltLevel + 1}");
        }
        else
        {
            Debug.LogWarning("Максимальный Молнии пули достигнут!");
        }
    }
    public void Offer() // запускается из аниматора
    {
        ReturnToPool();
    }
}

