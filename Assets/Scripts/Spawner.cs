using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> villagers;
    public List<GameObject> spawnPoints;

    private IEnumerator coroutine;
    [Range(1, 30)]
    public float avgWaitTime = 2f;

    [Range(1,30)]
    public int maxUnpossesedVillagerCount = 8;
    [HideInInspector]
    public List<GameObject> spawnedVillagers;

    private Village myVillage;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = Spawn();
        StartCoroutine(coroutine);
        myVillage = GetComponent<Village>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i < spawnedVillagers.Count;i++){
                if(spawnedVillagers[i].GetComponent<VillagerAI>().IsPossessed){
                    spawnedVillagers.RemoveAt(i);
                    myVillage.ModifyProsperity(-2);
                }
            }
    }

    private IEnumerator Spawn()
    {
        float median = (avgWaitTime/4);
        while (true)
        {
            float waitTime = avgWaitTime + Random.Range(-median, median);
            yield return new WaitForSeconds(waitTime);
            if(spawnedVillagers.Count < maxUnpossesedVillagerCount){
                spawnedVillagers.Add(Instantiate(villagers[Random.Range(0,villagers.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity));
            }
        }
    }
}