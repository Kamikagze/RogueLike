using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GainedResources : MonoBehaviour
{
    [SerializeField] private GameObject iconOfGained;
    private List<BaseScriptableObject> iconOfGainedList = new List<BaseScriptableObject>();
    [SerializeField] private Transform parent;

    public static event Action<int> Levelled;
    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AfterUpgrade(BaseScriptableObject scriptableObject, int level)
    {
        
            bool isHere = false;
            foreach (var item in iconOfGainedList)
            {
                if (item.ID == scriptableObject.ID)
                {
                    isHere = true;
                    Levelled?.Invoke(scriptableObject.ID);
                    
                }


            }
            if (isHere) return;
            else
            {
                iconOfGainedList.Add(scriptableObject);
                GameObject newIcon = Instantiate(iconOfGained, parent);
                InGainedHolder inGainedHolder = newIcon.GetComponent<InGainedHolder>();
                if (inGainedHolder != null)
                {
                    inGainedHolder.SetIcon(scriptableObject, level);
                }
            }

        
    }
    private void OnDisable()
    {

    }
}