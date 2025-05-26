using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public static event Action<Transform> OnGetHeart;
    public static event Action<Transform> OnGetOutHeart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        OnGetHeart?.Invoke(transform);
    }
    private void OnDisable()
    {
        OnGetHeart?.Invoke(transform);
    }
}
