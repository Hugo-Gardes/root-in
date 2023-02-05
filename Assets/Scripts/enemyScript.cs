using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public Animator animator;
    public float speed = 3f;
    public string enemitag;
    private float direction;
    private Vector3 localScale;
    private bool facingRight = false;
    public float distToGround = 1f;
    public float distToWall = 1f;
    public float jumpingPower = 0f;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask wallLayer;

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.tag == enemitag)
            animator.SetBool("isDead", true);
            speed = 0f;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        direction = -1f;
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(direction * speed, rigidBody.velocity.y);
        if (IsWall()) {
            print("ici");
            direction *= -1f;
        }
        print(direction);
        if (IsGrounded())
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        if (rigidBody.velocity.y > 0f)
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);

        if (animator.GetBool("isEnded"))
            Destroy(gameObject);
    }

    void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (direction > 0)
            facingRight = true;
        else if (direction < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    bool IsWall() {
        Debug.DrawRay(transform.position, Vector3.left * direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left * -direction, distToWall + (float)0.1, wallLayer);
        print(hit.collider);
        return (hit.collider != null);
    }

    bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + (float)0.1, groundLayer);
        return (hit.collider != null);
    }

}
