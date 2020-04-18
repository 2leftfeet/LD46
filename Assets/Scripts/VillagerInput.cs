using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerInput : MonoBehaviour, IInput
{
    [SerializeField] float wanderRadius = 5.0f;
    [SerializeField] float minWaitTime = 1.0f;
    [SerializeField] float maxWaitTime = 4.0f;
    Vector3 startPosition;
    Vector3 targetPos;

    public float Horizontal {get; private set;}
    public float Vertical {get; private set;}

    bool isMoving = true;
    float waitTimer;

    void Awake()
    {
        startPosition = transform.position;
        targetPos = ChooseNewTargetPosition();
    }

    void Update()
    {
        if(isMoving)
        {
            Vector2 direction = targetPos - transform.position;
            if(direction.sqrMagnitude <= 0.01f)
            {
                isMoving = false;
                waitTimer = Random.Range(minWaitTime, maxWaitTime);
            }
            direction.Normalize();
            Horizontal = direction.x;
            Vertical = direction.y;
        }
        else
        {
            Horizontal = 0.0f;
            Vertical = 0.0f;
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0.0f)
            {
                isMoving = true;
                targetPos = ChooseNewTargetPosition();
            }
        }
    }

    Vector3 ChooseNewTargetPosition()
    {
        return startPosition + (Vector3)Random.insideUnitCircle * wanderRadius;
    }
}
