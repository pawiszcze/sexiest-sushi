using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidRangeScript : MonoBehaviour
{

    public static VoidRangeScript instance;
    private Samurai_Script player;
    bool assigned;

    // Use this for initialization
    void Awake()
    {
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
            player.voidInRange.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foe")
        {

            player.voidInRange.Remove(collision.gameObject);
        }
    }

}
