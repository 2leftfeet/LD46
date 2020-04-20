using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int directionCount;
    public int enemyPackSize;
    public int packCount;
    public float timerBetweenPacks;
}

public class EnemyWaves : MonoBehaviour
{
    public List<Transform> spawns;

    public GameObject enemy;

    public List<Wave> enemyWaves;
    
    public List<GameObject> aliveEnemies;

    private int nextWaveIndex;

    void Update()
    {
        foreach(var enemy in aliveEnemies)
        {
            if(!enemy)
            {
                aliveEnemies.Remove(enemy);
            }
        }

        if(aliveEnemies.Count <= 0)
        {
            Debug.Log("wave done");
        }
    }

    IEnumerator SpawnNextWave()
    {
        Wave wave = enemyWaves[nextWaveIndex];
        nextWaveIndex++;

        var rnd = new System.Random();
        var activeSpawns = spawns.OrderBy(x => rnd.Next()).Take(wave.directionCount).ToList();
        for(int i = 0; i < wave.packCount; i++)
        {
            var currentSpawn = activeSpawns[Random.Range(0, activeSpawns.Count)];
            for(int j = 0; j < wave.enemyPackSize; j++)
            {
                aliveEnemies.Add(Instantiate(enemy, currentSpawn.position + (Vector3)Random.insideUnitCircle, Quaternion.identity));
            }
            yield return new WaitForSeconds(wave.timerBetweenPacks);
        }

    }

    [ContextMenu("SpawnNextWave")]
    public void SpawnNextWaveHelper()
    {
        StartCoroutine(SpawnNextWave());
    }
}
