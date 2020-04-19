using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just a flag component atm
/// </summary>
public class DemonGod : MonoBehaviour, IDestroyable
{
    public static DemonGod instance;

    public void Damage(float value)
    {
        Debug.Log("Demon OOF'd");
        InfluenceManager.Instance.ReduceInfluence(value);
    }

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }
}
