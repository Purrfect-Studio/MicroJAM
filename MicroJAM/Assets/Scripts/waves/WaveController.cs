using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importa o namespace do TextMeshPro

public class WaveController : MonoBehaviour
{
    public List<Wave> waves; // Lista de ondas
    private int currentWaveIndex = 0;
    private bool waveInProgress = false;

    // Referências para o UI TextMeshPro
    public TextMeshProUGUI waveInfoText; // Para exibir a onda atual
    public TextMeshProUGUI enemyCountText; // Para exibir a quantidade de inimigos

    void Start()
    {
        // Inicializa a UI com a primeira onda e o número de inimigos na cena
        UpdateWaveText(waves[currentWaveIndex].waveName);
        UpdateEnemyCountText();
    }

    void Update()
    {
        // Atualiza a interface com o número de inimigos restantes
        UpdateEnemyCountText();

        // Verifica se a onda atual terminou e se ainda existem inimigos na cena
        if (!waveInProgress && currentWaveIndex < waves.Count && !AreEnemiesLeft())
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        waveInProgress = true;
        Debug.Log($"Iniciando {wave.waveName}");

        // Atualiza o texto para refletir a onda atual
        UpdateWaveText(wave.waveName);

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

        // Espera até que todos os inimigos sejam derrotados antes de avançar para a próxima onda
        yield return new WaitUntil(() => !AreEnemiesLeft());

        currentWaveIndex++; // Avança para a próxima onda
        waveInProgress = false;
    }

    void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint)
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    // Verifica se há inimigos com a tag "Enemy" ainda vivos na cena
    bool AreEnemiesLeft()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }

    // Atualiza o texto com a onda atual
    void UpdateWaveText(string waveName)
    {
        if (waveInfoText != null)
        {
            waveInfoText.text = $"Onda Atual: {waveName}"; // Exibe a onda atual
        }
    }

    // Atualiza o texto com a quantidade de inimigos restantes na cena
    void UpdateEnemyCountText()
    {
        if (enemyCountText != null)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemyCountText.text = $"Inimigos Restantes: {enemyCount}"; // Exibe a quantidade de inimigos restantes
        }
    }
}
