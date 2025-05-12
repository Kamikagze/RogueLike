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

    protected float cooldownMultiplicator = 1; // �������������� ��� �����������

    protected bool isInWork; // ���� ��� ��������� ������������� �������

    protected virtual float Duration { get; set; }   // ������������ ������ ����������� (�� ��������� 0, ���� �� ��������������)
                                                     // ������ � ���, ��� ������ ������ �� ����� ����������� � ���� ����� �� ����� ����������.


    // ������ ��������� ���� ���������� �� ���� ������� � �������� ��������� ����� 1 �����
    // ������ � ��� ����� 1. �������� �������� ����������� ����� ����� ����������� � �� ����� ��������� �������� 
    // 2. ��������� �� ����� ����� ���������� �������
    // ����������� ��� ��� ������������ 
    // 3. ����������� � ������������ �������� � ������������� ��������������

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

                // �������� ��������� ������� ��������
                if (timer <= 0 && CanActivate())
                {
                    ActionOfAbill(); // ���� ������ �����������
                    isInWork = true;

                    // ������������� ������ �� ������������ �������� ��� 0, ��� ���� ����� ��������������� ���������� ����� ��� �� ���������� 
                    timer = Duration > 0 ? Duration : 0;
                }
            }
            else
            {
                // ��������� ������, ���� ����������� �������
                timer -= deltaTime;
                DurationPartOfAbill(deltaTime);
                // ���������, ����������� �� �������� �����������
                if (timer <= 0)
                {
                    EndAbility(); // ���������� ������ �����������
                }
            }
        }
    }

    protected void EndAbility()
    {
        Offers();// ����� ��� ���������� ���� �� ������ ����� � ������ ������
        isInWork = false; // ��������� ������ �����������
        timer = cooldownTime; // ������������ ������� �� ����� �����������
    }
    protected virtual void Offers()
    {

    }

    protected virtual bool CanActivate()
    {
        return !isInWork; // ��������� ������������ ������ ���� "�� � ������"
    }
    public void ChangeCooldown(float newCooldown)
    {
        float minimalize = newCooldown / cooldownTime;
        cooldownTime = newCooldown;
        // ���������� ������� �� ������������ ������� ����� ��������� ������
        // ����� ���������� ��� "������������"
        if (IsActive)
        {
            timer *= minimalize;
        }
    }
    public void PercentChangeCooldown(float percent)
    {

        cooldownTime *= percent;
        // ���������� ������� �� ������������ ������� ����� ��������� ������
        // ����� ���������� ��� "������������"
        if (IsActive)
        {
            timer *= percent;
        }
    }

    // ���� ����� ���������� ����������� ������ � ��������� � ��� ��������
    // ��-�������� ����� ������ ���� ��������� ��� ������ ���������� �������� ��������� ������������� �� � �����, �� �����������.
    protected virtual void ActionOfAbill()
    {
        // �� ����� ���� ������ ������ ������������ ����. �� ����� ������: ����� ������ ���������� ��������
        // ���� ��� ��� ����� �� �������� ���������

    }
    protected virtual void DurationPartOfAbill(float deltaTime)
    {

    }

    public void DictionaryRecepient(Dictionary<TypeUpgrade,float> dictionary)
    {
        foreach (var upgrade in dictionary)
        {
            TypeUpgrade typeUpgrade = upgrade.Key; // �������� ���� (��� ���������)
            float value = upgrade.Value; // �������� �������� (����������� ���������)

            ArtefactUpgrade(typeUpgrade, value); // �������� ����� � ����� �����������
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
