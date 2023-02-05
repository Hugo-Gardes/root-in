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
    public GameObject bullet;
    public float direction_bullet_x;
    public float direction_bullet_y;
    public bool negative_x = false;
    public bool negative_y = false;
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
            speed = Random.Range(-0.022f, 0.022f);
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
                bullet.SetActive(true);
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
            transform.position = new Vector3(transform.position.x + speed, transform.position.y + 0.016f, transform.position.z);
            laser.SetActive(false);
        }
        else if (time_attack <= 0)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y - 0.016f, transform.position.z);
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
        else if (transform.position.y < 0)
        {
            return_center = false;
            animator.SetInteger("health", health - 250);
            health = animator.GetInteger("health");
            do_attack = false;
            time_attack = 0.5f;
        }
    }

    public void do_phase_two()
    {
        float speed_x = Random.Range(0f, 0.022f);
        float speed_y = Random.Range(0f, 0.022f);
        if (negative_x == true)
        {
            speed_x = -speed_x;
        }
        if (negative_y == true)
        {
            speed_y = -speed_y;
        }
        if (transform.position.x + speed_x > 7f)
        {
            negative_x = true;
        }
        else if (transform.position.x + speed_x < -7f)
        {
            negative_x = false;
        }
        if (transform.position.y + speed_y > 4.34f)
        {
            negative_y = true;
        }
        else if (transform.position.y + speed_y < -4.34f)
        {
            negative_y = false;
        }
        transform.position = new Vector3(transform.position.x + speed_x, transform.position.y + speed_y, transform.position.z);
        if (time_attack <= 0)
        {
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            direction_bullet_x = Random.Range(-0.12f, 0.12f);
            direction_bullet_y = Random.Range(0, 1);
            if (direction_bullet_y == 0 && direction_bullet_x < 0)
            {
                direction_bullet_y = (0.12f + direction_bullet_x) * -1;
            }
            else if (direction_bullet_y == 0 && direction_bullet_x >= 0)
            {
                direction_bullet_y = 0.12f - direction_bullet_x;
            }
            else if (direction_bullet_y == 1 && direction_bullet_x < 0)
            {
                direction_bullet_y = (0.12f + direction_bullet_x) * -1;
            }
            else if (direction_bullet_y == 1 && direction_bullet_x >= 0)
            {
                direction_bullet_y = 0.12f - direction_bullet_x;
            }
            bullet.transform.position = new Vector3(bullet.transform.position.x + direction_bullet_x, bullet.transform.position.y + direction_bullet_y, transform.position.z);
            time_attack = 3.5f;
            animator.SetInteger("health", health - 50);
            health = animator.GetInteger("health");
        }
        else if (bullet.activeSelf == false)
        {
            time_attack -= Time.deltaTime;
        }
        else
        {
            time_attack -= Time.deltaTime;
            bullet.transform.position = new Vector3(bullet.transform.position.x + direction_bullet_x, bullet.transform.position.y + direction_bullet_y, transform.position.z);
        }
    }

    public void do_phase_three()
    {
        animator.SetInteger("health", 0);
        Destroy(gameObject);
    }
}
