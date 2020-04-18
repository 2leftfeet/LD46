using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInfluence : MonoBehaviour
{
    private bool isTargeting = false;

    private KeyCode singleKey;
    private KeyCode cancelKey;

    private void Awake()
    {
        var caster = transform.GetComponent<Caster>();
        singleKey = caster.singleKey;
        cancelKey = caster.cancelKey;
    }

    // Update is called once per frame
    void Update()
    {
        Target();
        Cast();
    }

    private void Target()
    {
        if(Input.GetKeyDown(singleKey) && !isTargeting)
        {
            isTargeting = true;
            CursorManager.instance.SetCursorState("CAST");
        }
        else if(Input.GetKeyDown(cancelKey) && isTargeting)
        {
            isTargeting = false;
            CursorManager.instance.SetCursorState("DEFAULT");
        }
    }

    // Will have to check for more shit once it's an actual spell
    private void Cast()
    {
        if(isTargeting && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isTargeting = false;
            CursorManager.instance.SetCursorState("DEFAULT");

            Influence();
        }
    }

    private void Influence()
    {
        // Dunno the logic here.
    }
}
