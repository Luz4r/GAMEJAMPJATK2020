using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float idleSpeed = 5f;
    public float aggroSpeed = 10f;
    public float patrolRange = 10f;
    public float jumpForce = 5f;

    private float currentSpeed;
    private Rigidbody2D rb;
    private Vector2 vecMovement;
    private GameObject player;
    private bool isAggro;
    private float xMovementLimitRight;
    private float xMovementLimitLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        isAggro = false;
        vecMovement = Vector2.one;
        xMovementLimitRight = transform.position.x + patrolRange;
        xMovementLimitLeft = transform.position.x;
        currentSpeed = idleSpeed;
    }

    void FixedUpdate()
    {
        if (isAggro)
        {
            FollowPlayer();
        }
        else
        {
            Idle();
        }

        rb.velocity = new Vector2(vecMovement.x * currentSpeed, rb.velocity.y);
    }

    private void Idle()
    {
        if(transform.position.x > xMovementLimitRight || transform.position.x < xMovementLimitLeft)
        {
            vecMovement *= -1;
        }
    }

    private void FollowPlayer()
    {
        vecMovement = new Vector2(player.transform.position.x - transform.position.x, 0.0f).normalized;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);    //TODO zawraca po skoku
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isAggro = true;
            currentSpeed = aggroSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAggro = false;
            currentSpeed = idleSpeed;
            vecMovement = Vector2.one;
            xMovementLimitRight = transform.position.x + patrolRange;
            xMovementLimitLeft = transform.position.x;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !isAggro)
        {
            vecMovement *= -1;
        }
    }
}
