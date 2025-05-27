using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenyCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject gainedResourses;


    public void Offer()
    {
        gainedResourses.SetActive(false);
    }
}
