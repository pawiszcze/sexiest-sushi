using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    //public int levelID;
    Samurai_Script player;
    GameManagerScript game;
    bool doOnce;
    //int levelID;
    
    void Start()
    {
        player = Samurai_Script.instance;
        game = GameManagerScript.instance;
        doOnce = true;
    }
    
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doOnce)
        {
            doOnce = false;
            if (collision.gameObject == player.gameObject)
            {
                game.gameObject.GetComponent<AudioSource>().PlayOneShot(game.coinSound);
                int levelID = player.currentZone;
                game.coinsCollected[levelID]++;
                game.UpdateCoins(levelID);
                Debug.Log(levelID);
                Destroy(gameObject);
            }
            else { doOnce = true; }
        }
    }
}