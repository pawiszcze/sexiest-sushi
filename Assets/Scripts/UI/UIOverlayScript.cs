using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlayScript : MonoBehaviour
{
    public static UIOverlayScript instance;
    
    void Awake()
    {
        instance = this;
    }
}
