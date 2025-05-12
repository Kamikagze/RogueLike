using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "Artifact", menuName = "ScriptableObjects/Artifacts/Artifact")]
public class ArtifactScriptableObject : ScriptableObject
{
    public string artifactName;
    public string comparedAbility;
    public bool multipleArtifact;
    public RarityType rarity;
    public AspectOfUpgrade aspectOfUpgrade;
    public List<AspectOfUpgrade> aspectsOfUpgrade;
    public Sprite artifactImage;
     

    [Header("AbilitiesUpgrades")]
    public AbilityKeys[] abilityKeys;
    public float damageUpgrade;
    public float countUpgrade;
    public float radiusUpgrade;
    public float cooldownUpgrade;
    public float durationUpgrade;
    public float special;


    [Header("HealthUpgrades")]
    public float maxHealth;
    public float regeneration;
    public float heel;
    public float lifes;


    [Header("StatsUpgrades")]
    public float armor;
    public float speed;
    public float exp;
    public float radius;
    public float cooldown;
    public float damage;

    [Header("EnemyUpgrades")]
    public float freezeDuration;
    public float health;
    public float eXPAfterDeath;
    public float bonusDamage;




    [TextArea]
    public string description;

    public static event Action<AbilityKeys[], Dictionary<TypeUpgrade, float>> ArtifactUpgradeOfAbilities;
    public static event Action<Dictionary<HealthAspects, float>> ArtifactUpgradeOfHealth;
    public static event Action<Dictionary<StatsAspects, float>> ArtifactUpgradeOfStats;
    public static event Action<Dictionary<EnemyAspects, float>> ArtifactUpgradeOfEnemy;


    public void UpgradeEvent()
    {
        Debug.Log("я вызвался");
        if (!multipleArtifact)
        {
            NonMultipleTypeOfAspectSender();
            Debug.Log("одноразовый");
        }
        else if (multipleArtifact)
        {
            MultipleDictionaryMaker();
            Debug.Log("отработал по массиву");
        }

    }
    private void NonMultipleTypeOfAspectSender()
    {
        switch (aspectOfUpgrade)
        {
            case AspectOfUpgrade.ability:
                DictionarySender();
                break;

            case AspectOfUpgrade.health:
                HealthUpgrade();
                break;

            case AspectOfUpgrade.stats:
                StatsUpgrade();
                break;

            case AspectOfUpgrade.enemy:
                EnemyUpgrade();
                break;

        }
    }
    private void MultipleDictionaryMaker()
    {
       
        foreach (AspectOfUpgrade aspect in aspectsOfUpgrade)
        {
            aspectOfUpgrade = aspect;
            Debug.Log($"{ aspect}");
            NonMultipleTypeOfAspectSender();
            
        }

    }
    private void DictionarySender()
    {
        Dictionary<TypeUpgrade, float> upgradesInArtifact = new Dictionary<TypeUpgrade, float>();

        // Проверяем каждое значение и добавляем в словарь, если оно не нулевое
        if (damageUpgrade != 0)
        {
            upgradesInArtifact[TypeUpgrade.damage] = damageUpgrade;
        }

        if (countUpgrade != 0)
        {
            upgradesInArtifact[TypeUpgrade.count] = countUpgrade;
        }

        if (radiusUpgrade != 0)
        {
            upgradesInArtifact[TypeUpgrade.radius] = radiusUpgrade;
        }

        if (cooldownUpgrade != 0)
        {
            upgradesInArtifact[TypeUpgrade.cooldown] = cooldownUpgrade;
            Debug.Log($"{cooldownUpgrade}");
        }

        if (durationUpgrade != 0)
        {
            upgradesInArtifact[TypeUpgrade.duration] = durationUpgrade;
        }
        if (special != 0)
        {
            upgradesInArtifact[TypeUpgrade.special] = special;
        }

        Debug.Log($"{upgradesInArtifact.Count}");
        // Запускаем событие только если словарь не пустой
        if (upgradesInArtifact.Count > 0 )
        {
            ArtifactUpgradeOfAbilities?.Invoke(abilityKeys, upgradesInArtifact);
            Debug.Log("z pfgecnbk cj,snbt");

        }
      
    }
    private void HealthUpgrade()
    {
        Dictionary<HealthAspects,float> upgradesInArtifact = new Dictionary<HealthAspects,float>();

        if (maxHealth != 0)
        {
            upgradesInArtifact[HealthAspects.maxHealth] = maxHealth;
        }
        if (regeneration != 0)
        {
            upgradesInArtifact[HealthAspects.regeneration] = regeneration;
        }
        if (heel != 0)
        {
            upgradesInArtifact[HealthAspects.heel] = heel; 
        }
        if (lifes != 0)
        {
            upgradesInArtifact[HealthAspects.lifes] = lifes;
        }

        if (upgradesInArtifact.Count > 0 )
        {
            ArtifactUpgradeOfHealth?.Invoke(upgradesInArtifact);
        }
        //else if (upgradesInArtifact.Count > 0 && multipleArtifact)
        //{
        //    LastUpgradesInArtifact = upgradesInArtifact.ToDictionary(kv => (Enum)kv.Key, kv => kv.Value);
        //}

    }
    private void StatsUpgrade()
    {
        Dictionary<StatsAspects, float> upgradesInArtifact = new Dictionary<StatsAspects, float>();

          if (armor!= 0)
        {
            upgradesInArtifact[StatsAspects.armor] = armor;
        }
          if (speed != 0)
        {
            upgradesInArtifact[StatsAspects.speed] = speed;
        }
          if (exp != 0)
        {
            upgradesInArtifact[StatsAspects.exp] = exp;
        }
          if (radius != 0)
        {
            upgradesInArtifact[StatsAspects.radius] = radius;
        }
          if (cooldown != 0)
        {
            upgradesInArtifact[StatsAspects.cooldown] = cooldown;
        }
          if (damage != 0)
        {
            upgradesInArtifact[StatsAspects.damage] = damage;
        }
        if (upgradesInArtifact.Count > 0 )
        {
            ArtifactUpgradeOfStats?.Invoke(upgradesInArtifact);
        }
        

    }
    private void EnemyUpgrade()
    {
        Dictionary<EnemyAspects, float> upgradesInArtifact = new Dictionary <EnemyAspects, float>();
        if (bonusDamage != 0)
        {
            upgradesInArtifact[EnemyAspects.damage] = bonusDamage;
        }
        if (freezeDuration != 0)
        {
            upgradesInArtifact[EnemyAspects.freeze] = freezeDuration;
        }
        if (eXPAfterDeath != 0)
        {
            upgradesInArtifact[EnemyAspects.EXP] = eXPAfterDeath;
        }
        if (health != 0)
        {
            upgradesInArtifact[EnemyAspects.health] = health;
        }
        
        if (upgradesInArtifact.Count > 0)
        {
            ArtifactUpgradeOfEnemy?.Invoke(upgradesInArtifact);
        }
    }
}

