using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool canMove = true;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    Vector2 move;

    // Start is called before the first frame update
    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            move = Vector2.zero;

            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
            return;
        }
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        move = move.normalized;
        bool isMoving = move.magnitude > 0;

        if(animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }

        if (spriteRenderer != null)
        {
            if (move.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (move.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = move * speed;
    }
}
