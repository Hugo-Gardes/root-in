using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 1.0f;
    public float jumpSpeed = 20.0f;
    public float distToGround = 10.0f;
    public LayerMask groundLayer;
    public LayerMask objLayer;
    public bool doubleJump = true;
    public bool canjump_double = true;
    public LevelGeneration levelGeneration;
    public Rigidbody2D m_Rigidbody;
    public Vector2 starting_point;
    public LayerMask room;
    public int lives = 3;
    private float dir = 1;

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
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        dir *= -1.0f;
    }

    bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + (float)0.1, groundLayer);
        RaycastHit2D hito = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + (float)0.1, objLayer);
        return (hit.collider != null || hito.collider != null);
    }

    void LateUpdate() {
        Collider2D hit;
        bool is_pass = false;
        if (m_Rigidbody.constraints == (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation)) {
            if (!levelGeneration.stop) {
                hit = Physics2D.OverlapCircle(transform.position, 3f, room);
                gameObject.transform.position = hit.gameObject.transform.Find("spawn_player").transform.position;
                starting_point = gameObject.transform.position;
                m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                m_Rigidbody.AddForce(Vector3.up * jumpSpeed);
            } else {
                return;
            }
        }
        bool grounded = false;
        if (Input.GetKey("q")) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (dir != -1.0f)
                Rotate();
            animator.SetBool("walk", true);
            is_pass = true;
        }
        if (Input.GetKey("d")) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (dir != 1.0f)
                Rotate();
            animator.SetBool("walk", true);
            is_pass = true;
        }
        if (!is_pass) {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
        }
        if (Input.GetKey("space")) {
            grounded = IsGrounded();
            if (grounded || doubleJump) {
                if (!grounded && doubleJump) {
                    doubleJump = false;
                }
                m_Rigidbody.AddForce(Vector3.up * jumpSpeed);
            }
            animator.SetBool("jump", true);
        } else {
            animator.SetBool("jump", false);
        }
        if (canjump_double && !doubleJump) {
            if (IsGrounded()) {
                doubleJump = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "death") {
            gameObject.transform.position = starting_point;
            lives -= 1;
            Debug.Log(lives);
            if (lives == 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
