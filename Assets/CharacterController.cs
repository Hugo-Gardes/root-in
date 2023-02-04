using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    public float speed = 1.0f;
    public float jumpSpeed = 20.0f;
    // public float distToGround = 10.0f;
    // public LayerMask groundLayer;
    // public bool doubleJump = true;
    // public bool canjump_double = true;

    public void Hit()
    {
        animator.SetBool("hit", true);
    }

    public void Heal()
    {
        animator.SetBool("hit", false);
    }

    /* bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + (float)0.1, groundLayer);
        return (hit.collider != null);
    } */

    void Update()
    {
        // bool grounded = false;
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
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
        }

        if (Input.GetKey("space"))
        {
            animator.SetBool("jump", true);
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpSpeed * Time.deltaTime);
            /*grounded = IsGrounded();
            if (grounded || doubleJump)
            {
                if (!grounded && doubleJump)
                {
                    doubleJump = false;
                }
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpSpeed);
            }*/
        } else
        {
            animator.SetBool("jump", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("attack", true);
        }
        /*if (canjump_double && !doubleJump)
        {
            if (IsGrounded())
            {
                doubleJump = true;
            }
        }*/
    }
}
