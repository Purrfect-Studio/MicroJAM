using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons")]
public class Weapons : ScriptableObject
{
    [Header("Shotgun")]
    public float shotgunDamage; // dano causado por cada bala
    public float shotgunFireRate; // tempo entre disparos
    public float shotgunReloadTime; // tempo recarregando
    public int shotgunBulletCount; // quantidades de balas atiradas ao mesmo tempo
    public float shotgunShotSpeed; // velocidade do projetil
    public float shotgunRange; // alcance maximo do projetil
    public float shotgunSpread; // precisao da arma
    public int shotgunMagazine; // quantidades de tiros antes de precisar recarregar a arma
    public float shotgunPenetration; // quantidades de inimigos que a bala atravessa

    [Header("Sniper")]
    public float sniperDamage;
    public float sniperFireRate;
    public float sniperReloadTime;
    public int sniperBulletCount;
    public float sniperShotSpeed;
    public float sniperRange;
    public float sniperSpread;
    public int sniperMagazine;
    public float sniperPenetration;

    [Header("Pistol")]
    public float pistolDamage;
    public float pistolFireRate;
    public float pistolReloadTime;
    public int pistolBulletCount;
    public float pistolShotSpeed;
    public float pistolRange;
    public float pistolSpread;
    public int pistolMagazine;
    public float pistolPenetration;

    [Header("Machine Gun")]
    public float machineGunDamage;
    public float machineGunFireRate;
    public float machineGunReloadTime;
    public int machineGunBulletCount;
    public float machineGunShotSpeed;
    public float machineGunRange;
    public float machineGunSpread;
    public int machineGunMagazine;
    public float machineGunPenetration;
}
