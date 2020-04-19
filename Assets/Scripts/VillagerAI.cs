using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class VillagerAI : MonoBehaviour, IInput
{
    public enum State {Idle, Moving, Possessed, F, ReadyToAttack, Attacking}
    [SerializeField]
    public State state;

    [SerializeField] float wanderRadius = 2.0f;
    [SerializeField] float minWaitTime = 1.0f;
    [SerializeField] float maxWaitTime = 4.0f;
    [SerializeField] LayerMask charactersMask = default;
    [SerializeField] LayerMask enemyMask = default;
    [SerializeField] float enemySearchRadius = 3.0f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float localAvoidanceSearchRange = 2.0f;
    [SerializeField] float localAvoidanceFactor = 0.3f;
    [SerializeField] Color possesedSkinColor = default;

    Vector3 startPosition;
    [SerializeField]
    Transform transTarget;

    public Vector3 targetPos{get; private set;}
    public float Horizontal {get; private set;}
    public float Vertical {get; private set;}
    public event Action OnAttack = delegate{};

    private SacrificePoint targetSacrificePoint;
    

    private Vector3 guardSpot;
    bool useLocalAvoidance = false;
    bool isGuarding = false;
    float waitTimer;

    void Awake()
    {
        startPosition = transform.position;
        GoRandomPosition();
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
                if (useLocalAvoidance)
                    LocalAvoidance();

                break;

            case State.Possessed:
                targetPos = transTarget.position;
                MoveToTarget();
                if (useLocalAvoidance)
                    LocalAvoidance();
                break;

            case State.F:
                targetPos = transTarget.position;
                MoveToTarget();
                if (Vector2.Distance(targetPos, transform.position) <= 0.15f)
                    SacrificeSelf();
                break;

            case State.ReadyToAttack:
                Horizontal = 0.0f;
                Vertical = 0.0f;
                CheckForEnemies();
                break;

            case State.Attacking:
                if(transTarget)
                {
                    targetPos = transTarget.position;
                    MoveToTarget();
                    if (Vector2.Distance(targetPos, transform.position) <= attackRange)
                    {
                        OnAttack();
                        Debug.Log("i atacc");
                    }
                }
                CheckIfTargetIsAlive();
                break;


        }
    }

    void MoveToTarget()
    {
        Vector2 direction = targetPos - transform.position;
        if(direction.sqrMagnitude <= 0.01f)
        {
            if(isGuarding && state != State.Attacking)
            {
                state = State.ReadyToAttack;
            }
            if(!transTarget && !targetSacrificePoint)
            {
                state = State.Idle;
                waitTimer = Random.Range(minWaitTime, maxWaitTime);
            }
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

    public void Possess(Transform target)
    {
        transTarget = target;
        useLocalAvoidance = true;
        waitTimer = 0.0f;

        GetComponent<Renderer>().material.SetColor("_SkinChanged", possesedSkinColor);
        GetComponent<BodyMovement>().ChangeSpeed(2.0f);

        state = State.Possessed; 
    }

    public void GoSacrificeSelf(SacrificePoint sacPoint, Transform target)
    {
        targetSacrificePoint = sacPoint;
        transTarget = target;

        state = State.F;
    }

    void GoRandomPosition()
    {
        targetPos = startPosition + (Vector3)Random.insideUnitCircle * wanderRadius;
        state = State.Moving;
    }

    void SacrificeSelf()
    {
        targetSacrificePoint.Sacrifice(this);
    }

    public void SetGuardSpot(Vector3 _guardSpot)
    {
        state = State.Moving;
        targetPos = _guardSpot;
        guardSpot = _guardSpot;
        isGuarding = true;
    }

    void CheckForEnemies()
    {
        Collider2D col = Physics2D.OverlapCircle(targetPos, enemySearchRadius, enemyMask);
        if(col)
        {
            transTarget = col.transform;
            state = State.Attacking;
        }
    }

    void CheckIfTargetIsAlive()
    {
        if(transTarget == null)
        {
            state = State.Moving;
            targetPos = guardSpot;
        }
    }
}
