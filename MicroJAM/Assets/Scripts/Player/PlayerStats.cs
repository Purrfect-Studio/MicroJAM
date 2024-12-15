using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Player")]
    public float maxHealth; // Vida máxima do jogador
    public float playerSpeed; // Velocidade de movimento
    public float playerStairSpeed;
    public float maxXp;
    public float currentXp;
    public int currentLevel;


}
