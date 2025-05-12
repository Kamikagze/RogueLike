using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFireBreathAnim : MonoBehaviour
{
    [SerializeField] Transform startingParent;
    [SerializeField] Transform secondParent;
    [SerializeField] Animator fireBreath;
    public void SetParent()
    {
        gameObject.transform.SetParent(secondParent);
    }
    public void ReturnParent()
    {
        gameObject.transform.SetParent(startingParent);
        transform.position = startingParent.position;
        transform.rotation = startingParent.rotation;

    }
    public void StarterEffect()
    {
        fireBreath.SetBool("Action", true);
    }

    public void Offer()
    {   
       fireBreath.SetBool("Action", false);        
    }
 
}
