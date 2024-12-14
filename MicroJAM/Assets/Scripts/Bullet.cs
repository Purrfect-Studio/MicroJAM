using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidade do projétil
    public float lifeTime = 3f; // Tempo de vida antes de ser destruído

    private Vector2 direction;

    void Start()
    {
        // Obtém a posição do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Definimos Z como 0 pois estamos em 2D

        // Calcula a direção em relação à posição do mouse
        direction = (mousePosition - transform.position).normalized;
        Destroy(gameObject, lifeTime);

    }

    void Update()
    {
        // Move o projétil na direção fornecida
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10f);
            }
            Destroy(gameObject);
        }
    }
}
