using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class ArtifactsHolder : MonoBehaviour
{
    [SerializeField] private List<ArtifactScriptableObject> commonArts;
    [SerializeField] private List<ArtifactScriptableObject> uncommonArtifacts;
    [SerializeField] private List<ArtifactScriptableObject> rareArtifacts;
    [SerializeField] private List<ArtifactScriptableObject> legendaryArtifacts;

    private const int commonValue = 750;
    private const int uncommonValue = 900;
    private const int rareValue = 975;
    private const int legendaryValue = 1000;

    //[Header("Systems")]
    //[SerializeField] private HealthSystem healthSystem;


    private List<ArtifactScriptableObject> ChoosenArtifacts = new List<ArtifactScriptableObject>();

    
    private RarityType[] RarityTypeOfArtifact(int count = 3)
    {
        RarityType[] artefactKeys = new RarityType[count];

        for (int i = 0; i < 3; i++)
        {
            int chance = UnityEngine.Random.Range(0, 1000);
            Debug.Log($"{chance}");

            if (chance <= commonValue)
            {
                artefactKeys[i] = RarityType.Common;
            }
            else if (chance > commonValue && chance <= uncommonValue)
            {
                artefactKeys[i] = RarityType.Uncommon;
            }
            else if (chance > uncommonValue && chance <= rareValue)
            {
                artefactKeys[i] = RarityType.Rare;
            }
            else if (chance > rareValue && chance <= legendaryValue)
            {
                artefactKeys[i] = RarityType.Legendary;
            }
        }

        return artefactKeys;
    }

    public List<ArtifactScriptableObject> ThreeArts()
    {
        ChoosenArtifacts.Clear();
        RarityType[] rarityTypes = RarityTypeOfArtifact();

        HashSet<ArtifactScriptableObject> uniqueArtifacts = new HashSet<ArtifactScriptableObject>();

        foreach (RarityType rarity in rarityTypes)
        {
            ArtifactScriptableObject selectedArtifact = TryToCatchAnArtifact(rarity, uniqueArtifacts);
            if (selectedArtifact != null)
            {
                uniqueArtifacts.Add(selectedArtifact);
                ChoosenArtifacts.Add(selectedArtifact);
            }
        }

        return ChoosenArtifacts;
    }

    private ArtifactScriptableObject TryToCatchAnArtifact(RarityType rarity, HashSet<ArtifactScriptableObject> uniqueArtifacts)
    {
        List<ArtifactScriptableObject> artifacts = GetArtifactsByRarity(rarity);
        return GetUniqueArtifact(artifacts, uniqueArtifacts);
    }

    private List<ArtifactScriptableObject> GetArtifactsByRarity(RarityType rarity)
    {
        switch (rarity)
        {
            case RarityType.Common:
                return commonArts;
            case RarityType.Uncommon:
                return uncommonArtifacts;
            case RarityType.Rare:
                return rareArtifacts;
            case RarityType.Legendary:
                return legendaryArtifacts;
            default:
                return new List<ArtifactScriptableObject>();
        }
    }

    private ArtifactScriptableObject GetUniqueArtifact(List<ArtifactScriptableObject> artifacts, HashSet<ArtifactScriptableObject> uniqueArtifacts)
    {
        var availableArtifacts = artifacts.Where(a => !uniqueArtifacts.Contains(a)).ToList();

        if (availableArtifacts.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableArtifacts.Count);
            return availableArtifacts[randomIndex];
        }

        return null;
    }

    public void ArtifactHasBeenChosen(ArtifactScriptableObject artifact)
    {
        RarityType rarityType = artifact.rarity;
        string name = artifact.artifactName; 

        switch (rarityType)
        {
            case RarityType.Common:
                ArtifactSeeKer(commonArts, name); break;
            case RarityType.Uncommon:
                ArtifactSeeKer(uncommonArtifacts,name); break;
            case RarityType.Rare:
                ArtifactSeeKer(rareArtifacts, name); break;
            case RarityType.Legendary:
                ArtifactSeeKer(legendaryArtifacts, name); break;

        }
    }
    private void ArtifactSeeKer(List<ArtifactScriptableObject> whereIShouldFind, string name)
    {
        foreach (var artifact in whereIShouldFind)
        {
            if (artifact.artifactName == name)
            {
                whereIShouldFind.Remove(artifact);
                break;
            }
        }
    }

    
}