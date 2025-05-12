using UnityEngine;
using System;


public class LavaField : AbstractAbill
{

    // �������� ����� ����, ������������ ������
    [SerializeField] StatsHolder statsHolder;
    [SerializeField] Transform lavaScale;
    [SerializeField] Transform player;
    private float minRadius = 1f;
    private float maxRadius = 2.5f;
    [SerializeField] private CircleCollider2D lavaFieldCollider;
    [SerializeField] private LavaFieldScriptableObject[] lavaFieldScriptableObjects;
    [SerializeField] private SpriteRenderer lavaFieldSpriteRenderer;
    [SerializeField] private LavaField[] lavaFields;
    [SerializeField] private Animator anim;

    
    public float lavaFieldCooldown;

    public int lavaFieldCount = 1;
 

    // ��� ����������� �����������
 

    public float LavaFieldDamage { get; private set; } // ���� �������� �� ���� ������� ��� ���������� �� ����� ������

    public static event Action LavaFieldActionEvent;
    private void Start()
    {
       
        StatsHolder.DamageImproverIncreased += DamageUpgrage;
        StatsHolder.RadiusIncreased += RadiusUpgrade;
        LavaFieldScriptableObject.LavaFieldUpgradeEvent += Reinitialize;
        
        Activate();
    }


  

    private void FindPlaceWhereLavaFieldStarts(Vector2 vector)
    {
        transform.position = vector;
    }
    private Vector2 GetRandomPointInCircle()
    {
        // �������� ��������� ����
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        // �������� ��������� ������
        float randomRadius = UnityEngine.Random.Range(minRadius, maxRadius);

        // ����������� � ���������� x � y ��� 2D
        float x = player.position.x + randomRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float y = player.position.y + randomRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        // ���������� ��������� ����� � ���� �������
        return new Vector2(x, y);
    }
   
    protected override void ActionOfAbill()
    {
        FindPlaceWhereLavaFieldStarts(GetRandomPointInCircle());
        anim.SetBool("Action", true);
    }
   
    protected override void Offers()
    {
        anim.SetBool("Action", false);
    }
    public override void CooldownReduction()
 
    {
        lavaFieldCooldown = lavaFieldScriptableObjects[abilityLevel].lavaFieldCooldown
            * statsHolder.CooldownReduction * bonusCooldown;
        ChangeCooldown(lavaFieldCooldown);
    }


   
    private void InitializeMoreLavaField(int i)
    {
        lavaFields[i].abilityLevel = this.abilityLevel;
        lavaFields[i].lavaFieldCount = this.lavaFieldCount;
        lavaFields[i].lavaFieldCooldown = this.lavaFieldCooldown;
        lavaFields[i].LavaFieldDamage = this.LavaFieldDamage;

        // �������������, ���� � ��� ���� �����-���� ���������� ������� ��� ������ ���������, ������� ����� ����������������
        lavaFields[i].lavaFieldScriptableObjects = this.lavaFieldScriptableObjects;

        lavaFields[i].Initialize(); // �������� �������������, ���� � ��� ���� ������������� �����������
    }
 
    protected override void OnDisable()
    {
        base.OnDisable();
        StatsHolder.DamageImproverIncreased -= DamageUpgrage;
        StatsHolder.RadiusIncreased -= RadiusUpgrade;
        LavaFieldScriptableObject.LavaFieldUpgradeEvent -= Reinitialize;
    }

    protected override void DamageUpgrage()
    {
        LavaFieldDamage = lavaFieldScriptableObjects[abilityLevel].lavaFieldDamage * bonusDamage;
        LavaFieldActionEvent?.Invoke();
    }

    protected override void RadiusUpgrade()
    {
        lavaScale.localScale = new Vector2((lavaFieldScriptableObjects[abilityLevel].lavaFieldRadius * statsHolder.Radius * bonusRadius),
           (lavaFieldScriptableObjects[abilityLevel].lavaFieldRadius * statsHolder.Radius * bonusRadius));
    }

    protected override void DurationUpgrade()
    {
        Duration = lavaFieldScriptableObjects[abilityLevel].lavaFieldDuration * bonusDuration;
    }

    protected override void CountUpgrade()
    {
        if (lavaFieldCount != lavaFieldScriptableObjects[abilityLevel].lavaFieldCount
            + bonusNumberOfCount && lavaFields != null)
        {
            lavaFieldCount = lavaFieldScriptableObjects[abilityLevel].lavaFieldCount + bonusNumberOfCount;
            for (int i = 0; i < lavaFields.Length; i++)
            {
                if (lavaFieldCount == i + 2)
                {
                    lavaFields[i].gameObject.SetActive(true);
                    InitializeMoreLavaField(i);
                }

            }
        }
    }
}


