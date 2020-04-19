using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAttack : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float knockbackForce = 1.0f;
    [SerializeField] float cooldown = 1.0f;

    float cooldownTimer = 0.0f;
    IHasTarget attackerAI;

    void Awake()
    {
        attackerAI = GetComponent<IHasTarget>();
    }

    void Update()
    {
        if(cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void DoAttack()
    {
        if(cooldownTimer <= 0.0f)
        {
            var attackTarget = attackerAI.GetTarget();
            var destroyable = attackTarget.GetComponent<IDestroyable>();
            destroyable.Damage(damage);

            var body = attackTarget.GetComponent<Rigidbody2D>();
            if(body)
            {
                Debug.Log("adding force");
                //body.AddForce((transform.position - attackTarget.position).normalized*knockbackForce);
                body.velocity = (attackTarget.position - transform.position).normalized*knockbackForce;
            }
            cooldownTimer = cooldown;
        }
    }

    void OnEnable()
    {
        attackerAI.OnAttack += DoAttack;
    }

    void OnDisable()
    {
        attackerAI.OnAttack -= DoAttack;
    }
}
