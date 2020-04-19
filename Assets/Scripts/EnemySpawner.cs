using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> villagers;
    public List<GameObject> spawnPoints;

    private IEnumerator coroutine;
    [Range(1, 30)]
    public float avgWaitTime = 15f;

    [Range(1,30)]
    public int maxUnpossesedVillagerCount = 1;
    [HideInInspector]
    public List<GameObject> spawnedVillagers;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = Spawn();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        float median = (avgWaitTime/4);
        while (true)
        {
            float waitTime = avgWaitTime + Random.Range(-median, median);
            yield return new WaitForSeconds(waitTime);
            for(int i=0; i < spawnedVillagers.Count;i++){
                if(spawnedVillagers[i].GetComponent<EnemyAI>().IsDead){
                    spawnedVillagers.RemoveAt(i);
                }
            }
            if(spawnedVillagers.Count < maxUnpossesedVillagerCount){
                spawnedVillagers.Add(Instantiate(villagers[Random.Range(0,villagers.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity));
            }
        }
    }
}