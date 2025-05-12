using System;

using UnityEngine;

public class EventEXPManager : MonoBehaviour
{

    public static event Action <int> ExpCollected;


    public static void OnExpCollected(int _crystalCost)
    {
        ExpCollected?.Invoke(_crystalCost);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
