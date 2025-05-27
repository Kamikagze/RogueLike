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
        // ��������� ����� ������
        if (pool == null)
        {
            Debug.LogError("Bullet pool is null! Make sure SetPool is called.");
        }
        gameObject.SetActive(false); // ������������ ������
        pool.ReturnObject(this); // ���������� ������ � ���
    }

    public void SetPool(IceArrowPool iceArrowPool)
    {
        pool = iceArrowPool; // ������������� ��� ��� ����

    }
    

    
    public void LevelUp(int level)
    {
        if (arrowLevel < levelsIseArrow.Length - 1) // ���������, �� � ������������ �� ������
        {
            arrowLevel = level;
            Checker();
            Debug.Log($"{levelsIseArrow[arrowLevel].iceArrowName} �������� �� ������ {arrowLevel + 1}");
        }
        
        else
        {
            Debug.LogWarning("������������ ������� ���� ���������!");
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
        // ��� �������� ����� ����� ����
        yield return new WaitForSeconds(lifetime);

        // ���������, ������� �� ����, � ���� ��, ���������� � � ���
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
