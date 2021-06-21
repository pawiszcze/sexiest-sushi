using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive : MonoBehaviour
{
    private Samurai player;
    private Crouching crouching;
    private Collider2D colli;
    
    void Start()
    {
        player = Samurai.instance;
        crouching = Crouching.instance;
        colli = gameObject.GetComponent<Collider2D>();
    }
    
    void Update()
    {
        colli.isTrigger = player.isUnderwater;
        if (player.isUnderwater)
        {
            crouching.Uncrouch();
        }
        crouching.enabled = !player.isUnderwater;
        Debug.Log(player.isUnderwater);
    }
}
