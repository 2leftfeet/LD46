using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5.0f;
    [SerializeField] float maxAcceleration = 1.0f;

    IInput input;
    Rigidbody2D body;
    Vector2 targetVelocity;

    void Awake()
    {
        input = GetComponent<IInput>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        targetVelocity = new Vector2(input.Horizontal, input.Vertical);
        targetVelocity = Vector2.ClampMagnitude(targetVelocity, 1.0f);

        targetVelocity *= maxSpeed;
    }

    void FixedUpdate()
    {
        Vector2 velocity = body.velocity;

        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(body.velocity.x, targetVelocity.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(body.velocity.y, targetVelocity.y, maxSpeedChange);

        body.velocity = velocity;
    }

    
}
