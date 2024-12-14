using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDamage : MonoBehaviour
{
    [Header("Configurações")]
    public string targetTag; // Tag do alvo (por exemplo, "enemy")
    public float damage; // Dano causado ao inimigo

    // Esse método é chamado quando o objeto com este script colide com outro objeto (via trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual colidimos tem a tag correta
        if (other.CompareTag(targetTag))
        {
            // Tenta pegar o script Health do objeto com o qual colidimos
            Health targetHealth = other.GetComponent<Health>();
            
            // Se o objeto tem o script Health, aplica o dano e exibe o dano flutuante
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage); // Chama o método para aplicar o dano
                Debug.Log("Dano causado: " + damage);
            }
        }
    }
}
