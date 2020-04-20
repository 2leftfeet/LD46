using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum State {Idle, Moving, Possessed, F, ReadyToAttack, Attacking}
public class VillagerAI : MonoBehaviour, IInput, IHasTarget
{
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
    public ParticleSystem possessEffect;
    public GameObject deathMark;
    public GameObject standMarker;
    public bool isInquisitor;

    Vector3 startPosition;
    [SerializeField]
    Transform transTarget;

    public Vector3 targetPos {get; private set;}
    public float Horizontal {get; private set;}
    public float Vertical {get; private set;}
    public event Action OnAttack = delegate{};

    public bool IsPossessed
    {
        get
        {
            return (state == State.F || state == State.Possessed || state == State.ReadyToAttack || state == State.Attacking);
        }
    }

    private SacrificePoint targetSacrificePoint;
    

    private Vector3 guardSpot;
    bool useLocalAvoidance = false;
    bool isGuarding = false;
    float waitTimer;

    float movingMaxTime = 3.0f;
    float movingTimer = 0.0f;

    GameObject standMarkerInstance;

    void Awake()
    {
        startPosition = transform.position;
        GoRandomPosition();
        if(isInquisitor)
        {
            state = State.Possessed;
        }
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
                movingTimer += Time.deltaTime;
                if(movingTimer >= movingMaxTime && !isGuarding)
                {
                    state = State.Idle;
                    movingTimer = 0.0f;
                }
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
                    if (Vector2.Distance(targetPos, transform.position) <= attackRange)
                    {
                        OnAttack();
                        Horizontal = Vertical = 0.0f;
                    }
                    else{
                        MoveToTarget();
                    }
                }
                CheckForNewTarget();
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
            else if(!transTarget && !targetSacrificePoint)
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
        deathMark.SetActive(false);
        isGuarding = false;
        if(standMarkerInstance)
            Destroy(standMarkerInstance);
        transTarget = target;
        useLocalAvoidance = true;
        waitTimer = 0.0f;

        GetComponent<Renderer>().material.SetColor("_SkinChanged", possesedSkinColor);
        GetComponent<BodyMovement>().ChangeSpeed(2.0f);

        state = State.Possessed; 

        Instantiate(possessEffect, transform.position, Quaternion.identity);
        
    }

    public void GoSacrificeSelf(SacrificePoint sacPoint, Transform target)
    {
        targetSacrificePoint = sacPoint;
        transTarget = target;

        state = State.F;
        deathMark.SetActive(true);
    }

    void GoRandomPosition()
    {
        targetPos = startPosition + (Vector3)Random.insideUnitCircle * wanderRadius;
        movingTimer = 0.0f;
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
        standMarkerInstance = Instantiate(standMarker, guardSpot, Quaternion.identity);
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

    public void CheckForNewTarget()
    {
        CheckForEnemies();
        if(transTarget == null)
        {
            state = State.Moving;
            targetPos = guardSpot;
        }
    }
    
    public Transform GetTarget()
    {
        return transTarget;
    }

    public Vector3 GetPoint()
    {
        return targetPos;
    }

    void OnDestroy()
    {
        if(standMarkerInstance){
            Destroy(standMarkerInstance);
        }
    }

    public void SetTargetTrans(Transform target)
    {
        transTarget = target;
    }
}
