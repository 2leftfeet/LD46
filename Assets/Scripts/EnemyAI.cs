using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy AI that pseudorandomly moves closer to player
/// </summary>
public class EnemyAI : MonoBehaviour, IInput, IHasTarget
{
    public enum State {Idle, Moving, Attacking, Dead}
    [SerializeField]
    private State state;

    [Header("Movement Stats")]
    [SerializeField] float wanderRadius = 4.0f;
    [SerializeField] float detectionRadius = 6.0f;
    [SerializeField] float loseTargetRadius = 9.0f;
    [SerializeField] float minWaitTime = 5.0f;
    [SerializeField] float maxWaitTime = 10.0f;

    [Header("Combat Stats")]
    [SerializeField] float hitRange = 1.0f;

    [SerializeField]
    Transform seekTarget;
    Transform attackTarget;

    private Vector3 startPos;

    [Header("Misc Stats")]
    public bool prioritizePlayer = false;
    public bool seekDemonGod = false;

    public Vector3 TargetPosition { get; private set; }
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    bool useLocalAvoidance = false;
    float waitTimer;

    public event Action OnAttack;

    public bool IsDead
    {
        get
        {
            return state == State.Dead;
        }
    }

    void Start()
    {
        startPos = transform.position;
        waitTimer = 5f;

        if (seekDemonGod)
            seekTarget = DemonGod.instance.transform;
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
                {
                    if (seekTarget)
                        GoSeekPosition();
                    else
                        GoRandomPosition();
                }

                CheckForTargets();
                break;

            case State.Moving:
                MoveToTarget();
                CheckForTargets();
                break;

            case State.Attacking:
                if (!attackTarget)
                    state = State.Idle;
                else
                {
                    TargetPosition = attackTarget.position;
                    float distance = Vector2.Distance(transform.position, attackTarget.position);
                    if (distance > loseTargetRadius)
                    {
                        state = State.Idle;
                    }
                    else if(distance < hitRange)
                    {
                        OnAttack();
                        Horizontal = Vertical = 0.0f;
                    }
                    else
                    {
                        MoveToTarget();
                    }
                }
                
                break;
        }
    }

    public void CheckForTargets()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            var villager = colliders[i].GetComponent<VillagerAI>();
            if (villager && villager.IsPossessed)
            {
                state = State.Attacking;
                attackTarget = villager.transform;
                if(!prioritizePlayer)
                    return;
            }
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            var demon = colliders[i].GetComponent<DemonGod>();
            if (demon)
            {
                state = State.Attacking;
                attackTarget = demon.transform;
                if (!prioritizePlayer)
                    return;
            }

            // TODO get antoher class to reference instead PlayerInput
            /*var player = colliders[i].GetComponent<PlayerInput>();
            if (player)
            {
                state = State.Attacking;
                attackTarget = player.transform;
                break;
            }*/

        }
    }

    void MoveToTarget()
    {
        Vector2 direction = TargetPosition - transform.position;
        if(direction.sqrMagnitude <= 0.01f && state == State.Moving)
        {
            state = State.Idle;
            waitTimer = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
        }

        direction.Normalize();

        Horizontal = direction.x;
        Vertical = direction.y;
    }

    void GoSeekPosition()
    {
        Vector3 lerpPos = Vector3.Lerp(transform.position, seekTarget.position, 0.33f);
        TargetPosition = lerpPos + (Vector3)UnityEngine.Random.insideUnitCircle * wanderRadius;
        state = State.Moving;
    }

    void GoRandomPosition()
    {
        TargetPosition = startPos + (Vector3)UnityEngine.Random.insideUnitCircle * wanderRadius;
        state = State.Moving;
    }

    public Transform GetTarget()
    {
        return attackTarget;
    }

    public void SetTarget(Transform target)
    {
        attackTarget = target;
    }

    public Vector3 GetPoint()
    {
        if (attackTarget != null)
            return attackTarget.position;
        else if (state == State.Moving)
            return TargetPosition;
        else
            return Vector3.down;
    }
}
