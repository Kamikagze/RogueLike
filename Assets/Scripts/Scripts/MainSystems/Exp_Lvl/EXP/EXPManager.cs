
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using System;
public class EXPManager : MonoBehaviour
{

    [SerializeField] Image expBar;
    [SerializeField] private TextMeshProUGUI expforNextLevel;
    public float nextLevelEXP;
    public float currenEXP;
    public int level;
    

    public static event Action CountOfRerolls;

    // Нужно переписать с использование ScriptableObjects
    // [Header("level")]
    public static event Action LevelingUp;

    

    void Start()
    {
        EventEXPManager.ExpCollected += TestOnExpCollected;
        AbstractEnemy.GiveEXPAfterDeath += IncomeEXPAfterDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void TestOnExpCollected(int _crystalCost)
    {
        currenEXP += _crystalCost;
        RefillPanel();

    }
    public void LevelUp(float _currenEXP)
    {
       
        
        if (currenEXP >= nextLevelEXP)
        { 
            OnLevelingUp();
            if (currenEXP >= nextLevelEXP)
            {
                level += 1; 
                RerollChecker();
                _currenEXP = currenEXP - nextLevelEXP ;
                currenEXP = _currenEXP;
                nextLevelEXP = Mathf.Ceil(nextLevelEXP * 1.15f);
                expBar.fillAmount = currenEXP / nextLevelEXP;
            }
                
        }

    }
    private void RerollChecker()
    {
        
        if (level % 3 == 0)
        {
            CountOfRerolls?.Invoke();
        }
    }
    private void IncomeEXPAfterDeath(float exp)
    {
        currenEXP += exp;
        RefillPanel();
    }
    private void RefillPanel()
    {
        LevelUp(currenEXP);
        expBar.fillAmount = currenEXP / nextLevelEXP;
    }
    public void OnLevelingUp()
    {
        LevelingUp?.Invoke();
    }

    private void OnDisable()
    {
        EventEXPManager.ExpCollected -= TestOnExpCollected;
        AbstractEnemy.GiveEXPAfterDeath -= IncomeEXPAfterDeath;
    }
}
