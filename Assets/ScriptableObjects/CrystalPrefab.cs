using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPrefab : MonoBehaviour
{
    [SerializeField] private CrystalEXP crystal;

    public static event Action<Transform> OnGetCrystal;
    public static event Action<Transform> OnDeleteCrystal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("есть касание");
            EventEXPManager.OnExpCollected(crystal.crystalValue);


            Destroy(gameObject);

        }
    }
    private void OnEnable()
    {
        OnGetCrystal?.Invoke(transform);
    }
    private void OnDisable()
    {
        if (gameObject != null)
        {
            OnDeleteCrystal?.Invoke(transform);
        }
    }
}
