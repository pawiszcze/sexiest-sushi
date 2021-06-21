using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    public static Crouching instance;

    private MovementController movementController;
    private Samurai samurai;
    private float crouchSpeed = 5f;
    private Vector3 newScale;
    private bool canUncrouch;
    private SpriteRenderer bodySprite;
    private Vector2 bottomOffset;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        movementController = MovementController.instance;
        samurai = Samurai.instance;
        bodySprite = gameObject.GetComponent<SpriteRenderer>();
        bottomOffset = new Vector2(0, -bodySprite.size.y / 2);
        
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
        movementController.maxSpeed = crouchSpeed;
        newScale = samurai.bodyTransform.localScale;
        newScale.y = 0.5f;
        samurai.bodyTransform.localScale = newScale;
    }

    public void Uncrouch()
    {
        canUncrouch = !Physics2D.OverlapBox((Vector2)transform.position - 1.5f * bottomOffset, new Vector2(bodySprite.size.x - 0.05f, bodySprite.size.y / 2), 0, 1 << 8);
        if (canUncrouch)
        {
            movementController.maxSpeed = movementController.walkSpeed;
            newScale = samurai.bodyTransform.localScale;
            newScale.y = 1f;
            samurai.bodyTransform.localScale = newScale;
        }
    }
}
