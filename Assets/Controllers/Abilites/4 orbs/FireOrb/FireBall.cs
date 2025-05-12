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

        // ��������� �������� ����
        rb.velocity = normalizedDirection * 2F;

        // ������ �������� ��� �������� ������� ����� ����
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
        // ��������� ����� ������
        if (pool == null)
        {
            Debug.LogError("Bullet pool is null! Make sure SetPool is called.");
        }
        gameObject.SetActive(false); // ������������ ������
        pool.ReturnObject(this); // ���������� ������ � ���
    }

    public void SetPool(FireBallPool fireBallPool)
    {
        pool = fireBallPool; // ������������� ��� ��� ����

    }



    public void LevelUp(int level)
    {
        if (fireBallLevel < levelsIseFireBall.Length - 1) // ���������, �� � ������������ �� ������
        {
            fireBallLevel = level;
            

            Debug.Log($"{levelsIseFireBall[fireBallLevel].fireBallName} �������� �� ������ {fireBallLevel + 1}");
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
            animator.SetTrigger("Explode");
        }
    }
}
