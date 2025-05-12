using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsHolder : MonoBehaviour
{

    public float Armor { get; private set; } = 0;
    public static event Action ArmorIncreased;
    public float Speed { get; private set; } = 2;
    public static event Action SpeedIncreased;
    public float EXPImprove { get; private set; } = 1;
    public static event Action EXPIncreased;
    public float Radius { get; private set; } = 1;
    public static event Action RadiusIncreased;
    public float CooldownReduction { get; private set; } = 1;
    public static event Action CooldownReductionIncreased;
    public float DamageImprover { get; private set; } = 1;
    public static event Action DamageImproverIncreased;

    private void OnEnable()
    {
        DamageUpgrade.DamageUpgradeEvent += DamageImprove;
        ArmorUpgrade.ArmorUpgradeEvent += ArmorImprove;
        SpeedUpgrade.SpeedUpgradeEvent += SpeedImprove;
        EXPUpgrade.EXPUpgradeEvent += EXPImprover;
        RadiusUpgrade.RadiusUpgradeEvent += RadiusImprove;
        CooldownUpgrade.CooldownUpgradeEvent += CooldownReductionImprove;

        ArtifactScriptableObject.ArtifactUpgradeOfStats += UpgradeSystem;
    }


    private void ArmorImprove(float armorImprove)
    {
        Armor += armorImprove;
        ArmorIncreased?.Invoke();
    }

    private void SpeedImprove(float speedImprove)
    {
        speedImprove = speedImprove / 100;
        Speed += speedImprove;
        SpeedIncreased?.Invoke();
    }

    private void EXPImprover(float expImprove)
    {
        EXPImprove += expImprove;
        EXPIncreased?.Invoke();
    }

    private void RadiusImprove(float radiusImprove)
    {
        radiusImprove = radiusImprove / 100;
        Radius += radiusImprove;
        RadiusIncreased?.Invoke();
    }
    private void CooldownReductionImprove(float cooldownReduction)
    {
        cooldownReduction = cooldownReduction / 100;
        CooldownReduction -= cooldownReduction;
        CooldownReductionIncreased?.Invoke();
    }
    public void DamageImprove(float damageImprove)
    {
        damageImprove = damageImprove / 100;
        DamageImprover += damageImprove;
        DamageImproverIncreased?.Invoke();
    }

    private void UpgradeSystem(Dictionary <StatsAspects, float> statsImprovements)
    {
        foreach (var upgrades in statsImprovements)
        {
            StatsAspects statsAspects = upgrades.Key;
            float value = upgrades.Value;

            ImproveType(statsAspects, value);
        }
    }
    private void ImproveType(StatsAspects statsAspects, float value)
    {

        switch (statsAspects)
        {
            case StatsAspects.armor:
                ArmorImprove(value); break;

            case StatsAspects.speed:
                SpeedImprove(value); break;

            case StatsAspects.exp:
                EXPImprover(value); break;

            case StatsAspects.radius:
                RadiusImprove(value); break;

            case StatsAspects.cooldown:
                CooldownReductionImprove(value); break;

            case StatsAspects.damage:
                DamageImprove(value); break;




        }
    }
    private void OnDisable()
    {
        DamageUpgrade.DamageUpgradeEvent -= DamageImprove;
        ArmorUpgrade.ArmorUpgradeEvent -= ArmorImprove;
        SpeedUpgrade.SpeedUpgradeEvent -= SpeedImprove;
        EXPUpgrade.EXPUpgradeEvent -= EXPImprover;
        RadiusUpgrade.RadiusUpgradeEvent -= RadiusImprove;
        CooldownUpgrade.CooldownUpgradeEvent -= CooldownReductionImprove;
        ArtifactScriptableObject.ArtifactUpgradeOfStats -= UpgradeSystem;
    }
}
