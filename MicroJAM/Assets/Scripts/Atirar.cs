using UnityEngine;
using System.Collections;

public class Atirar : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab do projétil
    public float fireRate = 0.2f; // Taxa de disparo (tempo entre cada tiro)
    public int maxAmmo = 10; // Máximo de munição
    public int currentAmmo; // Munição atual
    public float reloadTime = 2f; // Tempo de recarga
    private bool isReloading = false; // Verifica se está recarregando

    private float fireCooldown = 0f; // Contador para o cooldown do disparo

    void Start()
    {
        currentAmmo = maxAmmo; // Inicializa a munição atual
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
            }
            else
            {
                StartCoroutine(Reload()); // Inicia a recarga quando a munição acabar
            }
        }
    }

    void Fire()
    {
        // Instancia o projétil na posição do player
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // A direção será configurada no próprio método Start() do projétil
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
    }
}
