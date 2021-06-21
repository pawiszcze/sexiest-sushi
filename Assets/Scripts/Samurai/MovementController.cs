using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public static MovementController instance;
    public float maxSpeed;
    public float walkSpeed = 9.5f;
    public bool canControl;
    public Rigidbody2D rig;
    float move = 0;
    bool facingRight;

    private void Awake(){
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Start(){
        canControl = true; 
        facingRight = true;     
        maxSpeed = walkSpeed;
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate(){
        if (rig.velocity.x < maxSpeed)
        {
            rig.velocity = new Vector2(move * maxSpeed, rig.velocity.y);
        }
        else if (rig.velocity.x > maxSpeed)
        {
            rig.velocity = new Vector2(-move * maxSpeed, rig.velocity.y);
        }
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.A)){
            move = -1;
        }

        if (Input.GetKeyDown(KeyCode.D)){
            move = 1;
        }

        if (Input.GetKeyUp(KeyCode.D)){
            if (Input.GetKey(KeyCode.A)){
                move = -1;
            } else{
                move = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.A)){
            if (Input.GetKey(KeyCode.D)){
                move = 1;
            }
            else{
                move = 0;
            }
        }

        if (move < 0 && facingRight == true) { Flip(true); }
        if (move > 0 && facingRight == false) { Flip(false); }
    }

    void Flip(bool i){
        int flip = Convert.ToInt32(i);
        Vector3 newScale = this.gameObject.transform.localScale;
        newScale.x *= -1;
        this.gameObject.transform.localScale = newScale;
        facingRight = !i;
    }
}