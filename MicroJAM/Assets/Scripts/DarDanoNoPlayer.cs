using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarDanoNoPlayer : MonoBehaviour
{
    public PlayerHealthSystem playerHealthSystem;
    public PlayerController playerController;
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerController.isInvulnerable)
        {
            playerController.KBCount = playerController.KBTime; // Inicia o knockback
            playerController.isKnockRight = collision.transform.position.x < transform.position.x; // Define a direção do knockback

            playerHealthSystem.vida -= damage; // Reduz a vida do player

            StartCoroutine(playerController.InvulnerabilityCoroutine()); // Ativa a invulnerabilidade
        }
    }
}
