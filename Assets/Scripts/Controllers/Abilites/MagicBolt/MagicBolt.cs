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
        // ��������� ����� ������
        if (pool == null)
        {
            Debug.LogError("Bullet pool is null! Make sure SetPool is called.");
        }
        gameObject.SetActive(false); // ������������ ������
        pool.ReturnObject(this); // ���������� ������ � ���
    }

    public void SetPool(MagicBoltPool magicBoltPool)
    {
        pool = magicBoltPool; // ������������� ��� ��� ����

    }



    public void LevelUp(int level)
    {
        if (magicBoltLevel < levelsMagicBolt.Length - 1) // ���������, �� � ������������ �� ������
        {
            magicBoltLevel = level;

            Debug.Log($"{levelsMagicBolt[magicBoltLevel].magicBoltName} �������� �� ������ {magicBoltLevel + 1}");
        }
        else
        {
            Debug.LogWarning("������������ ������ ���� ���������!");
        }
    }
    public void Offer() // ����������� �� ���������
    {
        ReturnToPool();
    }
}

