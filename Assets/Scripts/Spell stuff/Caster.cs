using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab = default;
    [SerializeField] private GameObject rangePrefab = default;
    [HideInInspector] public bool isTargeting = false;
    public ParticleSystem possessEffect;

    public List<SpellObject> spells;

    [Header("Cancelling cast")]
    public KeyCode cancelKey;

    SpellObject activeSpell = null;
    private LineRenderer lineInstance;
    private GameObject rangeInstance;

    private float currentRange = 0f;

    private void Update()
    {
        foreach(var spell in spells)
        {
            if(Input.GetKeyDown(spell.castKey))
            {
                if(activeSpell != spell)
                {
                    EndCast();
                    StartCast(spell.range);
                    activeSpell = spell;
                    break;
                }
            }
        }

        if(Input.GetKey(cancelKey))
        {
            EndCast();
        }


        if(activeSpell)
        { 
            CheckForCast();
        }

        UpdateLine();
    }

    public void StartCast(float range)
    {
        isTargeting = true;
        CursorManager.instance.SetCursorState("CAST");

        // Range renderer.
        rangeInstance = Instantiate(rangePrefab, transform.position, Quaternion.identity, transform);
        float effectiveRange = range*2;
        rangeInstance.transform.localScale = new Vector3(effectiveRange, effectiveRange, 0);

        currentRange = range;

        // Line renderer.
        GameObject go = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineInstance = go.GetComponent<LineRenderer>();
    }

    public void EndCast()
    {
        if(activeSpell)
        {
            activeSpell = null;
            isTargeting = false;

            Destroy(rangeInstance);
            rangeInstance = null;

            Destroy(lineInstance.gameObject);
            lineInstance = null;

            CursorManager.instance.SetCursorState("DEFAULT");
        }
    }

    private void UpdateLine()
    {
        if(lineInstance)
        {
            Vector2 casterPos = transform.position;
            Vector2 targetPos = CursorManager.instance.GetWorldSpacePosition();

            if(IsDistanceValid(casterPos, targetPos, currentRange))
            {
                lineInstance.SetPosition(0, casterPos);
                lineInstance.SetPosition(1, targetPos);
            }
            else
            {
                lineInstance.SetPosition(0, casterPos);
                lineInstance.SetPosition(1, casterPos);
            }
        }
    }

    void CheckForCast()
    {
        if(isTargeting && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 targetPos = CursorManager.instance.GetWorldSpacePosition();

            if(IsDistanceValid(transform.position, targetPos, activeSpell.range))
            {
                activeSpell.Cast(this);
                EndCast();
            }
        }
    }

    public bool IsDistanceValid(Vector2 casterPos, Vector2 targetPos, float range)
    {
        //range *= 2;
        return ((casterPos - targetPos).sqrMagnitude) < range*range ? true : false;
    }
    
}
