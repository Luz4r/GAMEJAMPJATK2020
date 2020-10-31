using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D p_rigidbody2D;
    float speed = 0;
    public float acceleration = 10;
    public float deceleration = 10;

    public float dashForce = 10;
    public float rememberDashedFor = 300;
    float lastTimeDashed = 0;

    public float jumpForce;
    public float maxSpeed = 7;
    public float timeToMaxSpeed = 5;
    float side;

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
        Move();
        Jump();
        BetterJump();
        Dash();
        CheckIfGrounded();
        CheckIfTouchingLeftWall();
        CheckIfTouchingRightWall();
    }

    void Dash()
    {

        if (Time.time - lastTimeDashed <= rememberDashedFor && Input.GetKeyDown(KeyCode.LeftShift))
        {
            float x = Input.GetAxisRaw("Horizontal");

            float velocity = x * dashForce;
            p_rigidbody2D.velocity = new Vector2(velocity, p_rigidbody2D.velocity.y);
            
            lastTimeDashed = Time.time;

        }
    }

        void Move() {
        float x = Input.GetAxisRaw("Horizontal");


        
        if (x < 0 && speed < maxSpeed && !isTouchingLeftWall)
        {
            speed = speed - acceleration * Time.deltaTime;
        }
        else if (x > 0 && speed > -maxSpeed && !isTouchingRightWall)
        {
            speed = speed + acceleration * Time.deltaTime;
        }
        else
        {
            if (speed > deceleration * Time.deltaTime)
            {
                speed = speed - deceleration * Time.deltaTime;
            }
            else if (speed < -deceleration * Time.deltaTime)
            {
                speed = speed + deceleration * Time.deltaTime;
            }
            else speed = 0;
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
