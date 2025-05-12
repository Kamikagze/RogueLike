using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeartScript : MonoBehaviour, ITimeController
{
    [SerializeField] private GameObject heart;
   
  
    [SerializeField] private Transform positionOfSpawn;
    [SerializeField] private Animator anim;
    [SerializeField] private float timeSpawn;
    private float checkTime = 10f;
    [SerializeField] private float maxCount;
    [SerializeField] public float currentCount;


    public static event Action<Transform> OnGetHeart;
    public static event Action<Transform> OnGetOutHeart;
    private float currentTime;
    void Start()
    {
        anim.SetFloat("AnimSpeed", UnityEngine.Random.Range(0f, 3f));
        Heart.OnGetOutHeart += HPOuter;
    }

    // Update is called once per frame
    
    public void Timerred(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime > timeSpawn && currentCount < maxCount)
        {
            anim.SetFloat("AnimSpeed", UnityEngine.Random.Range(0f, 3f));
            Instantiate(heart, positionOfSpawn.position, Quaternion.identity);
            currentCount++;
            currentTime = 0f;
        }
        else if (currentTime > timeSpawn && currentCount>= maxCount)
        {
            currentTime = checkTime;
        }
    }
   
   

   
    private void HPOuter(Transform tp)
    {
        currentCount -= 1;
    }

    private void OnDisable()
    {
        Heart.OnGetOutHeart -= HPOuter;
    }

    
}
