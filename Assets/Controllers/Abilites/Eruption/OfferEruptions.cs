using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferEruptions : MonoBehaviour
{
    [SerializeField] private Transform defaultParent;
    [SerializeField] private Transform secondParent;


    private float startingposX;
    private float startingposY;

    private void Awake()
    {
        startingposX = transform.localPosition.x;
        startingposY = transform.localPosition.y;
    }
    private void OnEnable()
    {
       
        StartCoroutine(LifeTime());
    }
    public void Offer()
    {
        gameObject.SetActive(false);
    }



    public void SetNewParent()
    {
        gameObject.transform.SetParent(secondParent);
    }
    public void ReturnDefaultParent()
    {
        gameObject.transform.SetParent(defaultParent);
        transform.localPosition = new Vector2 ( startingposX,  + startingposY);
    }

    private IEnumerator LifeTime()
    {
        SetNewParent();
        yield return new WaitForSeconds(0.5F);
        ReturnDefaultParent();
        Offer();
    }
}
