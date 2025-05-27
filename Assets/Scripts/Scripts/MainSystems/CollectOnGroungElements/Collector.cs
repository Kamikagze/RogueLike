using System;
using UnityEngine;


public class Collector : MonoBehaviour
{


 

    public static event Action CollectHeart;
    public static event Action CollectArtifact;


   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EXP"))
        {
                                    
        }
        else if (collision.gameObject.CompareTag("HP"))
        {
            OnHPCollected();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Artifact"))
        {
            OnArtifactCollected();
            Destroy(collision.gameObject);
        }


    }
   
   
    private void OnHPCollected()
    { 
        CollectHeart?.Invoke();
    }
    private void OnArtifactCollected()
    {
        CollectArtifact?.Invoke();
    }
   
}
