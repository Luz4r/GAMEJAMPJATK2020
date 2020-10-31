using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D p_rigidbody2D;

    public int defaultAdditionalJumps = 1;
    public float acceleration = 20;
    public float deceleration = 100;
    public float maxSpeed = 30;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float dashForce = 100;
    public float jumpForce = 12;
    public float checkerRadius = 0.05f;
    public float rememberGroundedFor = 0.2f;
    public float rememberDashedFor = 0.2f;
    public Transform isGroundedChecker;
    public Transform isTouchingLeftWallChecker;
    public Transform isTouchingRightWallChecker;
    public LayerMask checkLayer;

    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public Direction keyPressed;
    public Direction playerSide = Direction.Right;

    int additionalJumps;
    float lastTimeGrounded;
    float lastTimeDashed;
    float speed = 0;
    bool isTouchingLeftWall = false;
    bool isTouchingRightWall = false;
    Direction currentMovementDirection;

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
      //  Dash();
        Jump();
        BetterJump();
        CheckIfGrounded();
        CheckIfTouchingLeftWall();
        CheckIfTouchingRightWall();
    }


    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (keyPressed == Direction.Left)
            {
                speed -= dashForce;
            }
            else
            if (keyPressed == Direction.Right)
            {
                speed += dashForce;
            }
        }
    }

    void SetCurrentMovementDirection()
    {
        float currentSpeed = p_rigidbody2D.velocity.x;

        if (currentSpeed > 0)
        {
            currentMovementDirection = Direction.Right;
        }
        else if (currentSpeed < 0)
        {
            currentMovementDirection = Direction.Left;
        }
        else
        {
            currentMovementDirection = Direction.None;
        }
    }

    void SetKeyPressed()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x > 0)
        {
            keyPressed = Direction.Right;
            playerSide = Direction.Right;

        }
        else if (x < 0)
        {
            keyPressed = Direction.Left;
            playerSide = Direction.Left;
        }
        else
        {
            keyPressed = Direction.None;
        }
    }

    
    void Move()
    {

        if (keyPressed == Direction.Left)
        {
            if (currentMovementDirection == Direction.Left || currentMovementDirection == Direction.None)
            {
                if (!isTouchingLeftWall)
                {
                    if (speed > -maxSpeed)
                    {
                        speed = speed - acceleration * Time.deltaTime;
                    }
                }
            }
            else if (currentMovementDirection == Direction.Right)
            {
                speed = speed - deceleration * Time.deltaTime;
            }
        }

        else

        if (keyPressed == Direction.Right)
        {
            if (currentMovementDirection == Direction.Right || currentMovementDirection == Direction.None)
            {
                if (!isTouchingRightWall)
                {
                    if (speed < maxSpeed)
                    {
                        speed = speed + acceleration * Time.deltaTime;
                    }
                }
            }
            else if (currentMovementDirection == Direction.Left)
            {
                speed = speed + deceleration * Time.deltaTime;
            }
        }

        else

        if (keyPressed == Direction.None)
        {
            if (currentMovementDirection == Direction.Left)
            {
                speed = speed + deceleration * Time.deltaTime;

                if (speed > -1)
                {
                    speed = 0;
                }
            }
            else if (currentMovementDirection == Direction.Right)
            {
                speed = speed - deceleration * Time.deltaTime;

                if (speed < 1)
                {
                    speed = 0;
                }
            }
        }

        if (isTouchingRightWall && speed > 0)
        {
            speed = 0;
        }
        if (isTouchingLeftWall && speed < 0)
        {
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

    void CheckIfTouchingLeftWall()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isTouchingLeftWallChecker.position, checkerRadius, checkLayer);

        if (colliders != null)
        {
            isTouchingLeftWall = true;
        }
        else
        {
            isTouchingLeftWall = false;
        }
    }

    void CheckIfTouchingRightWall()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isTouchingRightWallChecker.position, checkerRadius, checkLayer);
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
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkerRadius, checkLayer);
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