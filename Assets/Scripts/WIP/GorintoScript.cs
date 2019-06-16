using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorintoScript : MonoBehaviour
{
    public static GorintoScript instance;
    public Sprite[] sprites;

    private void Awake()
    {
        instance = this;
        //sprites = new Sprite[5];
    }
}
