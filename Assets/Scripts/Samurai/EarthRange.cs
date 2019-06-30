using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRange : MonoBehaviour {

    public static EarthRange instance;
    private Samurai player;
    bool assigned;

    void Awake()
    {
        instance = this;
        assigned = false;
    }

    protected virtual void Start()
    {
        if (!assigned)
        {
            player = Samurai.instance;
            assigned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {
            player.earthInRange.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {

            player.earthInRange.Remove(collision.gameObject);
        }
    }
}
