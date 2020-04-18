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
            var villager = hit.collider.GetComponent<VillagerInput>();
            if(villager)
            {
                villager.Possess(caster.transform);
            }
        }
    }
}
