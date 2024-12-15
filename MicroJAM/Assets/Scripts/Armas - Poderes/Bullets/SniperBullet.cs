using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float speed; // Velocidade do proj�til
    public float lifeTime; // Tempo de vida antes de ser destru�do
    public float spreadRadius; // Raio de dispers�o ao redor do mouse
    public float penetration;

    public Weapons weapons;

    private Vector2 direction;

    void Start()
    {

        // Obt�m a posi��o do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Definimos Z como 0 pois estamos em 2D

        Vector2 randomOffset = Random.insideUnitCircle * spreadRadius;
        Vector2 targetPosition = new Vector2(mousePosition.x, mousePosition.y) + randomOffset;

        // Calcula a dire��o em rela��o � posi��o do mouse
        direction = (targetPosition - (Vector2)transform.position).normalized;
        Destroy(gameObject, lifeTime);

    }

    void Update()
    {
        // Move o proj�til na dire��o fornecida
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
        /*if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }*/
         if (collision.gameObject.CompareTag("Vaso"))
        {
                Destroy(collision.gameObject);
        }
    }
}
