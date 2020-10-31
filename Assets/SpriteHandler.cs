using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    SpriteRenderer p_spriteRenderer;
    PlayerMovement p_playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        p_spriteRenderer = GetComponent<SpriteRenderer>();
        p_playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p_playerMovement.keyPressed == Direction.Left)
        {
            p_spriteRenderer.flipX = true;
        }
        else if (p_playerMovement.keyPressed == Direction.Right)
        {
            p_spriteRenderer.flipX = false;
        }
    }
}