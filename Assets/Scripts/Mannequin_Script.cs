using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin_Script : FoeScript {

	// Use this for initialization
	void Start () {
        rig = gameObject.GetComponent<Rigidbody2D>();
        facingRight = true;
        hitPoints = 1;
        isBurning = false;
        isGrounded = false;
        isScaredShitless = false;
        damage = 0;
        player = Samurai_Script.instance;
    }
	
	// Update is called once per frame
	override public void GetDamaged(int damage, Collider2D instigator) {
		
	}
}
