using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinScript : FoeScript {

    override protected void Start () {
        base.Start();
        rig = gameObject.GetComponent<Rigidbody2D>();
        facingRight = true;
        hitPoints = 1;
        isBurning = false;
        isGrounded = false;
        isScaredShitless = false;
        damage = 0;
        player = SamuraiScript.instance;
    }
}
