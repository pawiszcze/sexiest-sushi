using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    Samurai player;
    
	void Start () {
        player = Samurai.instance;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            player.canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.rig.gravityScale = 2;
            player.rig.drag = 2;
            player.goingDown = false;
            player.canClimb = false;
            player.isFalling = true;
        }
    }
}
