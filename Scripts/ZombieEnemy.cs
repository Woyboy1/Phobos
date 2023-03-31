using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{

    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationWalk();
    }


    void Update()
    {
        
    }

    public void AnimationWalk()
    {
        animator.SetBool("iswalking", true);
    }
}
