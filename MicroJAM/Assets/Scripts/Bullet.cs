using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidade do projétil
    public float lifeTime = 3f; // Tempo de vida antes de ser destruído

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // Normaliza para que a velocidade seja consistente
    }

    void Update()
    {
        // Move o projétil na direção fornecida
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void Start()
    {
        // Destroi o projétil após 'lifeTime' segundos
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Tag detectada: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
