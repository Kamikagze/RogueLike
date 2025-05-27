using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

    public void Test()
    {
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(true);
         
        }
        Destroy(gameObject);
    }
}
