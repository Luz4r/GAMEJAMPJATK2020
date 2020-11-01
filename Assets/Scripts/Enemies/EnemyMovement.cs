using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float idleSpeed = 5f;
    public float aggroSpeed = 10f;
    public float patrolRange = 10f;
    public float jumpForce = 5f;

    [HideInInspector]
    public bool isAggro;

    private float currentSpeed;
    private Rigidbody2D rb;
    private Vector2 vecMovement;
    private GameObject player;
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isAggro = true;
            currentSpeed = aggroSpeed;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
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
        
        if(collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<HealthSystem>().DealDamage(10f);
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            playerRB.velocity = new Vector2(playerRB.velocity.x + 20f, playerRB.velocity.y + 20f);
        }
    }
}
