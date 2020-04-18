using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> villagers;

    private IEnumerator coroutine;
    [Range(2, 10)]
    public float maxWaitInterval = 2f;
    [Range(1, 5)]
    public float maxSpawnRange = 1f;
    
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
            float rndX = Random.Range(-maxSpawnRange, maxSpawnRange);
            float rndY = Random.Range(-maxSpawnRange, maxSpawnRange);
            Vector2 newPosition = new Vector2(gameObject.transform.position.x+rndX, gameObject.transform.position.y+rndY);
            RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector3.zero);
            Debug.Log(hit);
            if(!hit){
                Instantiate(villagers[Random.Range(0,villagers.Count)], newPosition, Quaternion.identity);
            }
        }
    }
}