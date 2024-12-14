using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDamage : MonoBehaviour
{
    [Header("Configurações")]
    public string targetTag = "Player"; // Tag do alvo (por exemplo, "enemy")
    public int damage = 10; // Dano causado ao inimigo

    // Esse método é chamado quando o objeto com este script colide com outro objeto (via trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual colidimos tem a tag correta
        if (other.CompareTag(targetTag))
        {
            // Tenta pegar o script Health do objeto com o qual colidimos
            Health targetHealth = other.GetComponent<Health>();
            
            // Se o objeto tem o script Health, diminui a vida
            if (targetHealth != null)
            {
                targetHealth.life -= damage; // Diminui a vida do alvo
                Debug.Log("Dano causado: " + damage);
            }
        }
    }
}
