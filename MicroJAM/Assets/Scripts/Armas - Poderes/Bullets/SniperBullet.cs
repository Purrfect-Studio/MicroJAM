using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float speed = 10f; // Velocidade do projétil
    public float lifeTime = 3f; // Tempo de vida antes de ser destruído
    public float spreadRadius = 1f; // Raio de dispersão ao redor do mouse
    public float penetration = 0f;
    private OnHitDamage onHitDamage;

    public Weapons weapons;

    private Vector2 direction;

    void Start()
    {
        onHitDamage = GetComponent<OnHitDamage>();

        onHitDamage.damage = weapons.sniperDamage;
        speed = weapons.sniperShotSpeed;
        lifeTime = weapons.sniperRange;
        spreadRadius = weapons.sniperSpread;
        penetration = weapons.sniperPenetration;

        // Obtém a posição do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Definimos Z como 0 pois estamos em 2D

        Vector2 randomOffset = Random.insideUnitCircle * spreadRadius;
        Vector2 targetPosition = new Vector2(mousePosition.x, mousePosition.y) + randomOffset;

        // Calcula a direção em relação à posição do mouse
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
            if (penetration > 0)
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
