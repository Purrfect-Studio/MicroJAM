using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetarValores : MonoBehaviour
{
    public CurrentPlayerStats currentPlayerStats;
    public PlayerStats playerStats;
    public Weapons weaponStats;
    public void resetarValores()
    {
        currentPlayerStats.maxHealth = playerStats.maxHealth;
        currentPlayerStats.playerSpeed = playerStats.playerSpeed;
        currentPlayerStats.playerStairSpeed = playerStats.playerStairSpeed;
        currentPlayerStats.maxXp = playerStats.maxXp;
        currentPlayerStats.currentXp = playerStats.currentXp;
        currentPlayerStats.currentLevel = playerStats.currentLevel;

        currentPlayerStats.currentWeapon = 0;
        currentPlayerStats.fireRate = weaponStats.pistolFireRate;
        currentPlayerStats.maxAmmo = weaponStats.pistolMagazine;
        currentPlayerStats.reloadTime = weaponStats.pistolReloadTime;
        currentPlayerStats.bulletCount = weaponStats.pistolBulletCount;
        currentPlayerStats.damage = weaponStats.pistolDamage;
        currentPlayerStats.bulletSpeed = weaponStats.pistolShotSpeed;
        currentPlayerStats.lifeTime = weaponStats.pistolRange;
        currentPlayerStats.spreadRadius = weaponStats.pistolSpread;
        currentPlayerStats.penetration = weaponStats.pistolPenetration;
    }
}
