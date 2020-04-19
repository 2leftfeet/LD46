using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendVillagerSpell : ISpell
{
    public void Cast(Caster caster)
    {
        Vector3 castPos = CursorManager.instance.GetWorldSpacePosition();
        var villagerList = caster.GetComponent<PossessedVillagers>().possessedVillagers;
        if(villagerList.Count >= 0)
        {
            villagerList[villagerList.Count-1].SetGuardSpot(castPos);
            villagerList.RemoveAt(villagerList.Count-1);
        }
    }
}
