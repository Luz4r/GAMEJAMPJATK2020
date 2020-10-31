using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    SpriteRenderer p_spriteRenderer;
    PlayerMovement p_playerMovement;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        p_spriteRenderer = GetComponent<SpriteRenderer>();
        p_playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(p_playerMovement.keyPressed != Direction.None)
        {
            anim.SetBool("Move", true);
        }

        if (!p_playerMovement.isGrounded)
        {
            anim.SetBool("Jump", true);
        }
        if(p_playerMovement.isGrounded)
        {
            anim.SetBool("Jump", false);
        }

        switch(p_playerMovement.keyPressed)
        {
            case Direction.Left:
                p_spriteRenderer.flipX = true;
                break;
            case Direction.Right:
                p_spriteRenderer.flipX = false;
                break;
            case Direction.None:
                anim.SetBool("Move", false);
                break;
        }
    }
}