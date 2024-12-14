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
    public float shotgunBulletCount; // quantidades de balas atiradas ao mesmo tempo
    public float shotgunShotSpeed; // velocidade do projetil
    public float shotgunRange; // alcance maximo do projetil
    public float shotgunSpread; // precisao da arma
    public float shotgunMagazine; // quantidades de tiros antes de precisar recarregar a arma

    [Header("Sniper")]
    public float sniperDamage;
    public float sniperFireRate;
    public float sniperReloadTime;
    public float sniperBulletCount;
    public float sniperShotSpeed;
    public float sniperRange;
    public float sniperSpread;
    public float sniperMagazine;

    [Header("Pistol")]
    public float pistolDamage;
    public float pistolFireRate;
    public float pistolReloadTime;
    public float pistolBulletCount;
    public float pistolShotSpeed;
    public float pistolRange;
    public float pistolSpread;
    public float pistolMagazine;

    [Header("Metralhadora")]
    public float metralhadoraDamage;
    public float metralhadoraFireRate;
    public float metralhadoraReloadTime;
    public float metralhadoraBulletCount;
    public float metralhadoraShotSpeed;
    public float metralhadoraRange;
    public float metralhadoraSpread;
    public float metralhadoraMagazine;
}
