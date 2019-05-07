using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    GameManagerScript game;
    public int levelID;

	void Start () {
        game = GameManagerScript.instance;
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CoinScript>())
        {
            game.coinsInLevels[levelID]++;
            game.UpdateCoins(levelID);
        } else if (collision.gameObject.GetComponent<Samurai_Script>()){
            collision.gameObject.GetComponent<Samurai_Script>().currentZone = levelID;
            game.UpdateCoins(levelID);
        }
    }
}
