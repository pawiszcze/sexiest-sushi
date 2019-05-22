using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionBlockScript : MonoBehaviour
{
    public static DescriptionBlockScript instance;

    private void Awake()
    {
        instance = this;
    }
}
