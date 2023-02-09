using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{
    public static GlobalData Instance { get; private set; }
    public static string name { get; set; }

    void Awake()
    {
        Instance = this;
    }

    
}
