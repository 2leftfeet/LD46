using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> villagers;
    public List<GameObject> spawnPoints;

    private IEnumerator coroutine;
    [Range(2, 20)]
    public float maxWaitInterval = 2f;
    
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
        while (true)
        {
            float rnd = Random.Range(2f,maxWaitInterval);
            yield return new WaitForSeconds(rnd);
            Instantiate(villagers[Random.Range(0,villagers.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity);
        }
    }
}