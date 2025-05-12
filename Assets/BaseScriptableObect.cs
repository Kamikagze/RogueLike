using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScriptableObject : ScriptableObject
{
    [SerializeField] public Sprite image;

    [TextArea]
    public string note;
    public int ID;

    // добавить поле айди.
}
