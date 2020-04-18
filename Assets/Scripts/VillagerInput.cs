using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerInput : MonoBehaviour, IInput
{
    [SerializeField] float wanderRadius = 2.0f;
    [SerializeField] float minWaitTime = 1.0f;
    [SerializeField] float maxWaitTime = 4.0f;
    [SerializeField] LayerMask charactersMask = default;
    [SerializeField] float localAvoidanceSearchRange = 2.0f;
    [SerializeField] float localAvoidanceFactor = 0.3f;
    Vector3 startPosition;
    Transform possessTarget;

    public Vector3 targetPos{get; private set;}
    public float Horizontal {get; private set;}
    public float Vertical {get; private set;}

    bool isMoving = true;
    bool useLocalAvoidance = false;
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
            MoveToTarget();
            if(useLocalAvoidance)
            {
                LocalAvoidance();
            }
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

    void MoveToTarget()
    {
        if(possessTarget)
        {
            targetPos = possessTarget.position;
        }
        
        Vector2 direction = targetPos - transform.position;
        if(!possessTarget && direction.sqrMagnitude <= 0.01f)
        {
            isMoving = false;
            waitTimer = Random.Range(minWaitTime, maxWaitTime);
        }
        direction.Normalize();
        Horizontal = direction.x;
        Vertical = direction.y;
        
    }

    void LocalAvoidance()
    {
        Collider2D[] foundVillagers = Physics2D.OverlapCircleAll(transform.position, localAvoidanceSearchRange, charactersMask);
        for(int i = 0; i < foundVillagers.Length; ++i)
        {
            Vector3 awayFromVillager = transform.position - foundVillagers[i].transform.position;
            float multiplier = localAvoidanceFactor / foundVillagers.Length;
            awayFromVillager = awayFromVillager.normalized * multiplier;

            Horizontal += awayFromVillager.x;
            Vertical += awayFromVillager.y;
        }
    }

    public void Possess(Transform newTarget)
    {
        possessTarget = newTarget;
        useLocalAvoidance = true;
        isMoving = true;
        waitTimer = 0.0f;
    }

    Vector3 ChooseNewTargetPosition()
    {
        return startPosition + (Vector3)Random.insideUnitCircle * wanderRadius;
    }
}
