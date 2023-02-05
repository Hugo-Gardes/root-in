using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageBoss : MonoBehaviour
{
    public Animator animator;
    public float time = 3;
    public float time_attack = 0.5f;
    public int health;
    public bool do_attack = false;
    public bool return_center = false;
    public float speed;
    public GameObject laser;
    // Start is called before the first frame update
    public void Start()
    {
        animator = GetComponent<Animator>();
        health = animator.GetInteger("health");
        laser.SetActive(false);
    }

    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (time > 0 && do_attack == false)
        {
            time -= Time.deltaTime;
            speed = Random.Range(-0.018f, 0.018f);
        }
        else
        {
            do_attack = true;
            time = 3;
            if (health > 1000)
            {
                do_phase_one();
            }
            else if (health > 500)
            {
                do_phase_two();
            }
            else
            {
                do_phase_three();
            }
        }
    }

    public void do_phase_one()
    {
        if (return_center == false)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y + 0.013f, transform.position.z);
            laser.SetActive(false);
        }
        else if (time_attack <= 0)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y - 0.013f, transform.position.z);
            laser.SetActive(false);
        }
        else
        {
            time_attack -= Time.deltaTime;
        }
        if (transform.position.y > 4.34f)
        {
            return_center = true;
            laser.SetActive(true);
        }
        else if (transform.position.x < 0 && transform.position.y < 0)
        {
            return_center = false;
            animator.SetInteger("health", health - 100);
            health = animator.GetInteger("health");
            do_attack = false;
            time_attack = 0.5f;
        }
    }

    public void do_phase_two()
    {
        if (return_center == false)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y + 0.013f, transform.position.z);
            laser.SetActive(false);
        }
        else if (time_attack <= 0)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y - 0.013f, transform.position.z);
            laser.SetActive(false);
        }
        else
        {
            time_attack -= Time.deltaTime;
        }
        if (transform.position.y > 4.34f)
        {
            return_center = true;
            laser.SetActive(true);
        }
        else if (transform.position.x < 0 && transform.position.y < 0)
        {
            return_center = false;
            animator.SetInteger("health", health - 100);
            health = animator.GetInteger("health");
            do_attack = false;
            time_attack = 0.5f;
        }
    }

    public void do_phase_three()
    {
        if (return_center == false)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y + 0.013f, transform.position.z);
            laser.SetActive(false);
        }
        else if (time_attack <= 0)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y - 0.013f, transform.position.z);
            laser.SetActive(false);
        }
        else
        {
            time_attack -= Time.deltaTime;
        }
        if (transform.position.y > 4.34f)
        {
            return_center = true;
            laser.SetActive(true);
        }
        else if (transform.position.x < 0 && transform.position.y < 0)
        {
            return_center = false;
            animator.SetInteger("health", health - 100);
            health = animator.GetInteger("health");
            do_attack = false;
            time_attack = 0.5f;
        }
    }
}
