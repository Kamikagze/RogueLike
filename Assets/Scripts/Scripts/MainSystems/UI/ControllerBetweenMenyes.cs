using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBetweenMenyes : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject abilities;
    [SerializeField] GameObject placeForStats;

    public void HideDescription()
    {
        button.SetActive(false);
        abilities.SetActive(false);
        placeForStats.SetActive(true);
      
    }
    public void ActiveDescription()
    {

        button?.SetActive(true);
        abilities?.SetActive(true);
        placeForStats.SetActive(false);
    }
}
