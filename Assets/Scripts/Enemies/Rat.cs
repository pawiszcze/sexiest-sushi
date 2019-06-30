using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Foe
{
    int ground;
    int platform;
    int layers;
    Vector3 offset;

    private void Awake()
    {
        expValue = 10;
    }

    override protected void Start()
    {
        base.Start();
        maxSpeed = 1f;
        cooloffTime = 1f;
        currentSpeed = maxSpeed;
        rig = gameObject.GetComponent<Rigidbody2D>();
        damage = 10;
        facingRight = true;
        hitPoints = 25;
        isBurning = false;
        isGrounded = false;
        isScaredShitless = false;
        force = 200;

        sprite = transform.GetComponent<SpriteRenderer>();

        ground = 1 << 8;
        platform = 1 << 10;
        layers = ground | platform;
        
        player = Samurai.instance;
        offset = new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0);
    }

    override protected void Move()
    {
        if (!isHit)
        {
            if (isGrounded)
            {
                rig.velocity = new Vector2(5f * currentSpeed, rig.velocity.y);
                RaycastHit2D edgeFinder;
                RaycastHit2D obstacleFinder;
                
                obstacleFinder = Physics2D.Raycast((transform.position + offset), new Vector2((-1 + (2 * System.Convert.ToInt32(facingRight))),0), 0.1f, layers);
                edgeFinder = Physics2D.Raycast((transform.position + offset), Vector2.down, 1, layers);
                
                if (obstacleFinder.transform != null || edgeFinder.transform == null)
                {
                    Flip(facingRight);
                    offset = -offset;
                }
            }
        }
    }
}