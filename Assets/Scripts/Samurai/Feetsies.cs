﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feetsies : MonoBehaviour {

    Samurai player;

    private void Start()
    {
        player = Samurai.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            player.isFalling = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.isFalling = true;
        }
    }

}
