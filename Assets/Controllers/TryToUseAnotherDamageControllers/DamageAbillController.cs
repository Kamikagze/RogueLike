using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageAbillController : MonoBehaviour
{
    [Header("GlobalStats")]
    [SerializeField] StatsHolder statsHolder;


    [Header("Billet")]
    [SerializeField] BulletPool bullet;
    public float BulletTotalDamage {  get; private set; }

    [Header("Flash")]
    [SerializeField] FlashAbill flash;
    public float FlashAbillTotalDamage { get; private set; }

    [Header ("StoneWalk")]
    [SerializeField] StoneWalk stoneWalk;
    public float StoneWalkTotalDamage { get; private set; }

    [Header("WanderingFlash")]
    [SerializeField] WanderingFlash wanderingFlash;
    public float WanderingFlashTotalDamage { get; private set; }

    [Header("FreezingField")]
    [SerializeField] FreezingField freezingField;
    public float FreezingFieldTotalDamage { get; private set; }

    [Header("Meteor")]
    [SerializeField] Meteor meteor;
    public float MeteorTotalDamage { get; private set; }

    [Header("Ice Orb")]

    [SerializeField] IceArrowPool iceOrb;
    public float IceArrowTotalDamage { get; private set; }
    
    [Header("MagicBolt")]

    [SerializeField] MagicBoltPool magicBolt;
    public float MagicBoltTotalDamage { get; private set; }

    [Header("LavaField")]

    [SerializeField] LavaField lavaField;
    public float LavaFieldTotalDamage { get; private set; }

    [Header("FireBall")]

    [SerializeField] FireBallPool fireBall;
    public float FireBallTotalDamage { get; private set; }

    [Header("LightningOrb")]

    [SerializeField] LightningOrb lightningOrb;
    public float LightningOrbTotalDamage { get; private set; }

    [Header("GravityOrb")]

    [SerializeField] Gravity gravityOrb;
    public float GravityOrbTotalDamage { get; private set; }

    [Header("ElectricOrbital")]

    [SerializeField] ElectricOrbital electricOrbital;
    public float ElectricOrbitalTotalDamage { get; private set; } 
    
    [Header("Laser")]

    [SerializeField] Laser laser;
    public float LaserTotalDamage { get; private set; }

    [Header("StonePicke")]

    [SerializeField] StonePickePool stonePicke;
    public float StonePickeTotalDamage { get; private set; } 
    
    [Header("FireBreath")]

    [SerializeField] FireBreath fireBreath;
    public float FireBreathTotalDamage { get; private set; }

    // Eruption
    
    public float EruptionTotalDamade {  get; private set; }

    [Header("WindSlash")]
    [SerializeField] WindSlashHolder windSlash;
    public float WindSlashTotalDamage {  get; private set; }

    private void OnEnable()
    
    {
        //после увеличения урона (менять всем и все)
        StatsHolder.DamageImproverIncreased += SetDamageAfterGlobalImprovement;
        //пуля
        BulletPool.BulletDamageActionEvent += SetBulletTotalDamage;
        //вспышка
        FlashAbill.FlashDamageIncrease += SetFlashTotalDamage;
        //каменная поступь
        StoneWalk.StoneWalkActionEvent += SetStoneWalkTotalDamage;
        //блуждающая вспышка
        WanderingFlash.WanderingFlashActionEvent += SetWanderingFlashTotalDamage;
        //останавливающий холод
        FreezingField.FreezingFieldActionEvent += SetFreezingFieldTotalDamage;
        //метеор
        Meteor.MeteorActionEvent += SetMeteorTotalDamage;
        // ледяной орб
        IceArrowPool.IceArrowActionEvent += SetIceArrowTotalDamage;
        //молния
        MagicBoltPool.MagicBoltActionEvent += SetMagicBoltTotalDamage;
        //лавовое поле
        LavaField.LavaFieldActionEvent += SetLavaFieldTotalDamage;
        //огненный шар
        FireBallPool.FireBallActionEvent += SetFireBallTotalDamage;
        // орб молний
        LightningOrb.LightningOrbActionEvent += SetLightningOrbTotalDamage;
        // гравитационная ловушка
        Gravity.GravityOrbActionEvent += SetGravityOrbTotalDamage;
        // орбитал
        ElectricOrbital.ElectricOrbitalActionEvent += SetElectricOrbitalTotalDamage;
        //лазер
        Laser.LaserActionEvent += SetLaserTotalDamage;
        // каменные шипы
        StonePickePool.StonePickeActionEvent += SetStonePickeTotalDamage;
        // огненное дыхание
        FireBreath.FireBreathDamageIncrease += SetFireBreathTotalDamage;
        // Eruption
        FullFillButtons.Eruption += SetEruptionTotalDamage;
        // WindSlash
        WindSlashHolder.WindSlashDamageIncrease += SetWindSlashTotalDamage;
    }

    

    private void SetDamageAfterGlobalImprovement()
    {
        SetBulletTotalDamage();
        SetFlashTotalDamage();
        SetStoneWalkTotalDamage();
        SetWanderingFlashTotalDamage();
        SetFreezingFieldTotalDamage();
        SetMeteorTotalDamage();
        SetIceArrowTotalDamage();
        SetMagicBoltTotalDamage();
        SetLavaFieldTotalDamage();
        SetFireBallTotalDamage();
        SetLightningOrbTotalDamage();
        SetGravityOrbTotalDamage();
        SetElectricOrbitalTotalDamage();
        SetLaserTotalDamage();
        SetStonePickeTotalDamage();
        SetFireBreathTotalDamage();
        SetEruptionTotalDamage(); // если будут изменения - надо двигать порядок
        SetWindSlashTotalDamage();
    }
    private void SetBulletTotalDamage()
    {
        BulletTotalDamage = bullet.BulletDamage * statsHolder.DamageImprover;
    }
    private void SetFlashTotalDamage() // урон вспышки
    {
        FlashAbillTotalDamage = flash.FlashDamage * statsHolder.DamageImprover;
    }
    private void SetStoneWalkTotalDamage()
    {
        StoneWalkTotalDamage = stoneWalk.StoneWalkDamage * statsHolder.DamageImprover;
    }
    private void SetWanderingFlashTotalDamage()
    {
        WanderingFlashTotalDamage = wanderingFlash.WanderingFlashDamage * statsHolder.DamageImprover;
    }
    private void SetFreezingFieldTotalDamage()
    {
        FreezingFieldTotalDamage = freezingField.FreezingFieldDamage * statsHolder.DamageImprover;
    }
    private void SetMeteorTotalDamage()
    {
        MeteorTotalDamage = meteor.MeteorDamage * statsHolder.DamageImprover;
    }
    private void SetIceArrowTotalDamage()
    {
        IceArrowTotalDamage = iceOrb.IceArrowDamage * statsHolder.DamageImprover;
    }
    private void SetMagicBoltTotalDamage()
    {
        MagicBoltTotalDamage = magicBolt.MagicBoltDamage * statsHolder.DamageImprover;
    }
    private void SetLavaFieldTotalDamage()
    {
        LavaFieldTotalDamage = lavaField.LavaFieldDamage * statsHolder.DamageImprover;
    }
    private void SetFireBallTotalDamage()
    {
        FireBallTotalDamage = fireBall.FireBallDamage * statsHolder.DamageImprover;
    }
    private void SetLightningOrbTotalDamage()
    {
        LightningOrbTotalDamage = lightningOrb.LightningOrbDamage * statsHolder.DamageImprover;
    }
    private void SetGravityOrbTotalDamage()
    {
        GravityOrbTotalDamage = gravityOrb.GravityOrbDamage * statsHolder.DamageImprover;
    }
    private void SetElectricOrbitalTotalDamage()
    {
        ElectricOrbitalTotalDamage = electricOrbital.ElectricOrbitalDamage * statsHolder.DamageImprover;
    }
    private void SetLaserTotalDamage()
    {
        LaserTotalDamage = laser.LaserDamage * statsHolder.DamageImprover;
    }
    private void SetStonePickeTotalDamage()
    {
        StonePickeTotalDamage = stonePicke.StonePickeDamage * statsHolder.DamageImprover;
    }
    private void SetFireBreathTotalDamage()
    {
        FireBreathTotalDamage = fireBreath.FireBreathDamage * statsHolder.DamageImprover;
    }

    private void SetEruptionTotalDamage()
    {
       EruptionTotalDamade = (StoneWalkTotalDamage + StonePickeTotalDamage) * statsHolder.DamageImprover;
    }
    private void SetWindSlashTotalDamage()
    {
        WindSlashTotalDamage = windSlash.WindSlashDamage * statsHolder.DamageImprover;
    }
    public float showDamageStats(int i)
    {
        switch (i)
        {
            case 0:
                return BulletTotalDamage;
                case 1:
                return FireBallTotalDamage;
                case 2:
                return GravityOrbTotalDamage;
                case 3:
                return IceArrowTotalDamage;
                case 4:
                return LightningOrbTotalDamage;
                case 5:
                return 0; 
                case 6:
                return 0;
                case 7:
                return 0;
                case 8:
                return ElectricOrbitalTotalDamage;
                case 9:
                return FireBreathTotalDamage;
                case 10:
                return FlashAbillTotalDamage;
                case 11:
                return FreezingFieldTotalDamage;
                case 12:
                return LaserTotalDamage;
                case 13:
                return LavaFieldTotalDamage;
                case 14:
                return MagicBoltTotalDamage;
                case 15:
                return 0;
                case 16:
                return MeteorTotalDamage;
                case 17:
                return StonePickeTotalDamage;
                case 18:
                return StoneWalkTotalDamage;
                case 19:
                return WanderingFlashTotalDamage;
                case 20:
                return WindSlashTotalDamage;

                default: return 0;

        }
    }

    private void OnDisable()
    {
        StatsHolder.DamageImproverIncreased -= SetDamageAfterGlobalImprovement;
        //пуля
        BulletPool.BulletDamageActionEvent -= SetBulletTotalDamage;
        //вспышка
        FlashAbill.FlashDamageIncrease -= SetFlashTotalDamage;
        //каменная поступь
        StoneWalk.StoneWalkActionEvent -= SetStoneWalkTotalDamage;
        //блуждающая вспышка
        WanderingFlash.WanderingFlashActionEvent -= SetWanderingFlashTotalDamage;
        //останавливающий холод
        FreezingField.FreezingFieldActionEvent -= SetFreezingFieldTotalDamage;
        //метеор
        Meteor.MeteorActionEvent -= SetMeteorTotalDamage;
        // ледяной орб
        IceArrowPool.IceArrowActionEvent -= SetIceArrowTotalDamage;
        //молния
        MagicBoltPool.MagicBoltActionEvent -= SetMagicBoltTotalDamage;
        //лавовое поле
        LavaField.LavaFieldActionEvent -= SetLavaFieldTotalDamage;
        //огненный шар
        FireBallPool.FireBallActionEvent -= SetFireBallTotalDamage;
        // орб молний
        LightningOrb.LightningOrbActionEvent -= SetLightningOrbTotalDamage;
        // гравитационная ловушка
        Gravity.GravityOrbActionEvent -= SetGravityOrbTotalDamage;
        // орбитал
        ElectricOrbital.ElectricOrbitalActionEvent -= SetElectricOrbitalTotalDamage;
        //лазер
        Laser.LaserActionEvent -= SetLaserTotalDamage;
        // каменные шипы
        StonePickePool.StonePickeActionEvent -= SetStonePickeTotalDamage;
        // огненное дыхание
        FireBreath.FireBreathDamageIncrease -= SetFireBreathTotalDamage;
        // Эррапшн
        FullFillButtons.Eruption -= SetEruptionTotalDamage;
        //
        WindSlashHolder.WindSlashDamageIncrease -= SetWindSlashTotalDamage;
    }
}
