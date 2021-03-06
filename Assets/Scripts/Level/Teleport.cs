﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactive {

    public Teleport destination;
    public bool isActive;
    public SpriteRenderer thisSprite;
    bool isTeleporting;

	override protected void Start () {
        isTeleporting = false;
        interactionName = "teleport";
        player = Samurai.instance;
        thisSprite = GetComponent<SpriteRenderer>();
    }

	override public void Interact () {
        if (!isTeleporting)
        {
            if (destination.isActive)
            {
                StartCoroutine(Teleporting());
            }
            isActive = true;
        }
    }

    private void DebugIsActive()
    {
        if (destination != null && isActive && destination.isActive)
        {
            thisSprite.color = Color.green;
            destination.thisSprite.color = Color.green;
        }
    }

    private IEnumerator Teleporting()
    {
        isTeleporting = true;
        player.canControl = false;
        player.rig.drag = 20;
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, player.transform.position.z);
        player.rig.drag = 2;
        player.canControl = true;
        isTeleporting = false;
    }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject == player.gameObject)
        {
            isActive = true;
            DebugIsActive();
        }
        if (destination !=null && destination.isActive)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}
