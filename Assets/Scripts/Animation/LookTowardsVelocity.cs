using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsVelocity : MonoBehaviour
{
    Animator animator;
    Camera camera;
    Rigidbody2D body;

    void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(body.velocity.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("inputX", body.velocity.x);
            animator.SetFloat("inputY", body.velocity.y);
        }
    }

    
}