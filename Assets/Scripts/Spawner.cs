using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> villagers;

    private IEnumerator coroutine;
    [Range(2, 10)]
    public float waitInterval;
    [Range(1, 5)]
    public float circleRange;
    private CircleCollider2D collider;
    
    // Start is called before the first frame update
    void Start()
    {
        coroutine = Spawn();
        StartCoroutine(coroutine);
        collider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            float rnd = Random.Range(2f,waitInterval);
            yield return new WaitForSeconds(rnd);
            float rndX = Random.Range(-circleRange, circleRange);
            float rndY = Random.Range(-circleRange, circleRange);
            Vector2 newPosition = new Vector2(gameObject.transform.position.x+rndX, gameObject.transform.position.y+rndY);
            if(!Physics2D.OverlapCircle(newPosition, collider.radius)){
                Instantiate(villagers[Random.Range(0,villagers.Count-1)], newPosition, Quaternion.identity);
            }
        }
    }
}
