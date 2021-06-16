using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{

    public float fallMultiplier;
    public float lowFallMultiplier;
    public float maxSpeed;
    public bool canControl, isFalling, isClimbing, isCrouched;
    public bool goingDown;
    public Rigidbody2D rig;
    float vectX;
    float move = 0;
    float walkSpeed = 9.5f;
    bool directionChanged;
    bool facingRight;
    char lastKeyPressed;
    SpriteRenderer flashSprite; 
    SpriteRenderer bodySprite; 
    SpriteRenderer spearSprite;
    Transform flash; 

    public void Start()
    {
        lastKeyPressed = ' ';
        canControl = true; 
        facingRight = true;
        fallMultiplier = 5f;
        directionChanged = false;
        lowFallMultiplier = 7.5f;
        maxSpeed = walkSpeed;
        flash = transform.GetChild(4);
        flashSprite = flash.transform.GetComponent<SpriteRenderer>();
        vectX = flashSprite.size.x * flash.transform.localScale.x;
        rig = gameObject.GetComponent<Rigidbody2D>(); 
        isClimbing = false;
        goingDown = false;
        spearSprite = Spear.instance.gameObject.GetComponent<SpriteRenderer>();
        bodySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {
        if (rig.velocity.x < maxSpeed)
        {
            rig.velocity = new Vector2(move * maxSpeed, rig.velocity.y);
        }

        if (isFalling)
        {
            if (rig.velocity.y < 0)
            {
                rig.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * (fallMultiplier - 1);
            }
            else if (rig.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && !goingDown)
            {
                rig.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * (lowFallMultiplier - 1);
            }
        }
        else if (!isClimbing)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
        }
    }

    public void Update()
    {
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            lastKeyPressed = 'D';
            Debug.Log("Going right");
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            lastKeyPressed = 'A'; Debug.Log("Going left");
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            lastKeyPressed = ' ';
        }

        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) || (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)))
        {
            directionChanged = false;
        }

        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            lastKeyPressed = ' ';
        }

        if (lastKeyPressed == 'A')
        {
            move = -1;
        }
        else if (lastKeyPressed == 'D')
        {
            move = 1;
        }
        else
        {
            move = 0;
        }

        if (move < 0 && facingRight == true) { Flip(true); }
        if (move > 0 && facingRight == false) { Flip(false); }
    }

    void Flip(bool i)
    {
        int flip = Convert.ToInt32(i);
        bodySprite.flipX = i;
        spearSprite.flipX = i;                                                                                                                            //CHECK AFTER ROTATION IMPLEMENTED
        flashSprite.flipX = i;
        flash.transform.localPosition = new Vector3(flash.transform.localPosition.x + vectX - (flip * 2 * vectX), flash.transform.localPosition.y, flash.transform.localPosition.z);
        facingRight = !i;
    }
}
