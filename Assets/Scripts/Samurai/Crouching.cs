using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    private MovementController movementController;
    private Samurai samurai;
    private float crouchSpeedMultiplier = 0.5f;
    private Vector3 newScale;

    void Start()
    {
        movementController = MovementController.instance;
        samurai = Samurai.instance;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Crouch();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Uncrouch();
        }
    }

    void Crouch()
    {
        movementController.maxSpeed *= crouchSpeedMultiplier;
        newScale = samurai.bodyTransform.localScale;
        newScale.y = 0.5f;
        samurai.bodyTransform.localScale = newScale;
    }

    void Uncrouch()
    {
        movementController.maxSpeed = movementController.walkSpeed;
        newScale = samurai.bodyTransform.localScale;
        newScale.y = 1f;
        samurai.bodyTransform.localScale = newScale;
    }
}
