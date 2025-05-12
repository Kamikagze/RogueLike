using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGainedHolder : MonoBehaviour
{
    [SerializeField] private Image image;
    private int level;
    [SerializeField] private Button button;
    public int ID;

    private ControllerBetweenMenyes HideMenu;
    //private DamageAbillController dmController;
    //private AbstractAbill currAbill;
    private BaseScriptableObject abilityInside;
    public static event Action<int, BaseScriptableObject> ShowStatsOfThisAbill;
  
    public static event Action ElectricOrbitalEvent;

    [SerializeField] private Image levelImage;
    private void OnEnable()
    {
        GainedResources.Levelled += LevelUp;
        HideMenu = FindObjectOfType<ControllerBetweenMenyes>();
        //dmController = FindObjectOfType<DamageAbillController>();
        //currAbill = FindObjectOfType<AbstractAbill>();
        LevelMarker();
    }

    public void SetIcon(BaseScriptableObject scriptableObject, int lvl)
    {
        ID = scriptableObject.ID;
        image.sprite = scriptableObject.image;
        level = lvl;
        abilityInside = scriptableObject;
      
    }
    public void LevelUp(int i)
    {
        if (i==ID)
        {
            level++;
            LevelMarker();
        }
    }
    private void LevelMarker()
    {
        level = Mathf.Clamp(level, 0, 7);
        levelImage.fillAmount = (float)level / 7f;
    }
    public void ShowStats()
    {
        if (ID <= 20)
        {
            HideMenu.HideDescription();
            ShowStatsOfThisAbill?.Invoke(ID, abilityInside);
        }        
    }
  
    //private void Shoving()
    //{
        
    //    ShowStatsOfThisAbill?.Invoke(ID, abilityInside);
    //}
    ////private void Switcher(int ID, ref bool active)
    //{
    //    if (ID == 8)
    //    {
    //        active = true;
    //        ElectricOrbitalEvent?.Invoke();
    //        return;
    //    }
    //    else if (ID <= 20)
    //    {
    //        active = true;
    //        Shoving();
    //        return;
    //    }
    //    else if (ID > 20 && ID < 27)
    //    {
    //        active = true;

    //    }
        
        
    //}
     private void OnDisable()
    {
        GainedResources.Levelled -= LevelUp;
    }
}
