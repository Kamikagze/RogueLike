using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeManager : MonoBehaviour
{
    [SerializeField] private AbstractEnemy enemy;
   // [SerializeField] private MonsterSpawner[] monsterSpawners;

    private float passedTime = 0;
    private int passedCount;
    private float realTime;
    [SerializeField]private float timeUpper = 45f;
    private int minutesPassed;
    [SerializeField] private TextMeshProUGUI timer;
    private int secondsPassed;

    private int zaglyshka = 0;


    private List<ITimeController> interfaceImplementations = new List<ITimeController>();
    public static event Action OnUpgrade;

    private void Awake()
    {
        var allScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (var script in allScripts)
        {
            if (script is ITimeController timerredScript)
            {
                interfaceImplementations.Add(timerredScript);
            }
        }
    }
    private void Start()
    {
        enemy.StartingStats(passedCount);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        AllTimeInGame(deltaTime);
        
        

        IsEnoughToImprove();
        realTime = (passedCount * timeUpper) + passedTime;
        secondsPassed = Mathf.FloorToInt(realTime) % 60;
        minutesPassed = Mathf.FloorToInt(realTime / 60);
        timer.text = ($"{minutesPassed} : {CorrectionOfWriting(secondsPassed)}");
    }

    private void IsEnoughToImprove()
    {
        if (passedTime >= timeUpper)
        {
            passedTime = 0;
            passedCount++;
            enemy.StartingStats(passedCount);
            OnUpgrade?.Invoke();
        }
    }

    private void AllTimeInGame(float deltaTime)
    {
        passedTime += deltaTime;

        foreach (var implementation in interfaceImplementations)
        {
            implementation.Timerred(deltaTime);
        }

    }
    private void MinutesPassed()
    {
        realTime = passedCount * timeUpper + passedTime;
        if (realTime >= 60)
        {
            realTime = realTime - 60;
            minutesPassed += 1;
           
        }
    }
    private string CorrectionOfWriting(int seconds)
    {
        return seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        Scene current = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(current.name);
    }

   
}
