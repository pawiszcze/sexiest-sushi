using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionBlock : MonoBehaviour
{
    public static DescriptionBlock instance;

    private void Awake()
    {
        instance = this;
    }
}
