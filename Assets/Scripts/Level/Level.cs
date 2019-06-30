using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    GameManager game;
    public int levelID;
    Samurai player;

	void Start () {
        game = GameManager.instance;
        player = Samurai.instance;
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moni")
        {
            game.coinsInLevels[levelID]++;
            game.UpdateCoins(levelID);
        } else if (collision.gameObject.tag == "Player"){
            player.currentZone = levelID;
            game.UpdateCoins(levelID);
        }
    }
}
