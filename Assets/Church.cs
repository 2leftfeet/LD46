using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : MonoBehaviour
{
    public VillagerAI inquisitorPrefab;
    public Transform spawnPoint;

    public void SpawnInquisitor(GoldBarCarry carrier)
    {
        var inqList = carrier.GetComponent<PossessedVillagers>().inquisitors;
        VillagerAI inq = Instantiate(inquisitorPrefab, spawnPoint.position, Quaternion.identity);
        inq.SetTargetTrans(carrier.transform);
        inqList.Add(inq);
    }
}
