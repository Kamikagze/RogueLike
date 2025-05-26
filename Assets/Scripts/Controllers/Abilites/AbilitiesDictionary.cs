using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityList
{
    public AbilityKeys key;
    public List<AbstractAbill> abilities;
}
public class AbilitiesDictionary : MonoBehaviour
{
    [SerializeField]
    private List<AbilityList> abilitiesList;

    private void OnEnable()
    {
        ArtifactScriptableObject.ArtifactUpgradeOfAbilities += GettingAnArtifact;
    }


    private void GettingAnArtifact(AbilityKeys[] abilityKeys, Dictionary<TypeUpgrade, float> dictionary)
    {
        for (int i = 0; i < abilityKeys.Length; i++)
        {
            foreach (AbilityList ability in abilitiesList)
            {
                if (ability.key == abilityKeys[i])
                {
                    foreach (AbstractAbill abill in ability.abilities)
                    {
                        abill.DictionaryRecepient(dictionary);
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        ArtifactScriptableObject.ArtifactUpgradeOfAbilities -= GettingAnArtifact;
    }

}
