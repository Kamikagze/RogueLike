using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] ArtifactsHolder artifactsHolder;

    private List<ArtifactScriptableObject> gainedArtifacts = new List<ArtifactScriptableObject>();

    private Color commonColorBorders = new Color(246f / 255f, 245f / 255f, 240f / 255f);
    private Color uncommonColorBorders = new Color(74f / 255f, 144f / 255f, 226f / 255f);
    private Color rareColorBorders = new Color(255f / 255f, 215f / 255f, 0f);
    private Color legendaryColorBorders = new Color(178f / 255f, 34f / 255f, 34f / 255f);
    [Header("Borders")]
    [SerializeField] Image[] borders;
    [SerializeField] Image[] Icons;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI nameArtifact;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private Button acceptionButton;
    [SerializeField] private Button rerollButton;
    [SerializeField] private int rerollCount= 0;

    private ArtifactScriptableObject chosenArt;
    private Color nonActiveColor = new Color(0.77f, 0.79f, 1f, 0.2f);
    private Color activeColor = new Color(0.77f, 0.79f, 1f, 0.85f);

    public static event Action GainedAnArtifact;

    private void OnEnable()
    {
        Collector.CollectArtifact += GetTreeArtifacts;
        EXPManager.CountOfRerolls += GainRerol;
    }
    private void Start()
    {
        acceptionButton.interactable = false;
    }
    private void GetTreeArtifacts()
    {
        Cleaner();
        gainedArtifacts = artifactsHolder.ThreeArts();
        SetBordersColor();
        SetterRerollButton();
    }

    private void SetBordersColor()
    {
        for (int i = 0; i < 3; i++)
        {
            BorderColor(gainedArtifacts[i], i);
            SetIcons(i);
        }

    }
    private void BorderColor(ArtifactScriptableObject gainedArtifact, int index)
    {
        RarityType rarityType = gainedArtifact.rarity;
        switch (rarityType)
        {
            case RarityType.Common:
                borders[index].color = commonColorBorders; break;
            case RarityType.Uncommon:
                borders[index].color = uncommonColorBorders; break;
            case RarityType.Rare:
                borders[index].color = rareColorBorders; break;
            case RarityType.Legendary:
                borders[index].color = legendaryColorBorders; break;

        }
    }
    private void SetIcons(int index)
    {
        Icons[index].sprite = gainedArtifacts[index].artifactImage;
    }
    private void NameColor(int Index)
    {
        RarityType rarityType = gainedArtifacts[Index].rarity;
        switch (rarityType)
        {
                case RarityType.Common:
                nameArtifact.color = commonColorBorders; break ;
                case RarityType.Uncommon:
                nameArtifact.color = uncommonColorBorders; break;
                case RarityType.Rare:
                nameArtifact.color = rareColorBorders; break;
                case RarityType.Legendary:
                nameArtifact.color = legendaryColorBorders;break;
        }
    }
    public void ShowInfo(int index)
    {
        
        chosenArt = gainedArtifacts[index];
        nameArtifact.text = gainedArtifacts[index].artifactName;
        description.text = gainedArtifacts[index].description;
        NameColor(index);
        acceptionButton.interactable = true;
        acceptionButton.image.color = activeColor;
    }
    public void  Reroll()
    {
        rerollCount--;
        GetTreeArtifacts();
    }
    private void SetterRerollButton()
    {
        if (rerollCount > 0)
        {
            rerollButton.interactable = true;
            rerollButton.image.color = activeColor;
        }
        else
        {
            rerollButton.interactable= false;
            rerollButton.image.color = nonActiveColor;
        }
    }
    public void ClearInfo()
    {
        chosenArt.UpgradeEvent();
        artifactsHolder.ArtifactHasBeenChosen(chosenArt);
        Cleaner();
    }
    public void OnGainedArtifact()
    {
        GainedAnArtifact?.Invoke();
    }
    private void Cleaner()
    {        
        chosenArt = null;
        nameArtifact.text = null;
        description.text = null;
        acceptionButton.interactable = false;
        acceptionButton.image.color = nonActiveColor;
        gainedArtifacts.Clear();
    }
    private void GainRerol()
    {
        rerollCount++;
    }
    private void OnDisable()
    {
        Collector.CollectArtifact -= GetTreeArtifacts;
        EXPManager.CountOfRerolls -= GainRerol;
    }
}
