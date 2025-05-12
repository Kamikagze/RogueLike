using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    public static event Action<float> Touched;


    public static void OnTouched(float _damage)
    {
        Touched?.Invoke(_damage);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
