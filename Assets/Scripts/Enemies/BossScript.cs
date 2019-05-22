﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : FoeScript {

    public BossLanternScript spawnLantern;
    public bool isDead;

	void Start () {
        maxSpeed = 0f;
        currentSpeed = 0f;
        rig = gameObject.GetComponent<Rigidbody2D>();
        damage = 10;
        facingRight = true;
        hitPoints = 2;
        isBurning = false;
        isGrounded = false;
        isScaredShitless = false;
        force = 10000;
        player = SamuraiScript.instance;
    }
	
	override protected void Die()
    {
        spawnLantern.DimLantern();
        base.Die();
    }
}