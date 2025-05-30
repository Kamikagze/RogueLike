using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ��������, �� ������� ����� ��������� ������
    public float smoothSpeed = 0.125f; // �������� �����������
    public Vector3 offset; // �������� ������ ������������ ���������

    void LateUpdate()
    {
        if (target != null)
        {
            // ���������� ������� ������, �������� �������� � ������� ���������
            Vector3 desiredPosition = target.position + offset;
            // ������ ���������� ������ � ����, ��������� Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // ������������� ������� ������
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}
