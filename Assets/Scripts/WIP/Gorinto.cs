using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorinto : MonoBehaviour
{
    public static Gorinto instance;
    public Sprite[] sprites;

    private void Awake()
    {
        instance = this;
        //sprites = new Sprite[5];
    }
}
