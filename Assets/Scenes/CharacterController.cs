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

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundLayer);
        return (hit.collider != null);
    }

    void Update()
    {
        speed = 1.0f;
        Debug.Log("update");

        if (Input.GetKey("q"))
        {
        }
        else if (Input.GetKey("d"))
        {
        } else
        {
        }

        if (Input.GetKey("space") && IsGrounded())
        {
            
        } else
        {
            animator.SetBool("jump", false);
        }
    }
}
