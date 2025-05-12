using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperForHideButton : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject abilities;

    //private void Awake()
    //{
    //    gameObject.SetActive(false);
    //}
    private void OnEnable()
    {
        button.SetActive(false);
        abilities.SetActive(false);
    }
    private void OnDisable()
    {
        button?.SetActive(true);
        abilities?.SetActive(true);
    }
}
