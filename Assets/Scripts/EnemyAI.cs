using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy AI that pseudorandomly moves closer to player
/// </summary>
public class EnemyAI : MonoBehaviour, IInput, IHasTarget
{
    public enum State {Idle, Moving, Attacking}
    [SerializeField]
    private State state;

    [SerializeField] float wanderRadius = 5.0f;
    [SerializeField] float minWaitTime = 5.0f;
    [SerializeField] float maxWaitTime = 15.0f;
    [SerializeField] LayerMask charactersMask = default;
    [SerializeField] float localAvoidanceSearchRange = 2.0f;
    [SerializeField] float localAvoidanceFactor = 0.3f;
    [SerializeField] Color possesedSkinColor = default;

    [SerializeField]
    Transform playerTarget;

    public Vector3 targetPos{get; private set;}
    public float Horizontal {get; private set;}
    public float Vertical {get; private set;}

    bool useLocalAvoidance = false;
    float waitTimer;

    void Awake()
    {
        waitTimer = 5f;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                Horizontal = 0.0f;
                Vertical = 0.0f;
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0.0f)
                    GoRandomPosition();

                break;

            case State.Moving:
                MoveToTarget();


                break;

            case State.Attacking:
                break;
        }
    }

    void MoveToTarget()
    {
        Vector2 direction = targetPos - transform.position;
        if(direction.sqrMagnitude <= 0.01f)
        {
            state = State.Idle;
            waitTimer = Random.Range(minWaitTime, maxWaitTime);
        }

        direction.Normalize();
        Horizontal = direction.x;
        Vertical = direction.y;
    }

    void GoRandomPosition()
    {
        Vector3 lerpPos = Vector3.Lerp(transform.position, playerTarget.position, 0.15f);
        targetPos = lerpPos + (Vector3)Random.insideUnitCircle * wanderRadius;
        state = State.Moving;
    }

    public Transform GetTarget()
    {
        return playerTarget;
    }

    public Vector3 GetPoint()
    {
        return playerTarget.position;
    }
}
