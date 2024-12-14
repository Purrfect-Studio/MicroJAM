using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth; // Vida máxima do jogador
    public int currentHealth; // Vida atual do jogador
    public int damage; // Dano causado pelo jogador
    public float speed; // Velocidade de movimento
    public float attackSpeed; // Velocidade de ataque
    public int armor;// Armadura do jogador

    // Lista de armas do jogador
    public List<string> weapons = new List<string>();
}
