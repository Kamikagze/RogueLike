using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ѕерсонаж, за которым будет следовать камера
    public float smoothSpeed = 0.125f; // —корость сглаживани€
    public Vector3 offset; // —мещение камеры относительно персонажа

    void LateUpdate()
    {
        if (target != null)
        {
            // ќпредел€ем позицию камеры, добавл€€ смещение к позиции персонажа
            Vector3 desiredPosition = target.position + offset;
            // ѕлавно перемещаем камеру к цели, использу€ Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // ”станавливаем позицию камеры
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}
