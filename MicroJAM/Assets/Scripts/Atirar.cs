using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Atirar : MonoBehaviour
{
    public GameObject[] bulletPrefab; // Prefab do projétil
    public Sprite[] spritesArma; // Array de sprites das armas
    public SpriteRenderer armaSpriteRenderer; // Objeto que exibirá o sprite da arma
    public float fireRate; // Taxa de disparo (tempo entre cada tiro)
    public int maxAmmo; // Máximo de munição
    public int currentAmmo; // Munição atual
    public int bulletCount; // quantidade de tiros disparados por vez
    public float reloadTime; // Tempo de recarga
    private bool isReloading = false; // Verifica se está recarregando
    public AudioSource somRecarga;
    private PlayerHealthSystem playerHealthSystem;

    private float fireCooldown; // Contador para o cooldown do disparo
    public TextMeshProUGUI ammoText;
    private Weapons weapon;
    public CurrentPlayerStats currentPlayerStats;

    private int chosenWeapon;

    public GameObject recharge;

    void Start()
    {
        playerHealthSystem = GetComponent<PlayerHealthSystem>();
        recharge = GameObject.FindGameObjectWithTag("Recharge");
        recharge.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Level 01" || SceneManager.GetActiveScene().name == "Cain Teste")
        {
            Time.timeScale = 0;
        }
        else
        {
            fireRate = currentPlayerStats.fireRate;
            maxAmmo = currentPlayerStats.maxAmmo;
            currentAmmo = maxAmmo;
            bulletCount = currentPlayerStats.bulletCount;
            reloadTime = currentPlayerStats.reloadTime;
            chosenWeapon = currentPlayerStats.currentWeapon;
            if(chosenWeapon == 0)
            {
                bulletPrefab[0].GetComponent<OnHitDamage>().damage = currentPlayerStats.damage;
                bulletPrefab[0].GetComponent<PistolBullet>().speed = currentPlayerStats.bulletSpeed;
                bulletPrefab[0].GetComponent<PistolBullet>().lifeTime = currentPlayerStats.lifeTime;
                bulletPrefab[0].GetComponent<PistolBullet>().spreadRadius = currentPlayerStats.spreadRadius;
                bulletPrefab[0].GetComponent<PistolBullet>().penetration = currentPlayerStats.penetration;
            }
            else if(chosenWeapon == 1)
            {
                bulletPrefab[1].GetComponent<OnHitDamage>().damage = currentPlayerStats.damage;
                bulletPrefab[1].GetComponent<ShotgunBullet>().speed = currentPlayerStats.bulletSpeed;
                bulletPrefab[1].GetComponent<ShotgunBullet>().lifeTime = currentPlayerStats.lifeTime;
                bulletPrefab[1].GetComponent<ShotgunBullet>().spreadRadius = currentPlayerStats.spreadRadius;
                bulletPrefab[1].GetComponent<ShotgunBullet>().penetration = currentPlayerStats.penetration;
            }
            else if(chosenWeapon == 2)
            {
                bulletPrefab[2].GetComponent<OnHitDamage>().damage = currentPlayerStats.damage;
                bulletPrefab[2].GetComponent<SniperBullet>().speed = currentPlayerStats.bulletSpeed;
                bulletPrefab[2].GetComponent<SniperBullet>().lifeTime = currentPlayerStats.lifeTime;
                bulletPrefab[2].GetComponent<SniperBullet>().spreadRadius = currentPlayerStats.spreadRadius;
                bulletPrefab[2].GetComponent<SniperBullet>().penetration = currentPlayerStats.penetration;
            }
            else if(chosenWeapon == 3)
            {
                bulletPrefab[3].GetComponent<OnHitDamage>().damage = currentPlayerStats.damage;
                bulletPrefab[3].GetComponent<MachineGunBullet>().speed = currentPlayerStats.bulletSpeed;
                bulletPrefab[3].GetComponent<MachineGunBullet>().lifeTime = currentPlayerStats.lifeTime;
                bulletPrefab[3].GetComponent<MachineGunBullet>().spreadRadius = currentPlayerStats.spreadRadius;
                bulletPrefab[3].GetComponent<MachineGunBullet>().penetration = currentPlayerStats.penetration;
            }
            armaSpriteRenderer.sprite = spritesArma[chosenWeapon];
        }
        armaSpriteRenderer.sprite = spritesArma[chosenWeapon];

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
        Vector3 direction = worldMousePos - armaSpriteRenderer.transform.position;
        direction.z = 0; // Apenas no plano 2D

        // Calcula o ângulo em radianos e converte para graus
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Define a rotação da arma no eixo Z (somente no eixo Z, para 2D)
        armaSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + currentAmmo.ToString(); // Exibe a quantidade atual de munição
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
        somRecarga.Play();
    }

    public void escolherArma(int arma)
    {
        armaSpriteRenderer.enabled = true;
        chosenWeapon = arma;
        Debug.Log("Arma escolhida: " + chosenWeapon);

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

            currentPlayerStats.currentWeapon = chosenWeapon;
            currentPlayerStats.fireRate = fireRate;
            currentPlayerStats.maxAmmo = maxAmmo;
            currentPlayerStats.reloadTime = reloadTime;
            currentPlayerStats.bulletCount = bulletCount;
            currentPlayerStats.damage = weapon.pistolDamage;
            currentPlayerStats.bulletSpeed = weapon.pistolShotSpeed;
            currentPlayerStats.lifeTime = weapon.pistolRange;
            currentPlayerStats.spreadRadius = weapon.pistolSpread;
            currentPlayerStats.penetration = weapon.pistolPenetration;
            currentPlayerStats.maxHealth = weapon.pistolMaxHp;
            playerHealthSystem.maxHealth = weapon.pistolMaxHp;
            playerHealthSystem.sliderVida.maxValue = weapon.pistolMaxHp;
            playerHealthSystem.sliderVida.value = weapon.pistolMaxHp;

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

            currentPlayerStats.currentWeapon = chosenWeapon;
            currentPlayerStats.fireRate = fireRate;
            currentPlayerStats.maxAmmo = maxAmmo;
            currentPlayerStats.reloadTime = reloadTime;
            currentPlayerStats.bulletCount = bulletCount;
            currentPlayerStats.damage = weapon.shotgunDamage;
            currentPlayerStats.bulletSpeed = weapon.shotgunShotSpeed;
            currentPlayerStats.lifeTime = weapon.shotgunRange;
            currentPlayerStats.spreadRadius = weapon.shotgunSpread;
            currentPlayerStats.penetration = weapon.shotgunPenetration;
            currentPlayerStats.maxHealth = weapon.shotgunMaxHp;
            playerHealthSystem.maxHealth = weapon.shotgunMaxHp;
            playerHealthSystem.sliderVida.maxValue = weapon.shotgunMaxHp;
            playerHealthSystem.sliderVida.value = weapon.shotgunMaxHp;


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

            currentPlayerStats.currentWeapon = chosenWeapon;
            currentPlayerStats.fireRate = fireRate;
            currentPlayerStats.maxAmmo = maxAmmo;
            currentPlayerStats.reloadTime = reloadTime;
            currentPlayerStats.bulletCount = bulletCount;
            currentPlayerStats.damage = weapon.sniperDamage;
            currentPlayerStats.bulletSpeed = weapon.sniperShotSpeed;
            currentPlayerStats.lifeTime = weapon.sniperRange;
            currentPlayerStats.spreadRadius = weapon.sniperSpread;
            currentPlayerStats.penetration = weapon.sniperPenetration;
            currentPlayerStats.maxHealth = weapon.sniperMaxHp;
            playerHealthSystem.maxHealth = weapon.sniperMaxHp;
            playerHealthSystem.sliderVida.maxValue = weapon.sniperMaxHp;
            playerHealthSystem.sliderVida.value = weapon.sniperMaxHp;


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

            currentPlayerStats.currentWeapon = chosenWeapon;
            currentPlayerStats.fireRate = fireRate;
            currentPlayerStats.maxAmmo = maxAmmo;
            currentPlayerStats.reloadTime = reloadTime;
            currentPlayerStats.bulletCount = bulletCount;
            currentPlayerStats.damage = weapon.machineGunDamage;
            currentPlayerStats.bulletSpeed = weapon.machineGunShotSpeed;
            currentPlayerStats.lifeTime = weapon.machineGunRange;
            currentPlayerStats.spreadRadius = weapon.machineGunSpread;
            currentPlayerStats.penetration = weapon.machineGunPenetration;
            currentPlayerStats.maxHealth = weapon.machineGunMaxHp;
            playerHealthSystem.maxHealth = weapon.machineGunMaxHp;
            playerHealthSystem.sliderVida.maxValue = weapon.machineGunMaxHp;
            playerHealthSystem.sliderVida.value = weapon.machineGunMaxHp;

        }

        armaSpriteRenderer.sprite = spritesArma[chosenWeapon];
        currentPlayerStats.sprite = spritesArma[chosenWeapon];
        currentAmmo = maxAmmo;
        UpdateAmmoText();
        Time.timeScale = 1;
    }
}
