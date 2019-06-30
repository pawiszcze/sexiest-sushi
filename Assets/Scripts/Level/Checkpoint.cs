using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool startingCheckpoint;
    Samurai player;
    public static Checkpoint thisOne;
    public SpriteRenderer thisSprite;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        if (startingCheckpoint)
        {
            DebugIsActive();
        }
        player = Samurai.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DebugIsActive()
    {
        if (thisOne != null)
        {
            thisOne.thisSprite.color = Color.red;
        }
        thisSprite.color = Color.yellow;
        thisOne = this;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == player.gameObject)
        {
            DebugIsActive();
        }
    }


}
