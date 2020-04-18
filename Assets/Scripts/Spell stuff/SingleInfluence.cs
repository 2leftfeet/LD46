using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInfluence : ISpell
{


    // Will have to check for more shit once it's an actual spell
    public void Cast()
    {
        Influence();
    }

    private void Influence()
    {
        Debug.Log("I did single influence");
    }
}
