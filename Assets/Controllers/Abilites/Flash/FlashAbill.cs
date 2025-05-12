using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAbill : AbstractAbill
{
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] private CircleCollider2D flashCollider;
    [SerializeField]private Transform player;
    [SerializeField] private FlashScriptableObject[] flashScriptableObjects;
    private float radiusOfMoove = 10f;
   
    public int flashLevel = 0;
    public float duration = 0.4f;// Время для перемещения к противоположной стороне
    
    public float waitTime;  // Время ожидания перед следующим перемещением
    
    public float FlashDamage {  get; private set; }

    public static event Action FlashDamageIncrease;
    private void Start()
    {
        
        ReInitialize();
        StatsHolder.RadiusIncreased += ReInitialize;
        FlashScriptableObject.FlashUpgradeEvent += LevelUpFlash;
        Activate();
    }

    private IEnumerator MooveToSide(Vector2 targetPosition)
    {
        
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.position = Vector2.Lerp(transform.position, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // Ожидание следующего кадра
            }

            // Убедитесь, что объект точно достиг цели
            transform.position = targetPosition;

    }

    protected override void ActionOfAbill()
    {
        
        float angle = UnityEngine.Random.Range(0f, 360f);
        Vector2 startPosition = GetPositionOnCircle(angle);

        // Позиция противоположной стороны окружности 
        Vector2 targetPosition = GetPositionOnCircle(angle + 180f);

        transform.position = startPosition;

        StartCoroutine(MooveToSide(targetPosition));
    }
    


    
    private Vector2 GetPositionOnCircle(float angle)
    {
        // Конвертируем угол в радианы
        float radians = angle * Mathf.Deg2Rad;

        // Вычисляем координаты на окружности
        float x = player.position.x + Mathf.Cos(radians) * radiusOfMoove;
        float y = player.position.y + Mathf.Sin(radians) * radiusOfMoove;

        // Возвращаем позицию с учетом текущего положения объекта
        return new Vector2( x, y); // Возвращаем только X и Y для 2D
    }
    private void LevelUpFlash()
    {
        flashLevel += 1;
        ReInitialize();
        
    }
    public void ReInitialize()
    {
        RadiusUpgrade();
        CooldownReduction();
        DamageUpgrage();
    }
    protected override void DamageUpgrage()
    
    {
        FlashDamage = flashScriptableObjects[flashLevel].flashDamage;
        FlashDamageIncrease?.Invoke();
    }
    public override void CooldownReduction()
    {
        waitTime = flashScriptableObjects[flashLevel].flashCooldown * statsHolder.CooldownReduction * cooldownMultiplicator;
        ChangeCooldown(waitTime);
    }


 protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.RadiusIncreased -= ReInitialize;
        FlashScriptableObject.FlashUpgradeEvent -= LevelUpFlash;
    }

    protected override void RadiusUpgrade()
    {
        flashCollider.radius = flashScriptableObjects[flashLevel].flashRadius * statsHolder.Radius * bonusRadius;
    }

    protected override void DurationUpgrade()
    {
        
    }

    protected override void CountUpgrade()
    {
        
    }
}
   

