using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 10; // Vida máxima do jogador
    public int currentHealth = 10; // Vida atual do jogador
    public int damage = 5; // Dano causado pelo jogador
    public float speed = 5f; // Velocidade de movimento
    public float attackSpeed = 1f; // Velocidade de ataque
    public int armor = 0; // Armadura do jogador

    // Lista de armas do jogador
    public List<string> weapons = new List<string>();
}
