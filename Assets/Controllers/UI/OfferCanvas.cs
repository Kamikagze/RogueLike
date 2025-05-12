using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferCanvas : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    [SerializeField] private Canvas upgrades;
    //[SerializeField] private GameObject descriptionOfAbill;
    [SerializeField] private ControllerBetweenMenyes controllerBetweenMenyes;
    public void Offer()
    {
        menu.enabled = false;
     //   descriptionOfAbill.SetActive(false);
        upgrades.enabled = true;
        controllerBetweenMenyes.ActiveDescription();


    }
    public void ActivateAgain()
    {
        menu.enabled = true;
        upgrades.enabled = false;
    }
}
