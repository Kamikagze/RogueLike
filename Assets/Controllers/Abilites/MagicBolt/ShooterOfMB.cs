using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShooterOfMB : MonoBehaviour
{


    public MagicBoltPool magicBoltPool; // Ссылка на пул пуль
    public FounderOfEnemies enemies; // Враг или позиция, куда стреляем
    
    public int numberOfMagicBolt;


    private void Start()
    {
        
    }

   
    private void ShootMethod(Transform enemyPos)
    {
        UnityEngine.Debug.Log("ShootMethod called");
        if (enemyPos != null)
        {
            MagicBolt magicBolt = magicBoltPool.GetMagicBolt();
            if (magicBolt != null)
            {
                magicBolt.Initialize(enemyPos);
            }
            else
                {
                    UnityEngine.Debug.LogWarning("Не удалось получить пулю из пула");
                }
            
        }
    }

    public void TryToShoot()
    {
        int numberOfEnemies = GetNumberOfEnemies();
        Debug.Log("Number of enemies ready to shoot: " + numberOfEnemies);
        if (numberOfEnemies > 0)
        {
             Shooting(numberOfEnemies);
        }
    }

    private int GetNumberOfEnemies()
    {
        if (enemies != null)
        {
            Debug.Log(enemies.GetDetectedEnemies().Count);
            return enemies.GetDetectedEnemies().Count;
           
        }
        return 0;
    }

    private void Shooting(int totalEnemies)
    {
       
            
            int effectiveShotCount = Mathf.Min(numberOfMagicBolt, totalEnemies);

            

            // Перемешивание врагов для выбора случайных
            List<Transform> enemyList = new List<Transform>(enemies.GetDetectedEnemies());
            Shuffle(enemyList); // Перемешиваем врагов для случайного выбора

            // Стреляем к количеству, равному effectiveShotCount
            for (int i = 0; i < effectiveShotCount; i++)
            {
                ShootMethod(enemyList[i]);
            }

            
       
    }

    // Метод для перемешивания списка врагов
    private void Shuffle(List<Transform> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    private void OnDisable()
    {

    }
}