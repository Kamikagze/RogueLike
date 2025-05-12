using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArtifactScriptableObject))]
public class ArtifactScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Получаем ссылку на объект, редактируемый в инспекторе
        ArtifactScriptableObject artifact = (ArtifactScriptableObject)target;

        // Отображаем все поля, которые должны всегда быть видимыми
        EditorGUILayout.PropertyField(serializedObject.FindProperty("artifactName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("comparedAbility"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rarity"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("artifactImage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("aspectOfUpgrade"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("multipleArtifact"));



        if (artifact.multipleArtifact)
        {
            artifact.aspectOfUpgrade = AspectOfUpgrade.None;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("aspectsOfUpgrade"));
            foreach (AspectOfUpgrade aspect in artifact.aspectsOfUpgrade)
            {
                switch (aspect)
                {
                    case AspectOfUpgrade.ability:
                        ShowAbilityUpgrades();
                        break;
                    case AspectOfUpgrade.health:
                        ShowHealthUpgrades();
                        break;
                    case AspectOfUpgrade.stats:
                        ShowStatsUpgrades();
                        break;
                    case AspectOfUpgrade.enemy:
                        ShowEnemyUpgrades();
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {

            if (artifact.aspectOfUpgrade != AspectOfUpgrade.None)
            {
                switch (artifact.aspectOfUpgrade)
                {
                    case AspectOfUpgrade.ability:
                        ShowAbilityUpgrades();
                        break;
                    case AspectOfUpgrade.health:
                        ShowHealthUpgrades();
                        break;
                    case AspectOfUpgrade.stats:
                        ShowStatsUpgrades();
                        break;
                    case AspectOfUpgrade.enemy:
                        ShowEnemyUpgrades();
                        break;
                    default:
                        break;
                }
            }
        }


        // Остальные поля, если необходимо
        serializedObject.ApplyModifiedProperties();
    }

    private void ShowAbilityUpgrades()
    {
        EditorGUILayout.LabelField("Abilities Upgrades", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("abilityKeys"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damageUpgrade"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("countUpgrade"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("radiusUpgrade"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cooldownUpgrade"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("durationUpgrade"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("special"));

    }

    private void ShowHealthUpgrades()
    {
        EditorGUILayout.LabelField("Health Upgrades", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("regeneration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("heel"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lifes"));
    }

    private void ShowStatsUpgrades()
    {
        EditorGUILayout.LabelField("StatsUpgrades", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("armor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exp"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("radius"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cooldown"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
    }   
    private void ShowEnemyUpgrades()
    {
        EditorGUILayout.LabelField("EnemyUpgrades", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("freezeDuration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eXPAfterDeath"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bonusDamage"));
    }
}