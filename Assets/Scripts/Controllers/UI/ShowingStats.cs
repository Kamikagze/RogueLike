using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowingStats : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameOfAbil;
    [SerializeField] private TextMeshProUGUI description;


    [SerializeField]private DamageAbillController dmController;
    private List<AbstractAbill> abillList;
   private AbstractAbill currAbill;
    private BaseScriptableObject abilityInside;

    private void Awake()
    {
        abillList = new List<AbstractAbill>();
    }
    private void OnEnable()
    {
        AbstractAbill.OnAbill += GetNewAbillInList;
        InGainedHolder.ShowStatsOfThisAbill += ShowAllStats;
    }
    private void GetNewAbillInList(AbstractAbill abstractAbill)
    {
        abillList.Add(abstractAbill);
    }
    private void NeededAbill(int ID)
    {
        currAbill = null;
        foreach (AbstractAbill ab in abillList)
        {
            if (currAbill == null)
            {
                currAbill = ab.ShowStatsBegin(ID);
            }
            
        }
    }
    public void ShowAllStats(int ID, BaseScriptableObject abilityInside)
    {
        icon.sprite = abilityInside.image;

        NeededAbill(ID);

        float[] stats = currAbill.ShowStats(ID);

        nameOfAbil.text = abilityInside.name + "  " + "LVL " + stats[0];

        description.text = $"”рон {dmController.showDamageStats(ID)}\n" +
                    $"количество +{stats[1]}\n" +
                    $"размер +{stats[2]}%\n" +
                    $"перезар€дка {stats[3]}\n" +
                    $"длительность +{stats[4]}";

    }
    
    public void OFF()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        InGainedHolder.ShowStatsOfThisAbill -= ShowAllStats;
        AbstractAbill.OnAbill -= GetNewAbillInList;

    }
}
