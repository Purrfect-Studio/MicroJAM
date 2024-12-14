using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float speed; // Velocidade do projétil
    public float lifeTime; // Tempo de vida antes de ser destruído
    public float spreadRadius; // Raio de dispersão ao redor do mouse
    public float penetration;

    private Vector2 direction;

    public Weapons weapons;

    void Start()
    {
        // Obtém a posição do mouse no mundo e z é 0 pois estamos em 2D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Garantir que o Z seja 0 para o contexto 2D

        // Gera uma posição aleatória dentro de um círculo de spreadRadius em torno do mouse (somente X e Y)
        Vector2 randomOffset = Random.insideUnitCircle * spreadRadius;
        Vector2 targetPosition = new Vector2(mousePosition.x, mousePosition.y) + randomOffset;

        // Calcula a direção em relação à posição aleatória calculada
        direction = (targetPosition - (Vector2)transform.position).normalized;
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
            if(penetration > 0)
            {
                penetration -= 1;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
