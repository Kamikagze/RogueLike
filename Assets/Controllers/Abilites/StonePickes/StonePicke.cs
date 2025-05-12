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
        // ������ �������� ��� �������� ������� ����� ����
        StartCoroutine(StartLifetimeCoroutine());
       
    }


   

    private void ReturnToPool()
    {
        // ��������� ����� ������
        if (pool == null)
        {
            Debug.LogError("Bullet pool is null! Make sure SetPool is called.");
        }
        gameObject.SetActive(false); // ������������ ������
        pool.ReturnObject(this); // ���������� ������ � ���
    }

    public void SetPool(StonePickePool stonePickePool)
    {
        pool = stonePickePool; // ������������� ��� ��� ����

    }



    public void LevelUp(int level)
    {
        if (stonePickeLevel < levelsIseStonePicke.Length - 1) // ���������, �� � ������������ �� ������
        {
            stonePickeLevel = level;
          
            Debug.Log($"{levelsIseStonePicke[stonePickeLevel].stonePickeName} �������� �� ������ {stonePickeLevel + 1}");
        }
        else
        {
            Debug.LogWarning("������������ ������� ���� ���������!");
        }
    }
    private IEnumerator StartLifetimeCoroutine()
    {
        // ��� �������� ����� ����� ����
        yield return new WaitForSeconds(lifetime);

       

        // ���������, ������� �� ����, � ���� ��, ���������� � � ���
        if (gameObject.activeSelf)
        {
            ReturnToPool();
        }
    }
   
}
