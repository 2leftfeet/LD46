using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInfluence : ISpell
{
    
    // Will have to check for more shit once it's an actual spell
    public void Cast(Caster caster)
    {
        Vector3 castPos = CursorManager.instance.GetWorldSpacePosition();
        RaycastHit2D hit = Physics2D.Raycast(castPos, Vector2.zero);

        if(hit.collider != null)
        {
            var villager = hit.collider.GetComponent<VillagerAI>();
            Debug.Log("yabba");
            if(villager)
            {
                Possess(caster, villager);
                Debug.Log("yabba");
            }
            else
            {
                villager = hit.collider.GetComponentInParent<VillagerAI>();
                Debug.Log("yabba");
                if(villager)
                    Possess(caster, villager);
            }


            
        }
    }

    void Possess(Caster caster, VillagerAI villager)
    {
        villager.Possess(caster.transform);
        SoundManager.Instance.CreatePlayAndDestroy(SoundManager.Instance.possessSound, 1.0f);

        var possessedVillagers = caster.GetComponent<PossessedVillagers>().possessedVillagers;
        if(!possessedVillagers.Contains(villager))
            possessedVillagers.Add(villager);
    }
}
