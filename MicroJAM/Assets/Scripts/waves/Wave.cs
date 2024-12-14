using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave : MonoBehaviour
{
    public string waveName; // Nome da onda
    public List<EnemyGroup> enemyGroups; // Grupos de inimigos na onda
    public float spawnInterval; // Intervalo entre spawns
    public Transform[] spawnPoints; // Pontos de spawn
}
