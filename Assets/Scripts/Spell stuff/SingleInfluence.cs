using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInfluence : MonoBehaviour
{
    [SerializeField] private float range = default;

    private Caster thisCaster;
    private KeyCode singleKey;
    private KeyCode cancelKey;

    private void Awake()
    {
        thisCaster = transform.GetComponent<Caster>();
        singleKey = thisCaster.singleKey;
        cancelKey = thisCaster.cancelKey;
    }

    // Update is called once per frame
    void Update()
    {
        Target();
        Cast();
    }

    private void Target()
    {
        // Enter targeting state
        if(Input.GetKeyDown(singleKey) && !thisCaster.isTargeting)
            thisCaster.StartCast(range);

        // Exit targeting state
        else if(Input.GetKeyDown(cancelKey) && thisCaster.isTargeting)
            thisCaster.EndCast();

    }

    // Will have to check for more shit once it's an actual spell
    private void Cast()
    {
        if(thisCaster.isTargeting && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 casterPos = transform.position;
            Vector2 targetPos = CursorManager.instance.GetWorldSpacePosition();

            if(thisCaster.IsDistanceValid(casterPos, targetPos, range))
            {
                thisCaster.EndCast();
                Influence();
            }
        }
    }

    private void Influence()
    {
        // Dunno the logic here.
    }
}
