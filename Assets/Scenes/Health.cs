using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator animator;

    private int MAX_HEALTH = 100;
    [SerializeField] private int health = 100;

    private bool is_dead = false;
    private float timeToDestroy = 0.65f;
    private float timerDestroy = 0f;
    
    private bool is_damaged = false;
    private float timeDamaged = 0.4f;
    private float timerDamaged = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Heal(10);
        }

        if (is_dead)
        {
            this.timerDestroy += Time.deltaTime;

            if (timerDestroy >= timeToDestroy)
            {
                Destroy(gameObject); 
            }
        } else if (is_damaged)
        {
            this.timerDamaged += Time.deltaTime;

            if (timerDamaged >= timeDamaged)
            {
                this.timerDamaged = 0;
                this.is_damaged = false;
                this.animator.SetBool("hit", is_damaged);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Can't have negative damage");
        }

        if (!is_damaged)
        {
            this.health -= amount;
            this.is_damaged = true;
            this.animator.SetBool("hit", is_damaged);

            if (health <= 0)
            {
                Kill();
            }
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Can't have negative healing");
        }

        if (health + amount > MAX_HEALTH)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }

    public void Kill()
    {
        this.is_dead = true;
        this.animator.SetBool("death", is_dead);
    }
}
