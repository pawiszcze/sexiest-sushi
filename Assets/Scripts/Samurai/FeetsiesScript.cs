using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetsiesScript : MonoBehaviour {

    SamuraiScript player;

    private void Start()
    {
        player = SamuraiScript.instance;
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
