using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EXP_Spawner : MonoBehaviour, ITimeController
{
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject prefabEXP;
    public int expCounter;
    [SerializeField] private int maxValueOfCrystalsOnScreen;


    private float currentTime;
    private void OnEnable()
    {
        CrystalPrefab.OnDeleteCrystal += ControllerOfNuber;
    }


    [SerializeField] private List<GameObject> prefabs;
   
    

    private void ChooseOfPointForSpawn(out int spawnPoint)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("������ ����� ������ ����!");
            spawnPoint = -1; // ������������� -1 ��� ������
            
            return;
        }

        spawnPoint = Random.Range(0, spawnPoints.Length);
      
    }

    private void PercentVer(out int typeOfCrystal)
    {
        int percent = Random.Range(0, 100); // ���������� ��������� �������
        if (percent < 80)
        {
            typeOfCrystal = 0; // ��� ��������� 0
        }
        else if (percent >= 80 && percent <= 95)
        {
            typeOfCrystal = 1; // ��� ��������� 1
        }
        else
        {
            typeOfCrystal = 2; // ��� ��������� 2
        }

        // ����� �������� ��� ��� �������
        
    }
    private void Spawn(int spawnPoint,out int typeOfCrystal)
    {
        
        PercentVer (out typeOfCrystal);
        Vector2 spawnPosition = new Vector2(spawnPoints[spawnPoint].transform.position.x, spawnPoints[spawnPoint].transform.position.y);
        GameObject newObject = Instantiate(prefabs[typeOfCrystal], spawnPosition, prefabEXP.transform.rotation, parentTransform);

        
        return;
    }

   
   

    public void Timerred(float deltaTime)
    {
        if (expCounter <= maxValueOfCrystalsOnScreen)
        {
            currentTime += deltaTime;
            if (currentTime >= 1f)
            {
                ChooseOfPointForSpawn(out int spawnPoint);
                if (spawnPoint != -1) // ��������, ��� ����� ������ ���� ������� ���������
                {
                    Spawn(spawnPoint, out int typeOfCrystal);
                    expCounter += 1;
                }
                currentTime = 0f;
            }
        }

       
    }
    private void ControllerOfNuber(Transform no)
    {
        expCounter--;
    }
    private void OnDisable()
    {
        CrystalPrefab.OnDeleteCrystal -= ControllerOfNuber;
    }
}
