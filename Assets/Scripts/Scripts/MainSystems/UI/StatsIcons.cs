using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsIcons : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] stats;

    [SerializeField] private StatsHolder statsHolder;
    [SerializeField] private HealthSystem healthSystem;


    public void SetInfo()
    {
        stats[0].text = (statsHolder.Armor).ToString();
        stats[1].text = healthSystem.currentHealthPoints.ToString() + " / " + healthSystem.maxHealthPoints.ToString();
        stats[2].text = healthSystem.currentRegen.ToString();
        stats[3].text = (statsHolder.DamageImprover * 100 ).ToString();
        stats[4].text = (statsHolder.CooldownReduction * 100).ToString();
        stats[5].text = (statsHolder.Speed * 100).ToString();
        stats[6].text = (statsHolder.EXPImprove * 100).ToString();
        stats[7].text = healthSystem.countOfLife.ToString();
    }

    private void OnEnable()
    {
        SetInfo();
    }
}
