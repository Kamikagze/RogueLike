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

        // ������ �������� ��� �������� ������� ����� ����
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
         // ��������� ����� ������
        if (pool == null)
        {
            Debug.LogError("Bullet pool is null! Make sure SetPool is called.");
        }
        gameObject.SetActive(false); // ������������ ������
        pool.ReturnObject(this); // ���������� ������ � ���
    }

    public void SetPool(BulletPool bulletPool)
    {
        pool = bulletPool; // ������������� ��� ��� ����

    }
    public void SetGlobalStats(StatsHolder _globalStats)
    {
        globalStats = _globalStats; // ������������� stats

    }

   
    public void LevelUp(int level)
    {
        if (bulletLevel < levelsOfBullet.Length - 1) // ���������, �� � ������������ �� ������
        {
            bulletLevel = level;

           // Debug.Log($"{levelsOfBullet[bulletLevel].bulletName} �������� �� ������ {bulletLevel + 1}");
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
