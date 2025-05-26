using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : AbstractAbill
{
    [SerializeField] private GravityOrbScriptableObjects[] gravityOrbScriptableObjects;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Animator animator;
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] Transform gravityOrbScale;
    private float gravityActionDuration = 3f;
    private float gravityFlyTime = 1.5f;
    private float minRadius = 1f;
    private float maxRadius = 2.5f;
    public float gravityOrbCooldown;
    private float currentTime = 0;

    private bool isOnPosition = false;
    public static event Action<Vector2> OnGravity;

    [Header("Parents")]
    [SerializeField] private Transform parentDefolt;
    [SerializeField] private Transform parentAction;
    public bool isWithPlayer = false;

    private bool defaultScaleFactor = true;
    private Vector2 gravityOrbDefaultScale;
    private Vector2 gravityOrbActionScale;


    private Vector2 startPos;
    private Vector2 endPos;

    public float GravityOrbDamage { get; private set; } // Надо подумать на счет свойств или избавления от этого метода

    public static event Action GravityOrbActionEvent;
    private void Start()
    {
        
        
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        GravityOrbScriptableObjects.GravityOrbUpgrade += Reinitialize;
        SetParent();

        Activate();
        
    }

    protected override void ActionOfAbill()
    {
        currentTime = 0;
        isOnPosition = false ;
        startPos = GetStartPos();
        endPos = GetRandomPointInCircle();
        SetParent();
        animator.ResetTrigger("StartAction");
        animator.ResetTrigger("Explode");
    }
    protected override void DurationPartOfAbill(float deltaTime)
    {
        currentTime += deltaTime;
        if (!isOnPosition)
        {
            FirstPhase();
        }

        
    }

    private void ScaleChanger()
    {
        if (defaultScaleFactor) { gravityOrbScale.localScale = gravityOrbDefaultScale; }
        else { gravityOrbScale.localScale = gravityOrbActionScale; }
    }
    private void FirstPhase()
    {
        if (currentTime <= gravityFlyTime)
        {
            transform.position = Vector2.Lerp(startPos, endPos, Mathf.Clamp01(currentTime / gravityFlyTime));
        }
        else
        {
            transform.position = endPos;
            isOnPosition = true;
            currentTime = 0;
            defaultScaleFactor = false;
            ScaleChanger();
            animator.SetTrigger("StartAction");
        }
    }
    protected override void Offers()
    {
        animator.SetTrigger("Explode");
        

    }
    private Vector2 GetStartPos()
    {
        Vector2 pos = new Vector2 (playerPosition.transform.position.x, playerPosition.transform.position.y + 0.7f );
        return pos;
    }
   
    private void FindPlaceWhereGravityOrbStarts(Vector2 vector)
    {
        transform.position = vector;
    }
    private Vector2 GetRandomPointInCircle()
    {
        // Получаем случайный угол
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        // Получаем случайный радиус
        float randomRadius = UnityEngine.Random.Range(minRadius, maxRadius);

        // Преобразуем в координаты x и y для 2D
        float x = playerPosition.position.x + randomRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float y = playerPosition.position.y + randomRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        // Возвращаем случайную точку в виде вектора
        return new Vector2(x, y);
    }
   
    
   
    
    public void Gravitise()
    {
        OnGravity?.Invoke(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            Gravitise();
    }
    public void SetParent()
    {
        
        if (isWithPlayer)
        {
            isWithPlayer = false;
            gameObject.transform.SetParent(parentAction);
        }
        else if (!isWithPlayer)
        {
            isWithPlayer = true;
            defaultScaleFactor = true;
            ScaleChanger();
            gameObject.transform.SetParent(parentDefolt);
            transform.position = GetStartPos();

        }
    }
    protected override void OnDisable()
    {
        
        StatsHolder.DamageImproverIncreased -= RadiusUpgrade;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        GravityOrbScriptableObjects.GravityOrbUpgrade -= Reinitialize;
    }

    public override void CooldownReduction()
    {
        gravityOrbCooldown = gravityOrbScriptableObjects[abilityLevel].gravityOrbCooldown
            * statsHolder.CooldownReduction* bonusCooldown;
        ChangeCooldown(gravityOrbCooldown);
    }

    protected override void DamageUpgrage()
    {
        GravityOrbDamage = gravityOrbScriptableObjects[abilityLevel].gravityOrbDamage * bonusDamage;
        GravityOrbActionEvent?.Invoke();
    }

    protected override void RadiusUpgrade()
    {
        gravityOrbActionScale = new Vector2((gravityOrbScriptableObjects[abilityLevel].gravityOrbRadius
            * statsHolder.Radius * bonusRadius),
            (gravityOrbScriptableObjects[abilityLevel].gravityOrbRadius * statsHolder.Radius * bonusRadius));
               
        gravityOrbDefaultScale = gravityOrbActionScale / 2;
        ScaleChanger();
    }

    protected override void DurationUpgrade()
    {
        Duration = gravityActionDuration + gravityFlyTime + 0.1f;
    }

    protected override void CountUpgrade()
    {
       
    }
}
