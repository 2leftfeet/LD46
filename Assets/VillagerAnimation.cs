using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAnimation : MonoBehaviour
{
    Animator animator;
    Camera camera;
    VillagerAI input;

    void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        input = GetComponent<VillagerAI>();
    }

    void Update()
    {
        Vector2 lookDir = input.targetPos - transform.position;
        animator.SetFloat("inputX", lookDir.x);
        animator.SetFloat("inputY", lookDir.y);
    }

}
