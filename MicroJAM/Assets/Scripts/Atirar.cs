using UnityEngine;
using System.Collections;
using TMPro;

public class Atirar : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab do projétil
    public float fireRate = 0.2f; // Taxa de disparo (tempo entre cada tiro)
    public int maxAmmo = 10; // Máximo de munição
    public int currentAmmo; // Munição atual
    public int bulletCount; // quantidade de tiros disparados por vez
    public float reloadTime = 2f; // Tempo de recarga
    private bool isReloading = false; // Verifica se está recarregando

    private float fireCooldown = 0f; // Contador para o cooldown do disparo
    public TextMeshProUGUI ammoText;
    private Weapons weapon;

    void Start()
    {
        if(bulletPrefab.GetComponent<PistolBullet>() != null)
        {
            weapon = bulletPrefab.GetComponent<PistolBullet>().weapons;

            fireRate = weapon.pistolFireRate;
            maxAmmo = weapon.pistolMagazine;
            reloadTime = weapon.pistolReloadTime;
            bulletCount = weapon.pistolBulletCount;
        }
        else if(bulletPrefab.GetComponent<ShotgunBullet>() != null)
        {
            weapon = bulletPrefab.GetComponent<ShotgunBullet>().weapons;

            fireRate = weapon.shotgunFireRate;
            maxAmmo = weapon.shotgunMagazine;
            reloadTime = weapon.shotgunReloadTime;
            bulletCount = weapon.shotgunBulletCount;
        }else if(bulletPrefab.GetComponent<SniperBullet>() != null)
        {
            weapon = bulletPrefab.GetComponent<SniperBullet>().weapons;

            fireRate = weapon.sniperFireRate;
            maxAmmo = weapon.sniperMagazine;
            reloadTime = weapon.sniperReloadTime;
            bulletCount = weapon.sniperBulletCount;
        }else if(bulletPrefab.GetComponent<MachineGunBullet>() != null)
        {
            weapon = bulletPrefab.GetComponent<MachineGunBullet>().weapons;

            fireRate = weapon.machineGunFireRate;
            maxAmmo = weapon.machineGunMagazine;
            reloadTime = weapon.machineGunReloadTime;
            bulletCount = weapon.machineGunBulletCount;
        }

        currentAmmo = maxAmmo; // Inicializa a munição atual
        UpdateAmmoText();
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Verifica se o jogador apertou o botão de disparo
        if (Input.GetButton("Fire1") && fireCooldown <= 0f && !isReloading)
        {
            if (currentAmmo > 0)
            {
                Fire(); // Dispara
                currentAmmo--; // Reduz a munição a cada disparo
                fireCooldown = fireRate; // Reinicia o cooldown
                UpdateAmmoText();
            }
            else
            {
                StartCoroutine(Reload()); // Inicia a recarga quando a munição acabar
            }
        }
    }

      void UpdateAmmoText()
    {
        ammoText.text = "Munição: " + currentAmmo.ToString(); // Exibe a quantidade atual de munição
    }

    void Fire()
    {
        // Instancia o projétil na posição do player
        for (int i = bulletCount; i > 0; i--)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // A direção será configurada no próprio método Start() do projétil
        }
    }

    public IEnumerator Reload()
    {
        // Inicia a recarga e impede o disparo durante esse tempo
        isReloading = true;
        Debug.Log("Recargando...");
        yield return new WaitForSeconds(reloadTime); // Espera o tempo de recarga
        currentAmmo = maxAmmo; // Restaura a munição ao valor máximo
        isReloading = false;
        Debug.Log("Recarga concluída.");
        UpdateAmmoText(); // Atualiza o texto após a recarga
    }
}
