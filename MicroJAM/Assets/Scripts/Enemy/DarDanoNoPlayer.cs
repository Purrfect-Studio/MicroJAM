using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarDanoNoPlayer : MonoBehaviour
{
    GameObject player;
    public PlayerHealthSystem playerHealthSystem;
    public PlayerController playerController;
    public float damage = 1;

    [Header("Projetil")]
    public bool projetil = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
        playerHealthSystem = player.GetComponent<PlayerHealthSystem>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerController.isInvulnerable)
        {
            playerController.KBCount = playerController.KBTime; // Inicia o knockback
            playerController.isKnockRight = collision.transform.position.x < transform.position.x; // Define a direção do knockback

            playerHealthSystem.takeDamage(damage); // Reduz a vida do player

            StartCoroutine(playerController.InvulnerabilityCoroutine()); // Ativa a invulnerabilidade
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerController.isInvulnerable)
        {
            playerController.KBCount = playerController.KBTime; // Inicia o knockback
            playerController.isKnockRight = collision.transform.position.x < transform.position.x; // Define a direção do knockback

            playerHealthSystem.takeDamage(damage); // Reduz a vida do player

            StartCoroutine(playerController.InvulnerabilityCoroutine()); // Ativa a invulnerabilidade
        }
        if(collision.gameObject.CompareTag("Wall") && projetil)
        {
            Destroy(gameObject);
        }
    }
}
