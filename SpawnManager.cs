using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 10.0f;
    public int enemyCount;
    public int waveNumber = 1; 
    void Start()
    {
        SpawnEnemyWaves(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            waveNumber ++;
            SpawnEnemyWaves(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }
    void SpawnEnemyWaves(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
         float spawnPosZ = Random.Range(-spawnRange, spawnRange);
         float spawnPosX = Random.Range(-spawnRange, spawnRange);
         Vector3 randomPos = new Vector3 (spawnPosX, 0, spawnPosZ);

         return randomPos;
    }
}
