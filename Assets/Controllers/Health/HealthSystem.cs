using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem: MonoBehaviour, IHealthSystem, ITimeController
{
    [SerializeField] StatsHolder globalStats;
    [SerializeField] MagicShield magicShield;
    public float currentHealthPoints;
    public float maxHealthPoints;
    [SerializeField] private float invictibleTime;
    [SerializeField] HealthBar healthBar;

    private bool isUnableToTakeDamage;

    private float hpRestorer = 0.1f;
    public float currentRegen = 0;
    // Start is called before the first frame update
    private float currentTime = 0;

    private int currentLevel = 0;
    private float hpUpper = 4;

    public int countOfLife = 0;
    void Start()
    {
        
        IHealthSystem healthSystem = GetComponent<IHealthSystem>();
    }
    private void OnEnable()
    {
        TouchEvent.Touched += OnTouched;
        Collector.CollectHeart += Heel;

        ArtifactScriptableObject.ArtifactUpgradeOfHealth += UpgradeSystem;
    }
    // Update is called once per frame
   

    public void DamageTaker(float health, float Damage)
    {
       if (!isUnableToTakeDamage)
        {
            if (magicShield != null && magicShield.ActiveShields > 0)
            {
                magicShield.ShieldBreaker();
                return;
            }
            currentHealthPoints -= Damage * (1 - (globalStats.Armor / 100));
            healthBar.BarChanger(currentHealthPoints, maxHealthPoints);
        }
       
    }

    public void Heel()
    {
        currentHealthPoints += maxHealthPoints * hpRestorer;
        CheckerMaxHP();
        healthBar.BarChanger(currentHealthPoints, maxHealthPoints);
    }
    public void HealthImprovements(float value)
    {
        value = value / 100;
        float currentDifference = maxHealthPoints / currentHealthPoints;
        maxHealthPoints = maxHealthPoints + maxHealthPoints*value;
        currentHealthPoints *= currentDifference + 1;
        CheckerMaxHP();
        healthBar.BarChanger(currentHealthPoints, maxHealthPoints);

    }
    public void Timerred(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentRegen != 0)
        {
            if ( currentTime >= 1f)
            {
                currentHealthPoints += currentRegen;
                CheckerMaxHP();
                healthBar.BarChanger(currentHealthPoints, maxHealthPoints);
                currentTime = 0f;
            }
            
        }
    }
    private void CheckerMaxHP()
    {
        if (maxHealthPoints < currentHealthPoints)
        {  currentHealthPoints = maxHealthPoints;}
    }
    public void OnTouched(float _damage)
    {
        DamageTaker(currentHealthPoints, _damage);
        if (currentHealthPoints > 0)
        {
            StartCoroutine(InvictionTime());
        }
        else
        {
            Death();
        }
    }
    private IEnumerator InvictionTime()
    {
        TouchEvent.Touched -= OnTouched;
        yield return new WaitForSeconds(invictibleTime);
        TouchEvent.Touched += OnTouched;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GravityOrb"))
        {
            isUnableToTakeDamage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GravityOrb"))
        {
            isUnableToTakeDamage = false;
        }
    }
    private void RegenChanger(float value)
    {
        currentRegen += value;
    }
    private void HeelChanger(float value)
    {
        if(hpRestorer > 0)
        {
            value = value / 100;
            hpRestorer += value;
            if (hpRestorer < 0) {  hpRestorer = 0; }
        }
       
        
    }
    private void LifeCountChanger(float value)
    {
        countOfLife += (int)value;
    }


    private void Death()
    {
        countOfLife -= 1;
        if (countOfLife < 0)
        {

        }
        else if (countOfLife >= 0)
        {
            currentHealthPoints = maxHealthPoints;
        }
    }
    private void UpgradeSystem(Dictionary<HealthAspects, float> healthImprovements)
    {
        Debug.Log("work");
        foreach (var upgrades in healthImprovements)
        {
            HealthAspects healthAspects = upgrades.Key;
            float value = upgrades.Value;

            ImproveType(healthAspects, value);
        }
    }
    private void ImproveType(HealthAspects healthAspects, float value)
    {
        
        switch (healthAspects)
        {
            case HealthAspects.maxHealth:
                HealthImprovements(value); break;

            case HealthAspects.regeneration:
                RegenChanger(value); break;

            case HealthAspects.heel:
                HeelChanger(value); break;

            case HealthAspects:
                LifeCountChanger(value); break ;




        }

    }

    private void LevelHpImprovements()
    {
        currentLevel++;
        
    }
    private void OnDisable()
    {
        TouchEvent.Touched -= OnTouched;
        Collector.CollectHeart -= Heel;

        ArtifactScriptableObject.ArtifactUpgradeOfHealth -= UpgradeSystem;
    }
}
