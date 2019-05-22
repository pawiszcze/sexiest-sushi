using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDisappearScript : MonoBehaviour {

    SamuraiScript player;
    SpriteRenderer thisSprite;

    private void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        player = SamuraiScript.instance;
        thisSprite.color = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            thisSprite.color = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            thisSprite.color = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, 1f);
        }
    }
}
