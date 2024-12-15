using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Current Player Stats")]

public class CurrentPlayerStats : ScriptableObject
{
    [Header("Player")]
    public float maxHealth; // Vida máxima do jogador
    public float playerSpeed; // Velocidade de movimento
    public float playerStairSpeed;
    public float maxXp;
    public float currentXp;
    public int currentLevel;

    [Header("Weapon")]
    public int currentWeapon;
    public float fireRate;
    public int maxAmmo;
    public float reloadTime;
    public int bulletCount;
    public float damage;
    public float bulletSpeed;
    public float lifeTime;
    public float spreadRadius;
    public float penetration;
    public Sprite sprite;


}
