using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    GameManagerScript game;
    public int levelID;
    SamuraiScript player;

	void Start () {
        game = GameManagerScript.instance;
        player = SamuraiScript.instance;
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
