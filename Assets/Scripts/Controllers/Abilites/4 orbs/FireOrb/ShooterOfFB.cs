using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterOfFB : MonoBehaviour
{

    public FireBallPool fBPool; // ������ �� ��� ����
    [SerializeField] private Transform player;




    private void Start()
    {
       
    }


    public void ShootMethod()
    {
        // �������� ��������� ������� �� ����������
        Vector2 targetPosition = GetRandomPositionOnCircle(1f);

        // ������� ����������� � ���� ������� �������, ������� ������� ������
        Vector2 direction = targetPosition - (Vector2)transform.position;

        // �������� ���� �� ����
        FireBall fireball = fBPool.GetFireBall();

        if (fireball != null)
        {
            // ������������� ������� ����
            fireball.transform.position = transform.position;

            // ������������ �������� ��� � ������� �����������
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ����������� � ������� � �������
            fireball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // �������������� �������� ���
            fireball.Initialize(direction);
        }
        else
        {
            Debug.LogWarning("�� ������� �������� ���� �� ����");
        }
    }


    private Vector2 GetRandomPositionOnCircle(float radius)
    {
        // ���������� ��������� ���� � ��������
        float angle = Random.Range(0f, 2 * Mathf.PI);

        // ��������� ���������� ����� �� ���������� ������������ ������ �������
        float x = player.position.x + radius * Mathf.Cos(angle);
        float y = player.position.y + radius * Mathf.Sin(angle);

        // ���������� ��������� ����� �� ���������� ��� Vector2
        return new Vector2(x, y);
    }



    private void OnDisable()
    {
        
    }
}
