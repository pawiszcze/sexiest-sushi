using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlay : MonoBehaviour
{
    public static UIOverlay instance;
    
    void Awake()
    {
        instance = this;
    }
}
