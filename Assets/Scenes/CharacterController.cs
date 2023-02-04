using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator animator;
    public float speed = 1.0f;
    public float jumpSpeed = 1.0f;
    public float distToGround = 10.0f;
    public LayerMask groundLayer;
    public bool doubleJump = true;
    public bool canjump_double = true;

    public void Hit()
    {
        animator.SetBool("hit", true);
    }

    public void Heal()
    {
        animator.SetBool("hit", false);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + (float)0.1, groundLayer);
        return (hit.collider != null);
    }

    public void Update()
    {
        bool grounded = false;
        if (Input.GetKey("q"))
        {
            animator.SetBool("walk", true);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey("d"))
        {
            animator.SetBool("walk", true);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        } else
        {
            animator.SetBool("walk", true);
            animator.SetBool("run", true);
        }

        if (Input.GetKey("space"))
        {
            grounded = IsGrounded();
            if (grounded || doubleJump)
            {
                if (!grounded && doubleJump)
                {
                    doubleJump = false;
                }
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpSpeed);
            }
        }
        if (canjump_double && !doubleJump)
        {
            if (IsGrounded())
            {
                doubleJump = true;
            }
        }
    }
}
