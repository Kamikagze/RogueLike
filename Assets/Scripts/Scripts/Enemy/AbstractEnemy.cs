using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class AbstractEnemy : MonoBehaviour
{
    [SerializeField] public EnemyScriptableObject enemy;
    [SerializeField] private Rigidbody2D enemyRb;
    public Transform player;
    [SerializeField] public SpriteRenderer enemySpriteRenderer;
    [SerializeField] private BoxCollider2D enemyCollider;
    public static event Action<float> GiveEXPAfterDeath;
    [SerializeField] protected Animator animator;
    public float currentHealth = 2;
    public float currentDamage = 2;
    private Color marker = new Color(200 / 255, 200 / 255, 200 / 255);
    protected bool inProcessOfDeath = false;

    //Stats
    protected float freezeDuration = 1f;
   

    // bonusStats
    private float bonusFreezeDuration = 1f;
    private float bonusMaxHealth = 1f;
    private float bonusEXPAfterDeath = 1f;
    private float bonusDamage = 1f;

    public static event Action<Transform> Remove;
    protected virtual void OnEnable()
    {
        ArtifactScriptableObject.ArtifactUpgradeOfEnemy += UpgradeSystem;
    }
    protected enum EnemyState
    {
        Normal,
        Frozen,
        Shocked,
        Slowed
    }

    protected EnemyState currentState = EnemyState.Normal;

    [Header("AbillWhichNeedObjects")]
    public DamageAbillController damageAbillController;
    private Dictionary<string, Action<DamageAbillController, EnemyBehaviour>> damageActions;

    private bool isFrozen = false;
    private bool isShocked = false;
    protected bool isGravitised = false;

    private Vector2 gravityCenter;

    void Start()
    {
        damageActions = new Dictionary<string, Action<DamageAbillController, EnemyBehaviour>>
    {
        { "Bullet", (damageController, enemy) => OnTakeDamage(damageController.BulletTotalDamage, enemy) },
        { "Flash", (damageController, enemy) => OnTakeDamage(damageController.FlashAbillTotalDamage, enemy) },
        { "StoneWalk", (damageController, enemy) => OnTakeDamage(damageController.StoneWalkTotalDamage, enemy) },
        { "WanderingFlash", (damageController, enemy) => { OnTakeDamage(damageController.WanderingFlashTotalDamage, enemy); Shock(0.2f); } },
        { "FreezingField", (damageController, enemy) => { OnTakeDamage(damageController.FreezingFieldTotalDamage, enemy); Freeze(1.5f * bonusFreezeDuration); } },
        { "Meteor", (damageController, enemy) => OnTakeDamage(damageController.MeteorTotalDamage, enemy) },
        { "IceArrow", (damageController, enemy) => { OnTakeDamage(damageController.IceArrowTotalDamage, enemy); Slow(2f); } },
        { "MagicBolt", (damageController, enemy) => { OnTakeDamage(damageController.MagicBoltTotalDamage, enemy); Shock(0.3f); } },
        { "LavaField", (damageController, enemy) => { OnTakeDamage(damageController.LavaFieldTotalDamage, enemy); Slow(0.5f); } },
        { "FireBall", (damageController, enemy) => OnTakeDamage(damageController.FireBallTotalDamage, enemy) },
        { "LightningOrb", (damageController, enemy) => { OnTakeDamage(damageController.LightningOrbTotalDamage, enemy); Shock(0.2f); } },
        { "GravityOrb", (damageController, enemy) => { Gravity.OnGravity += Ongravitise; } },
        { "GravityExplosion", (damageController, enemy) => OnTakeDamage(damageController.GravityOrbTotalDamage, enemy) },
        { "Orbital", (damageController, enemy) => OnTakeDamage(damageController.ElectricOrbitalTotalDamage, enemy) },
        { "Laser", (damageController, enemy) => OnTakeDamage(damageController.LaserTotalDamage, enemy) },
        { "StonePicke", (damageController, enemy) => OnTakeDamage(damageController.StonePickeTotalDamage, enemy) },
        { "FireBreath", (damageController, enemy) => OnTakeDamage(damageController.FireBreathTotalDamage, enemy) },
        { "Cataclysm", (damageController, enemy) => { inProcessOfDeath = true; currentState = EnemyState.Shocked; animator.SetTrigger("defeated"); } },
        { "Eruption", (damageController, enemy) => OnTakeDamage(damageController.EruptionTotalDamade, enemy) },
        { "WindSlash", (damageController, enemy) => OnTakeDamage(damageController.WindSlashTotalDamage, enemy) }
    };
    }


    protected void HandleCollision(Collider2D collision, DamageAbillController damageAbillController, EnemyBehaviour enemy)
    {
        if (damageActions.TryGetValue(collision.gameObject.tag, out var action))
        {
            action(damageAbillController, enemy);
        }
    }
    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    public void Shock(float duration)
    {
        // Применячем шок, только если враг не заморожен
        if (!isFrozen && currentState == EnemyState.Normal)
        {
            isShocked = true;
            currentState = EnemyState.Shocked;
            StartCoroutine(ShockCoroutine(duration));
        }
    }

    // Функция для отображения урона
    public void DamageMarker(float duration)
    {
        StartCoroutine(DamageMarkerCoroutine(duration));
    }

    private IEnumerator DamageMarkerCoroutine(float duration)
    {
        
        enemySpriteRenderer.color = marker; // маркер
        
        yield return new WaitForSeconds(duration);
        if (currentState == EnemyState.Normal) { enemySpriteRenderer.color = Color.white; }
        else if (currentState == EnemyState.Frozen) { enemySpriteRenderer.color = Color.blue; }
        else if (currentState == EnemyState.Slowed) { enemySpriteRenderer.color = Color.yellow; }
        else if (currentState == EnemyState.Shocked) { enemySpriteRenderer.color = Color.white; }



        }

    public void Slow(float duration)
    {
        // Применяем замедление только если враг не в состоянии заморозки и шока
        if (!isFrozen && currentState == EnemyState.Normal)
        {
            currentState = EnemyState.Slowed;
            StartCoroutine(SlowCoroutine(duration));
        }
    }

    private IEnumerator ShockCoroutine(float duration)
    {
        enemySpriteRenderer.color = marker; // Цвет во время шока
        yield return new WaitForSeconds(duration);
        enemySpriteRenderer.color = Color.white; // Возвращаем цвет
        currentState = EnemyState.Normal;
        isShocked = false; // Убираем эффект шока
    }

    private IEnumerator SlowCoroutine(float duration)
    {
        enemySpriteRenderer.color = Color.yellow; // Цвет во время замедления
        yield return new WaitForSeconds(duration);
        enemySpriteRenderer.color = Color.white; // Возвращаем цвет
        currentState = EnemyState.Normal; // Сброс состояния
    }

    public void Freeze(float duration)
    {
        if (!isFrozen) // Не можем заморозить, если уже заморожен
        {
            isFrozen = true;
            currentState = EnemyState.Frozen;
            enemySpriteRenderer.color = Color.blue; // Меняем цвет на синий
            StartCoroutine(UnfreezeAfterDuration(duration));
        }
    }

    private IEnumerator UnfreezeAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentState = EnemyState.Normal; // Возвращаемся к нормальному состоянию
        isFrozen = false; // Убираем эффект заморозки
        enemySpriteRenderer.color = Color.white; // Цвет после разморозки
    }

    protected void MoveTowardsPlayer()
    {
        if (currentState != EnemyState.Frozen && currentState != EnemyState.Shocked && !isGravitised) // Двигаться только если не заморожен
        {
            float speedMultiplier = (currentState == EnemyState.Slowed) ? 0.6f : 1.0f; // Если замедлен, используем 60% скорости
            transform.position = Vector2.MoveTowards(transform.position, player.position,
                enemy.speed * speedMultiplier * enemy.speedMultiplier * Time.deltaTime);
        }
        else if (isGravitised)
        {
            transform.position = Vector3.MoveTowards(transform.position, gravityCenter, enemy.speed * 2 * Time.deltaTime);
        }

    }


    public void Ongravitise(Vector2  gravityPos)
    {
        isGravitised = true;
        gravityCenter = gravityPos;
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player")&& !isGravitised && !inProcessOfDeath)
        {
            Debug.Log($"я ударил " + "умираю ли я"+ inProcessOfDeath + "нанес урон" + currentDamage);
            TouchEvent.OnTouched(currentDamage* bonusDamage);
        }

    }
    

    protected abstract void OnTakeDamage(float damage, EnemyBehaviour enemy);

    protected void OnGiveEXPAfterDeath()
    {
        GiveEXPAfterDeath?.Invoke(enemy.howManyEXPHold * bonusEXPAfterDeath);
        Remove?.Invoke(transform);
        inProcessOfDeath = true;
        enemyCollider.enabled = false;
        currentState = EnemyState.Shocked;
        animator.SetTrigger("defeated");
    }
    public void Deathing()
    {
        
       
        Destroy(gameObject);
    }

    public void StartingStats(int times)
    {

        currentHealth = enemy.Health * MathF.Pow(enemy.HealthMultiplier * bonusMaxHealth, times);
        currentDamage = enemy.damage * MathF.Pow(enemy.damageMultiplier * bonusDamage, times);


        Debug.Log($"Current Health: {currentHealth}");
        Debug.Log($"Current Damage: {currentDamage}");
    }
   


    private void SetBonusDamage(float value)
    {
        value = value / 100;
        bonusDamage += value;
    }
    private void SetBonusHealth(float value)
    {
        value = value / 100;
        bonusMaxHealth += value;
    }
    private void SetBonusExp(float value)
    {
        value = value / 100;
        bonusEXPAfterDeath += value;
    }
    private void SetFreeseDuration(float value)
    {
        value = value / 100;
        bonusFreezeDuration += value;
    }

    private void UpgradeSystem(Dictionary<EnemyAspects, float> enemyImprovements)
    {
        Debug.Log("work");
        foreach (var upgrades in enemyImprovements)
        {
            EnemyAspects enemyAspects = upgrades.Key;
            float value = upgrades.Value;

            ImproveType(enemyAspects, value);
        }
    }
    private void ImproveType(EnemyAspects enemyAspects, float value)
    {

        switch (enemyAspects)
        {
            case EnemyAspects.damage:
                SetBonusDamage(value); break;

            case EnemyAspects.health:
               SetBonusHealth(value); break;

            case EnemyAspects.freeze:
               SetFreeseDuration(value); break;

            case EnemyAspects.EXP:
                SetBonusExp(value); break;




        }

    }











    protected virtual void OnDisable()
    {
       
        ArtifactScriptableObject.ArtifactUpgradeOfEnemy += UpgradeSystem;

    }

}