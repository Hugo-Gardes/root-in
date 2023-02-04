using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Animator animator;

    public void Hit()
    {
        animator.SetBool("hit", true);
    }

    public void Heal()
    {
        animator.SetBool("hit", false);
    }
}
