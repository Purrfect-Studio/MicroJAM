using UnityEngine;

public class Atirar : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab do projétil
    public float fireRate = 0.2f; // Taxa de disparo (tempo entre cada tiro)
    
    private float fireCooldown = 0f; // Contador para o cooldown do disparo

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Obtém a posição do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Definimos Z como 0 pois estamos em 2D

        // Rotaciona o player para olhar na direção do mouse
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Dispara projéteis automaticamente (se o cooldown permitir)
        if (fireCooldown <= 0f)
        {
            Fire(mousePosition);
            fireCooldown = fireRate; // Reinicia o cooldown
        }
    }

    void Fire(Vector3 targetPosition)
    {
        // Instancia o projétil na posição do player
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // Calcula a direção do projétil em relação ao mouse
        Vector2 direction = (targetPosition - transform.position).normalized;
        
        // Define a direção do projétil
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}
