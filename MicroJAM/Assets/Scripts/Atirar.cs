using UnityEngine;
using System.Collections;
using TMPro;

public class Atirar : MonoBehaviour
{
    public GameObject[] bulletPrefab; // Prefab do projétil
    public Sprite[] spritesArma; // Array de sprites das armas
    public GameObject armaObjeto; // Objeto que exibirá o sprite da arma
    public float fireRate = 0.2f; // Taxa de disparo (tempo entre cada tiro)
    public int maxAmmo = 10; // Máximo de munição
    public int currentAmmo; // Munição atual
    public int bulletCount; // quantidade de tiros disparados por vez
    public float reloadTime = 2f; // Tempo de recarga
    private bool isReloading = false; // Verifica se está recarregando

    private float fireCooldown = 0f; // Contador para o cooldown do disparo
    public TextMeshProUGUI ammoText;
    private Weapons weapon;

    private int chosenWeapon;

    public GameObject recharge;

    void Start()
    {
        recharge = GameObject.FindGameObjectWithTag("Recharge");
        recharge.SetActive(false);
        Time.timeScale = 0;
        currentAmmo = maxAmmo; // Inicializa a munição atual
        UpdateAmmoText();
    }

    void Update()
    {
        RotateWeaponToMouse();
        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && fireCooldown <= 0f && !isReloading)
        {
            if (currentAmmo > 0)
            {
                Fire(); // Dispara
                currentAmmo--; // Reduz a munição a cada disparo
                fireCooldown = fireRate; // Reinicia o cooldown
                UpdateAmmoText();
            }
        }
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload()); // Inicia a recarga quando a munição acabar
        }
    }

      void RotateWeaponToMouse()
    {
        // Obtemos a posição do mouse na tela e a convertê-la para o espaço do mundo
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        
        // Calcula a diferença entre a posição da arma e a posição do mouse
        Vector3 direction = worldMousePos - armaObjeto.transform.position;
        direction.z = 0; // Apenas no plano 2D

        // Calcula o ângulo em radianos e converte para graus
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Define a rotação da arma no eixo Z (somente no eixo Z, para 2D)
        armaObjeto.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void UpdateAmmoText()
    {
        ammoText.text = "Munição: " + currentAmmo.ToString(); // Exibe a quantidade atual de munição
    }

    void Fire()
    {
        for (int i = bulletCount; i > 0; i--)
        {
            GameObject bullet = Instantiate(bulletPrefab[chosenWeapon], transform.position, Quaternion.identity);
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        recharge.SetActive(true);
        Debug.Log("Recarregando...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Recarga concluída.");
        UpdateAmmoText();
        recharge.SetActive(false);
    }

    public void escolherArma(int arma)
    {
        armaObjeto.SetActive(true);
        chosenWeapon = arma;
        Debug.Log("Arma escolhida: " + chosenWeapon);

        // Define o sprite da arma correspondente
        armaObjeto.GetComponent<SpriteRenderer>().sprite = spritesArma[chosenWeapon];

        if (chosenWeapon == 0) // Pistola
        {
            weapon = bulletPrefab[0].GetComponent<PistolBullet>().weapons;
            fireRate = weapon.pistolFireRate;
            maxAmmo = weapon.pistolMagazine;
            reloadTime = weapon.pistolReloadTime;
            bulletCount = weapon.pistolBulletCount;

            bulletPrefab[0].GetComponent<OnHitDamage>().damage = weapon.pistolDamage;
            bulletPrefab[0].GetComponent<PistolBullet>().speed = weapon.pistolShotSpeed;
            bulletPrefab[0].GetComponent<PistolBullet>().lifeTime = weapon.pistolRange;
            bulletPrefab[0].GetComponent<PistolBullet>().spreadRadius = weapon.pistolSpread;
            bulletPrefab[0].GetComponent<PistolBullet>().penetration = weapon.pistolPenetration;
        }
        else if (chosenWeapon == 1) // Shotgun
        {
            weapon = bulletPrefab[1].GetComponent<ShotgunBullet>().weapons;
            fireRate = weapon.shotgunFireRate;
            maxAmmo = weapon.shotgunMagazine;
            reloadTime = weapon.shotgunReloadTime;
            bulletCount = weapon.shotgunBulletCount;

            bulletPrefab[1].GetComponent<OnHitDamage>().damage = weapon.shotgunDamage;
            bulletPrefab[1].GetComponent<ShotgunBullet>().speed = weapon.shotgunShotSpeed;
            bulletPrefab[1].GetComponent<ShotgunBullet>().lifeTime = weapon.shotgunRange;
            bulletPrefab[1].GetComponent<ShotgunBullet>().spreadRadius = weapon.shotgunSpread;
            bulletPrefab[1].GetComponent<ShotgunBullet>().penetration = weapon.shotgunPenetration;
        }
        else if (chosenWeapon == 2) // Sniper
        {
            SniperBullet sniper = bulletPrefab[2].GetComponent<SniperBullet>();
            OnHitDamage onHitDamage = bulletPrefab[2].GetComponent<OnHitDamage>();
            weapon = sniper.weapons;

            fireRate = weapon.sniperFireRate;
            maxAmmo = weapon.sniperMagazine;
            reloadTime = weapon.sniperReloadTime;
            bulletCount = weapon.sniperBulletCount;

            onHitDamage.damage = weapon.sniperDamage;
            sniper.speed = weapon.sniperShotSpeed;
            sniper.lifeTime = weapon.sniperRange;
            sniper.spreadRadius = weapon.sniperSpread;
            sniper.penetration = weapon.sniperPenetration;
        }
        else if (chosenWeapon == 3) // Metralhadora
        {
            weapon = bulletPrefab[3].GetComponent<MachineGunBullet>().weapons;
            fireRate = weapon.machineGunFireRate;
            maxAmmo = weapon.machineGunMagazine;
            reloadTime = weapon.machineGunReloadTime;
            bulletCount = weapon.machineGunBulletCount;

            bulletPrefab[3].GetComponent<OnHitDamage>().damage = weapon.machineGunDamage;
            bulletPrefab[3].GetComponent<MachineGunBullet>().speed = weapon.machineGunShotSpeed;
            bulletPrefab[3].GetComponent<MachineGunBullet>().lifeTime = weapon.machineGunRange;
            bulletPrefab[3].GetComponent<MachineGunBullet>().spreadRadius = weapon.machineGunSpread;
            bulletPrefab[3].GetComponent<MachineGunBullet>().penetration = weapon.machineGunPenetration;
        }

        currentAmmo = maxAmmo;
        UpdateAmmoText();
        Time.timeScale = 1;
    }
}
