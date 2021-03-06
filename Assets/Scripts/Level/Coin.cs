﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{    
    Samurai player;
    GameManager game;
    bool doOnce;
    AudioSource audioSource;
    
    void Start()
    {
        player = Samurai.instance;
        game = GameManager.instance;
        audioSource = game.gameObject.GetComponent<AudioSource>();
        doOnce = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doOnce)
        {
            doOnce = false;
            if (collision.gameObject == player.gameObject)
            {
                audioSource.PlayOneShot(game.coinSound);
                int levelID = player.currentZone;
                game.coinsCollected[levelID]++;
                game.UpdateCoins(levelID);
                Destroy(gameObject);
            }
            else { doOnce = true; }
        }
    }
}