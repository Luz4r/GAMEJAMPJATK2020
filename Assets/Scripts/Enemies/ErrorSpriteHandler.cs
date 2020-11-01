using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorSpriteHandler : MonoBehaviour
{
    private EnemyMovement enemyMov;
    private Animator anim;
    private void Start()
    {
        enemyMov = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("Aggro", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Aggro", false);
        }
    }
}
