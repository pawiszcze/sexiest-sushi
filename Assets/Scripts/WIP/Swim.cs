using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    Samurai player;
    Collider2D colli;

    // Start is called before the first frame update
    void Start()
    {
        player = Samurai.instance;
        colli = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        colli.isTrigger = player.isUnderwater;
        //Debug.Log(player);
    }
}
