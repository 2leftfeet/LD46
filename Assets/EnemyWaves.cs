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
    public float timerBetweenWaves;
    public float timerBeforeFirstWave;
    public List<Transform> spawns;

    public GameObject enemy;

    public List<Wave> enemyWaves;
    
    public List<GameObject> aliveEnemies;

    int nextWaveIndex = 0;
    bool waveInProgress = false;
    float waveCooldownTimer;
    
    void Awake()
    {
        waveCooldownTimer = timerBeforeFirstWave;
    }

    void Update()
    {
        if(waveInProgress)
        {
            aliveEnemies.RemoveAll(item => item == null);
            if(aliveEnemies.Count <= 0)
            {
                waveInProgress = false;
                waveCooldownTimer = timerBetweenWaves;
            }
        }
        else
        {
            waveCooldownTimer -= Time.deltaTime;
            if(waveCooldownTimer <= 0.0f)
            {
                SpawnNextWaveHelper();
            }
        }

       
    }

    IEnumerator SpawnNextWave()
    {
        waveInProgress = true;
        Wave wave = enemyWaves[nextWaveIndex];
        nextWaveIndex++;

        var rnd = new System.Random();
        var activeSpawns = spawns.OrderBy(x => rnd.Next()).Take(wave.directionCount).ToList();
        for(int i = 0; i < wave.packCount; i++)
        {
            var currentSpawn = activeSpawns[i % activeSpawns.Count];
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
