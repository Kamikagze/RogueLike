using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbill : MonoBehaviour, ICooldownable
{
    [SerializeField] protected int IDabill;
    protected float cooldownTime;
    protected float timer;

    public int abilityLevel = 0;
    public bool IsActive {get; protected set;}

    protected float cooldownMultiplicator = 1; // мультипликатор для перезарядок

    protected bool isInWork; // Флаг для избежания множественных вызовов

    protected virtual float Duration { get; set; }   // Длительность работы способности (по умолчанию 0, если не переопределено)
                                                     // момент в том, что сеттер открыт но кроме наследников к нему никто не может обратиться.


    // должно сработать если подключить ко всем абилкам и вызывать активацию через 1 метод
    // логика в том чтобы 1. получить контроль перезарядки сразу после изменениния а не ждать окончания корутины 
    // 2. избавится от тонны разом запущенных корутин
    // попробовать еще раз наследование 
    // 3. разобраться с виртуальными методами и полиморфизмом соответственно

    protected int bonusNumberOfCount;
    protected float bonusRadius = 1f;
    protected float bonusCooldown = 1f;
    protected float bonusDamage = 1f;
    protected float bonusDuration = 1f;


    public static event Action<AbstractAbill> OnAbill;
    protected void OnEnable()
    {
   //     InGainedHolder.ShowStatsOfThisAbill += ShowStatsBegin;
    }
    protected virtual void Initialize()
    {
        OnAbill?.Invoke(this);
        CooldownReduction();
        DamageUpgrage();
        RadiusUpgrade();
        DurationUpgrade();
        CountUpgrade();
    }
    protected virtual void Reinitialize()
    {
        abilityLevel += 1;
        Initialize();
    }
    public void Activate()
    {
        Initialize();
        if (!IsActive)
        {
            IsActive = true;
            timer = cooldownTime / 2;
        }
    }
    public void OffAbill()
    {
        if (IsActive)
        {
            IsActive = false;
            enabled = false;
            gameObject.SetActive(false);
        }
       
    }
    public virtual void ActionTime(float deltaTime)
    {
        if (IsActive)
        {
            if (!isInWork)
            {
                timer -= deltaTime;

                // Проверка окончания времени ожидания
                if (timer <= 0 && CanActivate())
                {
                    ActionOfAbill(); // Сама работа способности
                    isInWork = true;

                    // Устанавливаем таймер на длительность действия или 0, для того чтобы проигнорировать длительную часть при ее отсутствии 
                    timer = Duration > 0 ? Duration : 0;
                }
            }
            else
            {
                // Уменьшаем таймер, пока способность активна
                timer -= deltaTime;
                DurationPartOfAbill(deltaTime);
                // Проверяем, завершилось ли действие способности
                if (timer <= 0)
                {
                    EndAbility(); // Завершение работы способности
                }
            }
        }
    }

    protected void EndAbility()
    {
        Offers();// метод для отключения если он вообще нужен в данной абилке
        isInWork = false; // Завершаем работу способности
        timer = cooldownTime; // Перезагрузка таймера на время перезарядки
    }
    protected virtual void Offers()
    {

    }

    protected virtual bool CanActivate()
    {
        return !isInWork; // Позволяет активировать только если "не в работе"
    }
    public void ChangeCooldown(float newCooldown)
    {
        float minimalize = newCooldown / cooldownTime;
        cooldownTime = newCooldown;
        // используем процент от достигнутого времени чтобы сохранить абилку
        // потом пригодится для "концентрации"
        if (IsActive)
        {
            timer *= minimalize;
        }
    }
    public void PercentChangeCooldown(float percent)
    {

        cooldownTime *= percent;
        // используем процент от достигнутого времени чтобы сохранить абилку
        // потом пригодится для "концентрации"
        if (IsActive)
        {
            timer *= percent;
        }
    }

    // надо найти примемение виртуальому методу и научиться с ним работать
    // по-хорошему нужно внести сюда интерфейс для общего управления методами понижения перезарядками но в целом, не обязательно.
    protected virtual void ActionOfAbill()
    {
        // по итогу сама логика абилки закидывается сюда. но встал вопрос: зачем вообще глобальный менеджер
        // если уже все стоит на событиях улучшений

    }
    protected virtual void DurationPartOfAbill(float deltaTime)
    {

    }

    public void DictionaryRecepient(Dictionary<TypeUpgrade,float> dictionary)
    {
        foreach (var upgrade in dictionary)
        {
            TypeUpgrade typeUpgrade = upgrade.Key; // Получаем ключ (тип улучшения)
            float value = upgrade.Value; // Получаем значение (коэффициент улучшения)

            ArtefactUpgrade(typeUpgrade, value); // Вызываем метод с двумя параметрами
        }
    }
    protected void ArtefactUpgrade(TypeUpgrade typeUpgrade, float value)
    {
        switch (typeUpgrade)
        {
            case  TypeUpgrade.damage:
                {
                    value = value / 100;
                    bonusDamage += value;
                    DamageUpgrage();
                    break;
                }
            case TypeUpgrade.count:
                {
                    
                    bonusNumberOfCount += (int)value;
                    CountUpgrade();
                    break;
                }
            case TypeUpgrade.radius:
                {
                    value = value / 100;
                    bonusRadius += value;
                    RadiusUpgrade();
                    break;
                }
            case TypeUpgrade.cooldown:
                {
                    value = value / 100;
                    bonusCooldown -= value;
                    CooldownReduction();
                    break;
                }
            case TypeUpgrade.duration:
                {
                    value = value / 100;
                    bonusDuration += value;
                    DurationUpgrade();
                    break;
                }
            case TypeUpgrade.special:
                {
                    SpecialUpgrade();
                    break;
                }
        }
        
    }
    public virtual void SpecialUpgrade()
    {

    }
    public abstract void CooldownReduction();

   
    protected abstract void DamageUpgrage();

    protected abstract void RadiusUpgrade();
    protected abstract void DurationUpgrade();
    protected abstract void CountUpgrade();
  


    public virtual void PercentReduction(float percent)
    {
        cooldownMultiplicator = 1 - percent;
        CooldownReduction();
    }
    public virtual AbstractAbill ShowStatsBegin(int ID)
  
    {
        if (ID == IDabill)
            {
                return this;
            }
        return null;
    }


    public virtual float[] ShowStats(int ID)
    {
        if (ID == IDabill)
        {
            float[] stats = new float[]
            {abilityLevel+1, bonusNumberOfCount,bonusRadius,cooldownTime,bonusDuration };
            return stats;
        }
        return null;
    }

    protected virtual void OnDisable()
    {
      //  InGainedHolder.ShowStatsOfThisAbill -= ShowStatsBegin;
    }

}
