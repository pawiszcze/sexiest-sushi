using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRangeScript : MonoBehaviour {

    public static EarthRangeScript instance;
    private SamuraiScript player;
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
            player = SamuraiScript.instance;
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
