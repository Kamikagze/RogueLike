using JetBrains.Annotations;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class FullFillButtons : MonoBehaviour
{

    [Header("RequredComponents")]
    [SerializeField] AllAbilllHolder allabilllHolder;
    [SerializeField] ProcedureOfLevelingUp procedureOfLevelingUp;
    [SerializeField] GainedResources gainedResources;
    

    [Header("FirstPlate")]
    [SerializeField] Image fImage;
    [SerializeField] TextMeshProUGUI fNameAbill;
    [SerializeField] TextMeshProUGUI fNoteAbill;
    [SerializeField] Button fButtonAbill;
    [SerializeField] Image[] fBorders;

    [Header("SecondPlate")]
    [SerializeField] Image sImage;
    [SerializeField] TextMeshProUGUI sNameAbill;
    [SerializeField] TextMeshProUGUI sNoteAbill;
    [SerializeField] Button sButtonAbill;
    [SerializeField] Image[] sBorders;

    [Header("ThirdPlate")]
    [SerializeField] Image tImage;
    [SerializeField] TextMeshProUGUI tNameAbill;
    [SerializeField] TextMeshProUGUI tNoteAbill;
    [SerializeField] Button tButtonAbill;
    [SerializeField] Image[] tBorders;

    [Header("ColorPlate")]
    [SerializeField] private Color borderColor = new Color(1, 0, 1);

    [Header ("Rerol")]
    [SerializeField] Button fourthButtonAbill;

    public static event Action<int> UpgradeBullet;

    public static event Action<int> UpgradeIceArrow;

    public static event Action<int> UpgradeMagicBolt;

    public static event Action<int> UpgradeFireBall;

    public static event Action<int> UpgradeStonePicke;

   

    public static event Action Eruption;

    public static event Action StarFall;

    public static event Action IceHell;

    public static event Action RoamingLight;
    private void Start()
    {
      
        FillButtonsWithAbilities();
        ProcedureOfLevelingUp.ChooseAbil += Reroll;
    }

    private void FillButtonsWithAbilities()
    {
        StartCoroutine(allabilllHolder.ChooseAnAbilityLevelCoroutine(chosenAbilities =>
        {
            FillPanel(fImage, fNameAbill, fNoteAbill, chosenAbilities[0], fButtonAbill, fBorders);
            FillPanel(sImage, sNameAbill, sNoteAbill, chosenAbilities[1], sButtonAbill, sBorders);
            FillPanel(tImage, tNameAbill, tNoteAbill, chosenAbilities[2], tButtonAbill, tBorders);
        }));
    }

    private void FillPanel(Image image, TextMeshProUGUI nameText,
        TextMeshProUGUI noteText, ScriptableObject ability, Button button, Image[] borders )
    {
        if (ability != null)
        {
            // Определяем тип ScriptableObject и заполняем соответствующие поля
            if (ability is DamageUpgrade damageUpgrade)
            {
                image.sprite = damageUpgrade.image;
                nameText.text = damageUpgrade.damageName;
                noteText.text = damageUpgrade.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners(); // Удаляем предыдущие слушатели
                button.onClick.AddListener(() =>
                {
                    damageUpgrade.OnDamageUpgrade();
                    procedureOfLevelingUp.Activate();
                    allabilllHolder.allLevels["Damage"] += 1;
                    gainedResources.AfterUpgrade(damageUpgrade, allabilllHolder.allLevels["Damage"]);
                }); // Добавляем новый слушатель

            }
            else if (ability is EXPUpgrade expUpgrade)
            {
                image.sprite = expUpgrade.image; // Тоже предполагаем, что здесь изображение
                nameText.text = expUpgrade.expName;
                noteText.text = expUpgrade.note;
                button.onClick.RemoveAllListeners(); // Удаляем предыдущие слушатели
                button.onClick.AddListener(() =>
                {
                    expUpgrade.OnEXPUpgrade();
                    procedureOfLevelingUp.Activate();
                    allabilllHolder.expLevel += 1;
                    allabilllHolder.allLevels["EXP"] += 1;
                    gainedResources.AfterUpgrade(expUpgrade, allabilllHolder.allLevels["EXP"]);
                });
            }
            else if (ability is RadiusUpgrade radiusUpgrade)
            {
                image.sprite = radiusUpgrade.image; 
                nameText.text = radiusUpgrade.radiusName;
                noteText.text = radiusUpgrade.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners(); // Удаляем предыдущие слушатели
                button.onClick.AddListener(() =>
                {
                    radiusUpgrade.OnRadiusUpgrade();
                    procedureOfLevelingUp.Activate();
                    allabilllHolder.radiusLevel += 1;
                    allabilllHolder.allLevels["Radius"] += 1;
                    gainedResources.AfterUpgrade(radiusUpgrade, allabilllHolder.allLevels["Radius"]);
                });

            }
            else if (ability is SpeedUpgrade speedUpgrade)
            {
                image.sprite = speedUpgrade.image;
                nameText.text = speedUpgrade.speedName;
                noteText.text = speedUpgrade.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners(); // Удаляем предыдущие слушатели
                button.onClick.AddListener(() =>
                {
                    speedUpgrade.OnSpeedUpgrade();
                    procedureOfLevelingUp.Activate();
                    allabilllHolder.speedLevel += 1;
                    allabilllHolder.allLevels["Speed"] += 1;
                    gainedResources.AfterUpgrade(speedUpgrade, allabilllHolder.allLevels["Speed"]);
                });
            }

            else if (ability is CooldownUpgrade cooldownUpgrade)
            {
                image.sprite = cooldownUpgrade.image;
                nameText.text = cooldownUpgrade.cooldownName;
                noteText.text = cooldownUpgrade.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners(); // Удаляем предыдущие слушатели
                button.onClick.AddListener(() =>
                {
                    cooldownUpgrade.OnCooldownUpgrade();
                    procedureOfLevelingUp.Activate();
                    allabilllHolder.cooldownLevel += 1;
                    allabilllHolder.allLevels["Cooldown"] += 1;
                    gainedResources.AfterUpgrade(cooldownUpgrade, allabilllHolder.allLevels["Cooldown"]);
                });
            }
            else if (ability is ArmorUpgrade armorUpgrade)
            {
                image.sprite = armorUpgrade.image;
                nameText.text = armorUpgrade.armorName;
                noteText.text = armorUpgrade.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    armorUpgrade.OnArmorUpgrade();
                    procedureOfLevelingUp.Activate();
                    allabilllHolder.armorLevel += 1;
                    allabilllHolder.allLevels["Armor"] += 1;
                    gainedResources.AfterUpgrade(armorUpgrade, allabilllHolder.allLevels["Armor"]);
                });
            }
            else if (ability is BulletScriptableObject bullet)
            {
                image.sprite = bullet.image;
                nameText.text = bullet.bulletName;
                noteText.text = bullet.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {

                    procedureOfLevelingUp.Activate();
                    allabilllHolder.bulletLevel += 1;
                    UpgradeBullet?.Invoke(allabilllHolder.bulletLevel);
                    allabilllHolder.allLevels["Bullet"] += 1;
                    gainedResources.AfterUpgrade(bullet, allabilllHolder.allLevels["Bullet"]);
                });
            }
            else if (ability is FlashScriptableObject flash)
            {
                image.sprite = flash.image;
                nameText.text = flash.flashName;
                noteText.text = flash.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.flashLevel == 0) { allabilllHolder.ObjectsStarter(0); }
                    flash.OnFlashUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.flashLevel += 1;
                    allabilllHolder.allLevels["Flash"] += 1;
                    gainedResources.AfterUpgrade(flash, allabilllHolder.allLevels["Flash"]);
                });
            }
            else if (ability is StoneWalkScriptableObject stoneWalk)
            {
                image.sprite = stoneWalk.image;
                nameText.text = stoneWalk.stoneWalkName;
                noteText.text = stoneWalk.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.stoneWalkLevel == 0) { allabilllHolder.ObjectsStarter(1); }
                    stoneWalk.OnStoneWalkUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.stoneWalkLevel += 1;
                    allabilllHolder.allLevels["StoneWalk"] += 1;
                    gainedResources.AfterUpgrade(stoneWalk, allabilllHolder.allLevels["StoneWalk"]);

                });
            }
            else if (ability is WanderingFlashScriptableObject wanderingFlash)
            {
                image.sprite = wanderingFlash.image;
                nameText.text = wanderingFlash.wanderingFlashName;
                noteText.text = wanderingFlash.note;

                int currentLevel = allabilllHolder.wanderingFlashLevel;
                BorderColorChanger(7, currentLevel, borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.wanderingFlashLevel == 0) { allabilllHolder.ObjectsStarter(2); }
                    wanderingFlash.OnWanderingFlashUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.wanderingFlashLevel += 1;
                    allabilllHolder.allLevels["WanderingFlash"] += 1;
                    if (allabilllHolder.wanderingFlashLevel == 8) RoamingLight?.Invoke();
                    gainedResources.AfterUpgrade(wanderingFlash, allabilllHolder.allLevels["WanderingFlash"]);
                });
            }
            else if (ability is FreezingFieldScriptableObject freezingField)
            {
                image.sprite = freezingField.image;
                nameText.text = freezingField.freezingFieldName;
                noteText.text = freezingField.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.freezingFieldLevel == 0) { allabilllHolder.ObjectsStarter(3); }
                    freezingField.OnFreezingFieldUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.freezingFieldLevel += 1;
                    allabilllHolder.allLevels["FreezingField"] += 1;
                    gainedResources.AfterUpgrade(freezingField, allabilllHolder.allLevels["FreezingField"]);
                });
            }
            else if (ability is MeteorScriptableObject meteor)
            {
                image.sprite = meteor.image;
                nameText.text = meteor.meteorName;
                noteText.text = meteor.note;

                int currentLevel = allabilllHolder.meteorLevel;
                BorderColorChanger(8, currentLevel, borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.meteorLevel == 0) { allabilllHolder.ObjectsStarter(4); }
                    meteor.OnMeteorUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.meteorLevel += 1;
                    allabilllHolder.allLevels["Meteor"] += 1;
                    if (allabilllHolder.meteorLevel == 8) StarFall?.Invoke();
                    gainedResources.AfterUpgrade(meteor, allabilllHolder.allLevels["Meteor"]);
                });
            }
            else if (ability is IceArrowScriptableObject iceArrow)
            {
                image.sprite = iceArrow.image;
                nameText.text = iceArrow.iceArrowName;
                noteText.text = iceArrow.note;

                int currentLevel = allabilllHolder.iceArrowLevel;
                BorderColorChanger(8, currentLevel, borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.iceArrowLevel == 0) { allabilllHolder.ObjectsStarter(5); }
                    procedureOfLevelingUp.Activate();
                    UpgradeIceArrow?.Invoke(allabilllHolder.iceArrowLevel);
                    allabilllHolder.iceArrowLevel += 1;

                    allabilllHolder.allLevels["IceArrow"] += 1;
                    if (allabilllHolder.iceArrowLevel == 8) IceHell?.Invoke();
                    gainedResources.AfterUpgrade(iceArrow, allabilllHolder.allLevels["IceArrow"]);
                });
            }
            else if (ability is MagicBoltScriptableObject magicBolt)
            {
                image.sprite = magicBolt.image;
                nameText.text = magicBolt.magicBoltName;
                noteText.text = magicBolt.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.magicBoltLevel == 0) { allabilllHolder.ObjectsStarter(6); }
                    procedureOfLevelingUp.Activate();
                    UpgradeMagicBolt?.Invoke(allabilllHolder.magicBoltLevel);
                    allabilllHolder.magicBoltLevel += 1;

                    allabilllHolder.allLevels["MagicBolt"] += 1;
                    gainedResources.AfterUpgrade(magicBolt, allabilllHolder.allLevels["MagicBolt"]);
                });
            }
            else if (ability is LavaFieldScriptableObject lavaField)
            {
                image.sprite = lavaField.image;
                nameText.text = lavaField.lavaFieldName;
                noteText.text = lavaField.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.lavaFieldLevel == 0) { allabilllHolder.ObjectsStarter(7); }
                    lavaField.OnLavaFieldUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.lavaFieldLevel += 1;
                    allabilllHolder.allLevels["LavaField"] += 1;
                    gainedResources.AfterUpgrade(lavaField, allabilllHolder.allLevels["LavaField"]);
                });
            }
            else if (ability is FireBallScriptableObject fireBall)
            {
                image.sprite = fireBall.image;
                nameText.text = fireBall.fireBallName;
                noteText.text = fireBall.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.fireBallLevel == 0) { allabilllHolder.ObjectsStarter(8); }
                    procedureOfLevelingUp.Activate();
                    UpgradeFireBall?.Invoke(allabilllHolder.fireBallLevel);
                    allabilllHolder.fireBallLevel += 1;

                    allabilllHolder.allLevels["FireBall"] += 1;
                    gainedResources.AfterUpgrade(fireBall, allabilllHolder.allLevels["FireBall"]);
                });
            }
            else if (ability is LightningOrbScriptableObjects lightningOrb)
            {
                image.sprite = lightningOrb.image;
                nameText.text = lightningOrb.lightningOrbName;
                noteText.text = lightningOrb.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.lightningOrbLevel == 0) { allabilllHolder.ObjectsStarter(9); }
                    lightningOrb.OnLightningOrbUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.lightningOrbLevel += 1;
                    allabilllHolder.allLevels["LightningOrb"] += 1;
                    gainedResources.AfterUpgrade(lightningOrb, allabilllHolder.allLevels["LightningOrb"]);
                });
            }
            else if (ability is GravityOrbScriptableObjects gravityOrb)
            {
                image.sprite = gravityOrb.image;
                nameText.text = gravityOrb.gravityOrbName;
                noteText.text = gravityOrb.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.gravityLevel == 0) { allabilllHolder.ObjectsStarter(10); }
                    gravityOrb.OnGravityOrbUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.gravityLevel += 1;
                    allabilllHolder.allLevels["GravityOrb"] += 1;
                    gainedResources.AfterUpgrade(gravityOrb, allabilllHolder.allLevels["GravityOrb"]);
                });
            }
            else if (ability is ElectricOrbitalScriptableObject electricOrbital)
            {
                image.sprite = electricOrbital.image;
                nameText.text = electricOrbital.electricOrbitalName;
                noteText.text = electricOrbital.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.electricOrbitalsLevel == 0) { allabilllHolder.ObjectsStarter(11); }
                    electricOrbital.OnElectricOrbitalUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.electricOrbitalsLevel += 1;
                    allabilllHolder.allLevels["ElectricOrbital"] += 1;
                    gainedResources.AfterUpgrade(electricOrbital, allabilllHolder.allLevels["ElectricOrbital"]);
                });
            }
            else if (ability is LaserScriptableObject laser)
            {
                image.sprite = laser.image;
                nameText.text = laser.laserName;
                noteText.text = laser.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.laserLevel == 0) { allabilllHolder.ObjectsStarter(12); }
                    laser.OnLaserUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.laserLevel += 1;
                    allabilllHolder.allLevels["Laser"] += 1;
                    gainedResources.AfterUpgrade(laser, allabilllHolder.allLevels["Laser"]);
                });
            }
            else if (ability is StonePickeScriptableObject stonePicke)
            {
                image.sprite = stonePicke.image;
                nameText.text = stonePicke.stonePickeName;
                noteText.text = stonePicke.note;

                int currentLevel = allabilllHolder.stonePickeLevel;
                BorderColorChanger(8, currentLevel, borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.stonePickeLevel == 0) { allabilllHolder.ObjectsStarter(13); }
                    procedureOfLevelingUp.Activate();
                    UpgradeStonePicke?.Invoke(allabilllHolder.stonePickeLevel);
                    allabilllHolder.stonePickeLevel += 1;

                    allabilllHolder.allLevels["StonePicke"] += 1;
                    if (allabilllHolder.stonePickeLevel == 8) Eruption.Invoke();
                    gainedResources.AfterUpgrade(stonePicke, allabilllHolder.allLevels["StonePicke"]);
                });
            }
            else if (ability is MagicShieldScriptableObject magicShield)
            {
                image.sprite = magicShield.image;
                nameText.text = magicShield.shieldName;
                noteText.text = magicShield.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.magicShieldLevel == 0) { allabilllHolder.ObjectsStarter(14); }
                    magicShield.OnShieldUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.magicShieldLevel += 1;
                    allabilllHolder.allLevels["MagicShield"] += 1;
                    gainedResources.AfterUpgrade(magicShield, allabilllHolder.allLevels["MagicShield"]);
                });
            }
            else if (ability is FireBreathScriptableObject fireBreath)
            {
                image.sprite = fireBreath.image;
                nameText.text = fireBreath.fireBreathName;
                noteText.text = fireBreath.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.fireBreathLevel == 0) { allabilllHolder.ObjectsStarter(15); }
                    fireBreath.OnFireBreathUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.fireBreathLevel += 1;
                    allabilllHolder.allLevels["FireBreath"] += 1;
                    gainedResources.AfterUpgrade(fireBreath, allabilllHolder.allLevels["FireBreath"]);
                });
            }
            else if (ability is CataclysmScriptableObject cataclysm)
            {
                image.sprite = cataclysm.image;
                nameText.text = cataclysm.cataclysmName;
                noteText.text = cataclysm.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.cataclysmLevel == 0) { allabilllHolder.ObjectsStarter(16); }
                    cataclysm.OnCataclysmUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.cataclysmLevel += 1;
                    allabilllHolder.allLevels["Cataclysm"] += 1;
                    gainedResources.AfterUpgrade(cataclysm, allabilllHolder.allLevels["Cataclysm"]);
                });
            }
            else if (ability is WindSlashScriptableObject windSlash)
            {
                image.sprite = windSlash.image;
                nameText.text = windSlash.windSlashName;
                noteText.text = windSlash.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.windSlashLevel == 0) { allabilllHolder.ObjectsStarter(17); }
                    windSlash.OnWindSlashUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.windSlashLevel += 1;
                    allabilllHolder.allLevels["WindSlash"] += 1;
                    gainedResources.AfterUpgrade(windSlash, allabilllHolder.allLevels["WindSlash"]);
                });
            }
            else if (ability is ConcentrationScriptableObject concentration)
            {
                image.sprite = concentration.image;
                nameText.text = concentration.concentrationName;
                noteText.text = concentration.note;

                BorderColorChanger(borders);

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (allabilllHolder.concentrationLevel == 0) { allabilllHolder.ObjectsStarter(18); }
                    concentration.OnConcentrationUpgrade();
                    procedureOfLevelingUp.Activate();

                    allabilllHolder.concentrationLevel += 1;
                    allabilllHolder.allLevels["Concentration"] += 1;
                    gainedResources.AfterUpgrade(concentration, allabilllHolder.allLevels["Concentration"]);
                });
            }
            else
            {
                // Если способности нет, скрываем панель или устанавливаем значения по умолчанию
                //  image.sprite = null;
                nameText.text = "Нет способности";
                noteText.text = "";
            }


        }

    }
    public void Reroll()
    {
        FillButtonsWithAbilities();
    }
    private void OnDisable()
    {
        ProcedureOfLevelingUp.ChooseAbil -= Reroll;
    }
    private void BorderColorChanger(int requiredLevel, int currentLevel, Image[] borders )
    {
        foreach (Image b in borders)
        {
          b.color = (requiredLevel == currentLevel) ? borderColor : Color.white; 
            
        }
    }
    private void BorderColorChanger(Image[] borders)
    {
        foreach (Image b in borders)
        {
            b.color = Color.white;
        }
    }

}
