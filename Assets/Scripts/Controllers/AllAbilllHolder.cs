using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class AllAbilllHolder : MonoBehaviour
{
    [Header("BulletsVariants")]
    [SerializeField] private BulletScriptableObject[] bulletLevels;
    public int bulletLevel = 0;

    [Header("FlashVariants")]
    [SerializeField] private GameObject flashStarter;
    [SerializeField] private FlashScriptableObject[] flashLevels;
    public int flashLevel = 0;

    [Header("StoneWalkVariants")]
    [SerializeField] private GameObject stoneWalkStarter;
    [SerializeField] private StoneWalkScriptableObject[] stoneWalkLevels;
    public int stoneWalkLevel = 0;

    [Header("WanderingFlashVariants")]
    [SerializeField] private GameObject wanderingFlashStarter;
    [SerializeField] private WanderingFlashScriptableObject[] wanderingFlashLevels;
    public int wanderingFlashLevel = 0;

    [Header("FreezingFieldVariants")]
    [SerializeField] private GameObject freezingFieldStarter;
    [SerializeField] private FreezingFieldScriptableObject[] freezingFieldLevels;
    public int freezingFieldLevel = 0;

    [Header("MeteorVariants")]
    [SerializeField] private GameObject meteorStarter;
    [SerializeField] private MeteorScriptableObject[] meteorLevels;
    public int meteorLevel = 0;

    [Header("IceArrowVariants")]
    [SerializeField] private GameObject iceArrowStarter;
    [SerializeField] private IceArrowScriptableObject[] iceArrowLevels;
    public int iceArrowLevel = 0;

    [Header("MagicBoltVariants")]
    [SerializeField] private GameObject magicBoltStarter;
    [SerializeField] private MagicBoltScriptableObject[] magicBoltLevels;
    public int magicBoltLevel = 0;

    [Header("LavaFieldVariants")]
    [SerializeField] private GameObject lavaFieldStarter;
    [SerializeField] private LavaFieldScriptableObject[] lavaFieldLevels;
    public int lavaFieldLevel = 0;

    [Header("FireBallVariants")]
    [SerializeField] private GameObject fireBallStarter;
    [SerializeField] private FireBallScriptableObject[] fireBallLevels;
    public int fireBallLevel = 0;

    [Header("LightningOrbVariants")]
    [SerializeField] private GameObject lightningOrbStarter;
    [SerializeField] private LightningOrbScriptableObjects[] lightningOrbLevels;
    public int lightningOrbLevel = 0;

    [Header("GravityVariants")]
    [SerializeField] private GameObject gravityStarter;
    [SerializeField] private GravityOrbScriptableObjects[] gravityLevels;
    public int gravityLevel = 0;

    [Header("ElectricOrbitals")]
    [SerializeField] private GameObject electricOrbitalsStarter;
    [SerializeField] private ElectricOrbitalScriptableObject[] electricOrbitalsLevels;
    public int electricOrbitalsLevel = 0;

    [Header("Laser")]
    [SerializeField] private GameObject laserStarter;
    [SerializeField] private LaserScriptableObject[] laserLevels;
    public int laserLevel = 0;

    [Header("StonePicke")]
    [SerializeField] private GameObject stonePickeStarter;
    [SerializeField] private StonePickeScriptableObject[] stonePickeLevels;
    public int stonePickeLevel = 0;

    [Header("MagicShield")]
    [SerializeField] private GameObject magicShieldStarter;
    [SerializeField] private MagicShieldScriptableObject[] magicShieldLevels;
    public int magicShieldLevel = 0;

    [Header("FireBreath")]
    [SerializeField] private GameObject fireBreathStarter;
    [SerializeField] private FireBreathScriptableObject[] fireBreathLevels;
    public int fireBreathLevel = 0;

    [Header("Cataclysm")]
    [SerializeField] private GameObject cataclysmStarter;
    [SerializeField] private CataclysmScriptableObject[] cataclysmLevels;
    public int cataclysmLevel = 0;

    [Header("Concentration")]
    [SerializeField] private GameObject concentrationStarter;
    [SerializeField] private ConcentrationScriptableObject[] concentrationLevels;
    public int concentrationLevel = 0;


    [Header("WindSlash")]
    [SerializeField] private GameObject windSlashStarter;
    [SerializeField] private WindSlashScriptableObject[] windSlashLevels;
    public int windSlashLevel = 0;

    [Header("StatsUpgrades")]
    [Header("ArmorUpgrades")]
    [SerializeField] private ArmorUpgrade[] armorLevels;
    public int armorLevel = 0;

    [Header("SpeedUpgrades")]
    [SerializeField] private SpeedUpgrade[] speedLevels;
    public int speedLevel = 0;

    [Header("EXPUpgrades")]
    [SerializeField] private EXPUpgrade[] expLevels;
    public int expLevel = 0;

    [Header("RadiusUpgrades")]
    [SerializeField] private RadiusUpgrade[] radiusLevels;
    public int radiusLevel = 0;

    [Header("CooldownUpgrades")]
    [SerializeField] private CooldownUpgrade[] cooldownLevels;
    public int cooldownLevel = 0;

    [Header("damageUpgrades")]
    [SerializeField] private DamageUpgrade[] damageLevels;
    public int damageLevel = 0;

    [Header("EPICUPGRADES")]
    [Header("EarthShake")]
    [SerializeField] private StonePickeScriptableObject earthShake;
    private bool isEarthShaking;

    [Header("StarFall")]
    [SerializeField] private MeteorScriptableObject starFall;
    private bool isStarfalling;

    [Header("IceHell")]
    [SerializeField] private IceArrowScriptableObject iseHell;
    private bool isIceHelling;

    [Header("RoamingLight")]
    [SerializeField] private WanderingFlashScriptableObject roamingLight;
    private bool isRoamingLight;

    [Header("ALL Upgrades")]
    private List<ScriptableObject[]> allUpgrades = new List<ScriptableObject[]>();
    public Dictionary<string,int> allLevels;
    private GameObject[] starters;
    private void Awake()
    {
        // Сохраняем все массивы улучшений в одном списке для упрощенного доступа
        allUpgrades.Add(bulletLevels);
        allUpgrades.Add(flashLevels);
        allUpgrades.Add(stoneWalkLevels);
        allUpgrades.Add(wanderingFlashLevels);
        allUpgrades.Add(freezingFieldLevels);
        allUpgrades.Add(meteorLevels);
        allUpgrades.Add(iceArrowLevels);
        allUpgrades.Add(magicBoltLevels);
        allUpgrades.Add(lavaFieldLevels);
        allUpgrades.Add(fireBallLevels);
        allUpgrades.Add(lightningOrbLevels);
        allUpgrades.Add(gravityLevels);
        allUpgrades.Add(electricOrbitalsLevels);
        allUpgrades.Add(laserLevels);
        allUpgrades.Add(stonePickeLevels);
        allUpgrades.Add(magicShieldLevels);
        allUpgrades.Add(fireBreathLevels);
        allUpgrades.Add(cataclysmLevels);
        allUpgrades.Add(windSlashLevels);
        allUpgrades.Add(concentrationLevels);
        allUpgrades.Add(armorLevels);
        allUpgrades.Add(speedLevels);
        allUpgrades.Add(expLevels);
        allUpgrades.Add(radiusLevels);
        allUpgrades.Add(cooldownLevels);
        allUpgrades.Add(damageLevels);


        allLevels = new Dictionary<string, int>()
        {
            {"Bullet", bulletLevel},
            {"Flash", flashLevel},
            {"StoneWalk", stoneWalkLevel},
            {"WanderingFlash", wanderingFlashLevel},
            {"FreezingField", freezingFieldLevel},
            {"Meteor", meteorLevel},
            {"IceArrow", iceArrowLevel},
            {"MagicBolt", magicBoltLevel},
            {"LavaField", lavaFieldLevel},
            {"FireBall", fireBallLevel},
            {"LightningOrb", lightningOrbLevel},
            {"GravityOrb", gravityLevel },
            {"ElectricOrbital", electricOrbitalsLevel},
            {"Laser", laserLevel},
            {"StonePicke", stonePickeLevel},
            {"MagicShield", magicShieldLevel},
            {"FireBreath", fireBreathLevel},
            {"Cataclysm", cataclysmLevel},
            {"WindSlash", windSlashLevel},
            {"Concentration", concentrationLevel},
            {"Armor", armorLevel},
            {"Speed", speedLevel},
            {"EXP",  expLevel},
            {"Radius", radiusLevel },
            {"Cooldown", cooldownLevel},
            {"Damage", damageLevel},
        };

        starters = new GameObject[]
        {
            flashStarter,
            stoneWalkStarter,
            wanderingFlashStarter,
            freezingFieldStarter,
            meteorStarter,
            iceArrowStarter,
            magicBoltStarter,
            lavaFieldStarter,
            fireBallStarter,
            lightningOrbStarter,
            gravityStarter,
            electricOrbitalsStarter,
            laserStarter,
            stonePickeStarter,
            magicShieldStarter,
            fireBreathStarter,
            cataclysmStarter,
            windSlashStarter,
            concentrationStarter
        };
       
    }
    public IEnumerator ChooseAnAbilityLevelCoroutine(Action<ScriptableObject[]> callback)
    {
        ScriptableObject[] abilities = ChooseAnAbilityLevel();
        yield return null; // Для синхронизации
        callback?.Invoke(abilities);
    }
    // метод который выберет 3 случайных способности и сохранит ссылки
    // на эти объекты в виде массива из 3х элементов
    public ScriptableObject[] ChooseAnAbilityLevel()
    {
        List<ScriptableObject> selectedUpgrades = CheckerAnActiveAbill();
        HashSet<int> selectedIndexes = new HashSet<int>();
        // для отслеживания уникальных способностей
        HashSet<ScriptableObject> uniqueUpgrades = new HashSet<ScriptableObject>(selectedUpgrades); 
        
        // Calculate a base chance for selection of each ability
        int chance = 100 / allUpgrades.Count;
        int cycles = 0;
        // Continue selection until we have 3 distinct abilities
        while (selectedUpgrades.Count < 3 || cycles < 10)
        {
            for (int i = 0; i < allUpgrades.Count; i++)
            {
                if (cycles == 100) return NoMatter3Elements(selectedUpgrades);
                if (selectedIndexes.Contains(i)) continue; // Skip already selected indexes

                int variety = UnityEngine.Random.Range(0, 100);
                string key = allLevels.ElementAt(i).Key; // Получаем ключ для текущего уровня
                if (variety <= chance && allLevels[key] < allUpgrades[i].Length)
                {
                    ScriptableObject upgradeToAdd = allUpgrades[i][allLevels[key]];

                    // Проверяем, уникальна ли способность
                    if (uniqueUpgrades.Add(upgradeToAdd)) // Если успешно добавили, значит она уникальна
                    {
                        selectedUpgrades.Add(upgradeToAdd);
                    }

                    selectedIndexes.Add(i);

                    if (uniqueUpgrades.Count == 3) break; // Break early if 3 unique abilities are selected
                }
                cycles++;
            }
        }
        
        return NoMatter3Elements(selectedUpgrades);
    }

    private ScriptableObject[] NoMatter3Elements(List<ScriptableObject> selectedUpgrades)
    {
        ScriptableObject[] result = new ScriptableObject[3];
        for (int i = 0; i < 3; i++)
        {
            if (i < selectedUpgrades.Count)
            {
                result[i] = selectedUpgrades[i];
            }
            else
            {
                result[i] = null; // Если недостаточно уникальных способностей, заполняем null
            }
        }

        return result;
    }
    private List<ScriptableObject> CheckerAnActiveAbill()
    {
        List<ScriptableObject> selectedUpgrades = EpicUpgrade();

        for (int i = 0; i < allLevels.Count; i++)
        {
            string key = allLevels.ElementAt(i).Key; // Получаем ключ для текущего уровня

            // Check if current level is not maximizing the upgrade array and potentially available for upgrade.
            if (allLevels[key] < allUpgrades[i].Length && allLevels[key] > 0)
            {
                int variety = UnityEngine.Random.Range(0, 100);
                if (variety <= 30)
                {
                    selectedUpgrades.Add(allUpgrades[i][allLevels[key]]);
                }
            }
        }

        return selectedUpgrades;
    }
    public void ObjectsStarter(int index)
    {
       
            for (int i = 0; i < starters.Length; i++)
            {
                if (i == index) { starters[i].gameObject.SetActive(true); }

            }
    }

    
    private List<ScriptableObject> EpicUpgrade()
    {
        List<ScriptableObject> selectedUpgrades = new List<ScriptableObject>();

        if (!isEarthShaking && stoneWalkLevel == 7 && stonePickeLevel == 7 && UnityEngine.Random.Range(0,100) < 40)
        {
            selectedUpgrades.Add(earthShake);
        }
        else if (!isStarfalling && meteorLevel == 7 && magicBoltLevel == 7 && UnityEngine.Random.Range(0, 100) < 40)
        {
            selectedUpgrades.Add(starFall);
        }
        else if (!isIceHelling && bulletLevel == 7 && iceArrowLevel == 7 && UnityEngine.Random.Range(0, 100) < 40)
        {
            selectedUpgrades.Add(iseHell);
        }
        else if (!isRoamingLight && wanderingFlashLevel == 7 && flashLevel == 7 & UnityEngine.Random.Range(0, 100) < 40)
        {
            selectedUpgrades.Add(roamingLight);
        }
            return selectedUpgrades;
    }

}