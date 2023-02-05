using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    public float distToGround = 1f;
    public float distToWall = 1f;
    private float direction;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask wallLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && IsWall())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }

        Flip();
    }

    bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + (float)0.1, groundLayer);
        return (hit.collider != null);
    }

    bool IsWall() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left * -direction, distToWall + (float)0.1, wallLayer);
        return (hit.collider != null);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(horizontal * speed, rigidBody.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // private bool OnFeet()
    // {
    //     return Physics2D.OverlapCircle(feetCheck.position, 0.2f, groundLayer);
    // }
}
