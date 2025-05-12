
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class AbilityEntry
{
    public string key;
    public AbstractAbill ability;
}

public class CooldownManager : MonoBehaviour, ITimeController
{
    [SerializeField] private List<AbilityEntry> abilitiesList;

    private Dictionary<string, AbstractAbill> abilities;


    void Awake()
    {
        abilities = new Dictionary<string, AbstractAbill>();
        foreach (var entry in abilitiesList)
        {
            abilities[entry.key] = entry.ability;
        }
    }
    private void OnEnable()
    {
        StatsHolder.CooldownReductionIncreased += GlobalCooldown;
        Concentration.ConcentrationActionEvent += ConcentrationCD;
    }

    public void Timerred(float deltaTime)
   
        {
            
            foreach (var ability in abilities.Values)
            {
                ability.ActionTime(deltaTime);
            }
        }


    public void ChangeAbilityCooldown(string abilityKey, float newCooldown) // Меняем индекс на ключ
    {
        if (abilities.ContainsKey(abilityKey)) // Проверяем, существует ли способность с данным ключом
        {
            abilities[abilityKey].ChangeCooldown(newCooldown);
        }
    }

    private void GlobalCooldown()
    {
        foreach (var ability in abilities.Values) // Перебираем способности с использованием .Values
        {
            if (ability.IsActive)
            {
                ability.CooldownReduction();
            }
        }
    }

    private void ConcentrationCD(float percent)
    {
        foreach (var ability in abilities.Values) // Используем .Values для итерации
        {
            if (ability.IsActive)
            {
                ability.PercentReduction(percent);
            }
        }
    }

    private void OnDisable()
    {
        StatsHolder.CooldownReductionIncreased -= GlobalCooldown;
        Concentration.ConcentrationActionEvent -= ConcentrationCD;
    }
}

