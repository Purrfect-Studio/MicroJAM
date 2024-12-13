using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
     public List<Wave> waves; // Lista de ondas
    public float timeBetweenWaves = 5f; // Tempo entre ondas
    private int currentWaveIndex = 0;
    private bool waveInProgress = false;

    void Update()
    {
        if (!waveInProgress && currentWaveIndex < waves.Count)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
    }

    IEnumerator SpawnWave(Wave wave)
{
    waveInProgress = true;
    Debug.Log($"Iniciando {wave.waveName}");

    foreach (var group in wave.enemyGroups)
    {
        for (int i = 0; i < group.enemyCount; i++)
        {
            // Sorteia um ponto de spawn para cada inimigo individualmente
            Transform spawnPoint = wave.spawnPoints[Random.Range(0, wave.spawnPoints.Length)];
            SpawnEnemy(group.enemyPrefab, spawnPoint);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    yield return new WaitForSeconds(timeBetweenWaves);
    currentWaveIndex++;
    waveInProgress = false;
}

void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint)
{
    Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
}

    void SpawnEnemy(GameObject enemyPrefab, Transform[] spawnPoints)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}

