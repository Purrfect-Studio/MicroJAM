using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Health health; // Referência ao script Health

    // Start is called before the first frame update
    void Start()
    {
        // Inicialização, se necessário
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para aplicar dano ao inimigo
    public void ApplyDamage(float damage)
    {
        health.TakeDamage(damage); // Chama o método de dano do script Health
    }
}
