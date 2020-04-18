using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAnimation : MonoBehaviour
{
    Animator animator;
    Camera camera;
    VillagerInput input;

    void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        input = GetComponent<VillagerInput>();
    }

    void Update()
    {
        animator.SetFloat("inputX", input.targetPos.x);
        animator.SetFloat("inputY", input.targetPos.y);
    }

}
