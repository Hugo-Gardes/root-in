using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpSpeed = 1.0f;
    public float distToGround = 10.0f;
    public LayerMask groundLayer;
    public bool doubleJump = true;
    public bool canjump_double = true;
    public LevelGeneration levelGeneration;
    public Rigidbody2D m_Rigidbody;

    bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + (float)0.1, groundLayer);
        return (hit.collider != null);
    }

    void Update() {
        if (m_Rigidbody.constraints == (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation)) {
            if (!levelGeneration.stop) {
                m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                m_Rigidbody.AddForce(Vector3.up * jumpSpeed);
            } else {
                return;
            }
        }
        bool grounded = false;
        if (Input.GetKey("z"))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey("q"))
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey("s"))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey("d"))
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey("space")) {
            grounded = IsGrounded();
            if (grounded || doubleJump) {
                if (!grounded && doubleJump) {
                    doubleJump = false;
                }
                m_Rigidbody.AddForce(Vector3.up * jumpSpeed);
            }
        }
        if (canjump_double && !doubleJump) {
            if (IsGrounded()) {
                doubleJump = true;
            }
        }
    }
}
