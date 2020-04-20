using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendVillagerSpell : ISpell
{
    public void Cast(Caster caster)
    {
        Vector3 castPos = CursorManager.instance.GetWorldSpacePosition();
        var pv = caster.GetComponent<PossessedVillagers>();
        
        if(pv.inquisitors.Count > 0)
        {
            pv.inquisitors[pv.inquisitors.Count-1].SetGuardSpot(castPos);
            pv.inquisitors.RemoveAt(pv.inquisitors.Count-1);
            return;
        }

        if(pv.possessedVillagers.Count > 0)
        {
            pv.possessedVillagers[pv.possessedVillagers.Count-1].SetGuardSpot(castPos);
            pv.possessedVillagers.RemoveAt(pv.possessedVillagers.Count-1);
        }
    }
}
