using System;
using System.Collections;

using UnityEngine;

public class Shoot : MonoBehaviour
{

        public BulletPool bulletPool; // ������ �� ��� ����
        public FounderOfEnemies enemies; // ���� ��� �������, ���� ��������
       



    private void Start()
    {
        FullFillButtons.IceHell += DestroyThis;

    }
   

    public void ShootMethod()
    {
        if (enemies != null)
        {
            var nearestEnemy = enemies.FindNearestEnemy();

            // ���������, ���������� �� ��������� ����
            if (nearestEnemy != null)
            {
                // ������� ����������� � ���������� �����
                Vector2 direction = (nearestEnemy.position - transform.position).normalized;

                // �������� ���� �� ����
                Bullet bullet = bulletPool.GetBullet();

                if (bullet != null)
                {
                    // ������������� ������� ���� � �������������� �
                    bullet.transform.position = transform.position;
                    bullet.Initialize(direction);
                }
                else
                {
                    Debug.LogWarning("�� ������� �������� ���� �� ����");
                }
            }
            else
            {
               // Debug.LogWarning("��� ��������� ������ ��� ��������");
            }
        }
    }


   

    private void DestroyThis()
    {
        StopAllCoroutines();
       gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        FullFillButtons.IceHell -= DestroyThis;
    }
}
