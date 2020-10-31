using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Direction keyPressed;
    Direction currentMovementDirection;
    
    Rigidbody2D p_rigidbody2D;
    float speed = 0;
    public float acceleration = 10;
    public float deceleration = 10;
    

    public float jumpForce;
    public float maxSpeed = 7;

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius = 0.05f;
    public LayerMask groundLayer;

    bool isTouchingLeftWall = false;
    bool isTouchingRightWall = false;
    public float checkWallRadius = 0.05f;
    public Transform isTouchingLeftWallChecker;
    public Transform isTouchingRightWallChecker;
    public LayerMask wallLayer;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    public int defaultAdditionalJumps = 1;
    int additionalJumps;

    // Start is called before the first frame update
    void Start()
    {
        p_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetKeyPressed();
        SetCurrentMovementDirection();
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
        CheckIfTouchingLeftWall();
        CheckIfTouchingRightWall();
    }

    void SetCurrentMovementDirection() {
        float currentSpeed = p_rigidbody2D.velocity.x;

        if (currentSpeed > 0)
        {
            currentMovementDirection = Direction.Right;
        }
        else if (currentSpeed < 0) {
            currentMovementDirection = Direction.Left;
        } else
        {
            currentMovementDirection = Direction.None;
        }
    }
    void SetKeyPressed() {
        float x = Input.GetAxisRaw("Horizontal");

        if (x > 0)
        {
            keyPressed = Direction.Right;
        }
        else if (x < 0)
        {
            keyPressed = Direction.Left;
        }
        else {
            keyPressed = Direction.None;
        }
    }


    void Move() {

        if (keyPressed == Direction.Left)
        {
            if (currentMovementDirection == Direction.Left || currentMovementDirection == Direction.None)
            {
                if (!isTouchingLeftWall)
                {
                    if (speed > -maxSpeed)
                    {
                        speed -= acceleration * Time.deltaTime;
                    }
                }
            }
            else if (currentMovementDirection == Direction.Right) {
                speed -= deceleration * Time.deltaTime;
            }
        }

        else if (keyPressed == Direction.Right)
        {
            if (currentMovementDirection == Direction.Right || currentMovementDirection == Direction.None)
            {
                if (!isTouchingRightWall)
                {
                    if (speed < maxSpeed)
                    {
                        speed += acceleration * Time.deltaTime;
                    }
                }
            }
            else if (currentMovementDirection == Direction.Left)
            {
                speed += deceleration * Time.deltaTime;
            }
        }

        else if (keyPressed == Direction.None) {
            if (currentMovementDirection == Direction.Left)
            {
                speed += deceleration * Time.deltaTime;
            }
            else if (currentMovementDirection == Direction.Right)
            {
                speed -= deceleration * Time.deltaTime;
            }
        }
        
        if (isTouchingRightWall && speed > 0) {
            speed = 0;
        }
        if (isTouchingLeftWall && speed < 0) {
            speed = 0;
        }
        p_rigidbody2D.velocity = new Vector2(speed, p_rigidbody2D.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0))
        {
            p_rigidbody2D.velocity = new Vector2(p_rigidbody2D.velocity.x, jumpForce);
            additionalJumps--;
        }
    }
    void BetterJump()
    {
        if (p_rigidbody2D.velocity.y < 0)
        {
            p_rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (p_rigidbody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            p_rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void CheckIfTouchingLeftWall() {
        Collider2D colliders = Physics2D.OverlapCircle(isTouchingLeftWallChecker.position, checkWallRadius, wallLayer);

        if (colliders != null)
        {
            isTouchingLeftWall = true;
        }
        else
        {
            isTouchingLeftWall = false;
        }
    }

    void CheckIfTouchingRightWall() {
        Collider2D colliders = Physics2D.OverlapCircle(isTouchingRightWallChecker.position, checkWallRadius, wallLayer);
        if (colliders != null)
        {
            isTouchingRightWall = true;
        }
        else
        {
            isTouchingRightWall = false;
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

}
