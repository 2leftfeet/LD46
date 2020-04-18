using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtAnimation : MonoBehaviour
{
    Animator animator;
    Camera camera;

    void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 mousePoint = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        Vector3 toMouse = mousePoint - transform.position;

        animator.SetFloat("inputX", toMouse.x);
        animator.SetFloat("inputY", toMouse.y);
    }

    
}
