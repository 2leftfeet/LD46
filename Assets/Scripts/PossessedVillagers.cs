using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedVillagers : MonoBehaviour
{
    public List<VillagerAI> possessedVillagers;
    public List<VillagerAI> inquisitors;

    public void Update()
    {
        possessedVillagers.RemoveAll(item => item == null);
        inquisitors.RemoveAll(item => item == null);
    }
}
