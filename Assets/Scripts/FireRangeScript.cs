using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangeScript : MonoBehaviour {

    public static FireRangeScript instance;
    private Samurai_Script player;
    bool assigned;
    
	void Awake () {
        instance = this;
        assigned = false;
	}

    protected virtual void Start()
    {
        if (!assigned)
        {
            player = Samurai_Script.instance;
            assigned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {
            player.fireInRange.Add(collision.gameObject);
        } 

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {

            player.fireInRange.Remove(collision.gameObject);
        }
    }
}
