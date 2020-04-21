using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static event Action OnWaveDefeat;

    public float timerBetweenWaves;
    public float timerBeforeFirstWave;
    public List<Transform> spawns;

    public GameObject enemy;

    private List<Wave> enemyWaves;
    
    public ArrowIndicatorController arrowIndicatorController;
    public SpeechBubble speechBubble;
    public int waveCount;

    List<GameObject> aliveEnemies;
    int nextWaveIndex = 0;
    bool waveInProgress = false;
    float waveCooldownTimer;
    
    void Awake()
    {
        aliveEnemies = new List<GameObject>();
        waveCooldownTimer = timerBeforeFirstWave;
        CreateRandomWaves();
    }

    void CreateRandomWaves()
    {
        enemyWaves = new List<Wave>();
        for(int i = 1; i <= waveCount; i++)
        {
            int enemyCount = (int)Mathf.Floor(1.5f * i);
            var directionCount = UnityEngine.Random.Range(1, Mathf.Min(enemyCount, 4));
            var packCount = UnityEngine.Random.Range(directionCount, Mathf.Min(enemyCount, 6));
            var packSize = enemyCount/packCount;

            Wave newWave = new Wave();
            newWave.directionCount = directionCount;
            newWave.enemyPackSize = packSize;
            newWave.packCount = packCount;
            newWave.timerBetweenPacks = packSize;
            enemyWaves.Add(newWave);
        }
    }

    void Update()
    {
        if(waveInProgress)
        {
            aliveEnemies.RemoveAll(item => item == null);
            if(aliveEnemies.Count <= 0)
            {
                OnWaveDefeat?.Invoke();
                waveInProgress = false;
                waveCooldownTimer = timerBetweenWaves;
                if(nextWaveIndex >= enemyWaves.Count)
                {
                    SceneManager.LoadScene("Outro");
                }
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
    
    public void SpawnSingleWave()
    {
        StartCoroutine(SpawnNextWave(true));
    }

    IEnumerator SpawnNextWave(bool skipBubble)
    {
        waveInProgress = true;
        Wave wave = enemyWaves[nextWaveIndex];
        nextWaveIndex++;

        var rnd = new System.Random();
        var activeSpawns = spawns.OrderBy(x => rnd.Next()).Take(wave.directionCount).ToList();
        if(speechBubble &&! skipBubble)
        {
            speechBubble.PlayShowText("I feel that there's a raid coming! Prepare your minions!");
        }
        if(arrowIndicatorController)
        {
            foreach(var spawn in activeSpawns)
            {
                if(spawn.position.x > 0.0f){
                    arrowIndicatorController.PlayRight();
                }
                if(spawn.position.x < 0.0f){
                    arrowIndicatorController.PlayLeft();
                }
                if(spawn.position.y > 0.0f){
                    arrowIndicatorController.PlayTop();
                }
                if(spawn.position.y < 0.0f){
                    arrowIndicatorController.PlayBottom();
                }
            }
        }

        for(int i = 0; i < wave.packCount; i++)
        {
            var currentSpawn = activeSpawns[i % activeSpawns.Count];
            for(int j = 0; j < wave.enemyPackSize; j++)
            {
                aliveEnemies.Add(Instantiate(enemy, currentSpawn.position + (Vector3)UnityEngine.Random.insideUnitCircle, Quaternion.identity));
            }
            yield return new WaitForSeconds(wave.timerBetweenPacks);
        }

    }

    [ContextMenu("SpawnNextWave")]
    public void SpawnNextWaveHelper()
    {
        StartCoroutine(SpawnNextWave(false));
    }
}
