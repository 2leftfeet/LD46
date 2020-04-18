using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    SingleInfluence
}

[CreateAssetMenu(menuName="Spells/Spell")]
public class SpellObject : ScriptableObject
{
    public float range;
    public KeyCode castKey;
    public SpellType type;
    ISpell spellLogic;

    void OnEnable()
    {
        switch(type)
        {
            case SpellType.SingleInfluence:
                spellLogic = new SingleInfluence();
                break;
            default:
                break;
        }
    }

    public void Cast()
    {
        spellLogic.Cast();
    }
}
