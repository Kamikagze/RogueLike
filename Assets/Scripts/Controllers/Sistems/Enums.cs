using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
}
public enum RarityType
{
    
    Common,
    Uncommon,
    Rare,
    Legendary
}
public enum TypeUpgrade
{
    damage,
    count,
    radius,
    cooldown,
    duration,
    special
}
public enum AspectOfUpgrade
{
    ability,
    health,
    stats,
    enemy,
    None
}
public enum EnemyAspects
{
     freeze,
     health,
     EXP,
     damage
}
public enum HealthAspects
{
    maxHealth,
    regeneration,
    heel,
    lifes
}
public enum StatsAspects
{
    armor,
    speed,
    exp,
    radius,
    cooldown,
    damage
}
public enum AbilityKeys
{
    cataclysm,
    concentration,
    fireBall,
    fireBreath,
    flash,
    freezingField,
    magicBolt,
    magicShield,
    meteor,
    stoneWalk,
    windSlash,
    erruption,
    laser,
    lightningOrb,
    iceOrb,
    wanderingFlash,
    lavaFields,
    bullet,
    gravityOrb,
    electricOrb,
    stonePicke,
}