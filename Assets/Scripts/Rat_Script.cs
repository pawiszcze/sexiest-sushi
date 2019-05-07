using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_Script : FoeScript
{
    private void Awake()
    {
        expValue = 10;
    }

    void Start()
    {
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

        currentSpeed = maxSpeed;
        player = Samurai_Script.instance;
    }

    override protected void Move()
    {
        if (!isHit) {
            if (isGrounded)
            {
                rig.velocity = new Vector2(5f * currentSpeed, rig.velocity.y);
                RaycastHit2D hit;
                RaycastHit2D obstacleFinder;
                if (facingRight)
                {
                    obstacleFinder = Physics2D.Raycast((transform.position + new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.right, 0.1f, 1 << 8);
                    hit = Physics2D.Raycast((transform.position + new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, 1, 1 << 8);
                    Debug.DrawRay((transform.position + new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.right, Color.blue);
                    Debug.DrawRay((transform.position + new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, Color.red);
                }
                else
                {
                    obstacleFinder = Physics2D.Raycast((transform.position - new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.left, 0.1f, 1 << 8);
                    hit = Physics2D.Raycast((transform.position - new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, 1, 1 << 8);
                    Debug.DrawRay((transform.position - new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, Color.red);
                }

                if (hit.transform == null)
                {
                    if (facingRight)
                    {
                        hit = Physics2D.Raycast((transform.position + new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, 1, 1 << 10);
                        Debug.DrawRay((transform.position + new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, Color.red);
                    }
                    else
                    {
                        hit = Physics2D.Raycast((transform.position - new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, 1, 1 << 10);
                        Debug.DrawRay((transform.position - new Vector3(GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 0, 0)), Vector2.down, Color.red);
                    }
                }

                if(obstacleFinder.transform != null)
                {
                    Flip(facingRight);
                }

                if (hit.transform == null)
                {
                    Flip(facingRight);
                }
            }
            else
            {
                //rig.velocity = new Vector2(0, rig.velocity.y);
            }
        }
    }

}


