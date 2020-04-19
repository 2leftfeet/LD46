using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesheetAnimator : MonoBehaviour
{
    Animator animator;
    Camera camera;
    IHasTarget target;

    void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        target = GetComponent<IHasTarget>();
    }

    void Update()
    {
        Vector2 lookDir = target.GetPoint() - transform.position;
        animator.SetFloat("inputX", lookDir.x);
        animator.SetFloat("inputY", lookDir.y);
    }

}
