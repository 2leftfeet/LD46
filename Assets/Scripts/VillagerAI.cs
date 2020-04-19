using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : MonoBehaviour, IInput, IHasTarget
{
    public enum State {Idle, Moving, Possessed, F}
    [SerializeField]
    private State state;

    [SerializeField] float wanderRadius = 2.0f;
    [SerializeField] float minWaitTime = 1.0f;
    [SerializeField] float maxWaitTime = 4.0f;
    [SerializeField] LayerMask charactersMask = default;
    [SerializeField] float localAvoidanceSearchRange = 2.0f;
    [SerializeField] float localAvoidanceFactor = 0.3f;
    [SerializeField] Color possesedSkinColor = default;

    Vector3 startPosition;
    [SerializeField]
    Transform transTarget;

    public Vector3 targetPos {get; private set;}
    public float Horizontal {get; private set;}
    public float Vertical {get; private set;}

    public bool IsPossessed
    {
        get
        {
            return (state == State.F || state == State.Possessed);
        }
    }

    private SacrificePoint targetSacrificePoint;

    bool useLocalAvoidance = false;
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
        }
    }

    void MoveToTarget()
    {
        Vector2 direction = targetPos - transform.position;
        if(!transTarget && !targetSacrificePoint && direction.sqrMagnitude <= 0.01f)
        {
            state = State.Idle;
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

    public Transform GetTarget()
    {
        return transTarget;
    }

    public Vector3 GetPoint()
    {
        return targetPos;
    }
}
