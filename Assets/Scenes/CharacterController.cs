using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    public float speed = 1.0f;
    public float jumpSpeed = 2.0f;
    public float dir = 1.0f;
    public float distToGround = 0.8f;
    public LayerMask groundLayer;

    public void Hit()
    {
        animator.SetBool("hit", true);
    }

    public void Heal()
    {
        animator.SetBool("hit", false);
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * 180);
        dir *= -1.0f;
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundLayer);
        return (hit.collider != null);
    }

    void Update()
    {
        speed = 1.0f;

        if (Input.GetKey("q"))
        {
            if (dir != -1.0f)
                this.Rotate();
            if (Input.GetKey("left shift"))
            {
                animator.SetBool("run", true);
                speed = 2.0f;
            } else
                animator.SetBool("run", false);
            animator.SetBool("walk", true);
            transform.Translate(Vector3.left * speed * dir * Time.deltaTime);
        }
        else if (Input.GetKey("d"))
        {
            if (dir != 1.0f)
                this.Rotate();
            if (Input.GetKey("left shift"))
            {
                animator.SetBool("run", true);
                speed = 2.0f;
            }
            else
                animator.SetBool("run", false);
            animator.SetBool("walk", true);
            transform.Translate(Vector3.right * speed * dir * Time.deltaTime);
        } else
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
        }

        if (Input.GetKey("space") && IsGrounded())
        {
            animator.SetBool("jump", true);
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpSpeed);
        } else
        {
            animator.SetBool("jump", false);
        }
    }
}
