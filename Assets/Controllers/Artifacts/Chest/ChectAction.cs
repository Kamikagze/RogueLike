using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChectAction : MonoBehaviour
{
    public static event Action<Transform> ChestTransform;
    private void OnEnable()
    {
        ChestTransform?.Invoke(transform);
        Debug.Log($"{transform}");
    }
    private void OnDisable()
    {
        ChestTransform?.Invoke(null);
    }
}
