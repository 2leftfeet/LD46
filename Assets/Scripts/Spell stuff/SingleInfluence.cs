using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInfluence : ISpell
{
    public static event Action OnPossess;

    // Will have to check for more shit once it's an actual spell
    public void Cast(Caster caster)
    {
        Vector3 castPos = CursorManager.instance.GetWorldSpacePosition();
        RaycastHit2D hit = Physics2D.Raycast(castPos, Vector2.zero, Mathf.Infinity, 1 << 10);

        //Debug.Log(hit.collider.name);

        if(hit.collider != null)
        {
            var villager = hit.collider.GetComponent<VillagerAI>();
            if(villager)
            {
                Possess(caster, villager);
            }
            else
            {
                villager = hit.collider.GetComponentInParent<VillagerAI>();
                if(villager)
                    Possess(caster, villager);
            }


            
        }
    }

    void Possess(Caster caster, VillagerAI villager)
    {
        if (OnPossess != null)
            OnPossess();

        villager.Possess(caster.transform);
        SoundManager.Instance.CreatePlayAndDestroy(SoundManager.Instance.possessSound, 1.0f);

        var pv = caster.GetComponent<PossessedVillagers>();
        

        if(!villager.isInquisitor)
        {
            if(!pv.possessedVillagers.Contains(villager))
                pv.possessedVillagers.Add(villager);
        }
        else
        {
            if(!pv.inquisitors.Contains(villager))
                pv.inquisitors.Add(villager);
        }
    }
}
