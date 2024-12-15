using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Health health; // Referência ao script Health
    private Animator animator; // Referência ao componente Animator
    private Vector3 ultimaPosicao; // Para detectar se o inimigo está parado
    private float movimentoThreshold = 0.01f; // Limite para considerar que o inimigo está se movendo
    private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer do inimigo
    private Transform player; // Referência ao transform do player (encontrado dinamicamente)

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Obtém o componente Animator anexado ao inimigo
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o componente SpriteRenderer do inimigo
        ultimaPosicao = transform.position; // Salva a posição inicial do inimigo
        
        // Busca o player pela tag "Player"
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        VerificarMovimento();
        FliparSprite(); // Chama o método para flipar o sprite
    }

    // Método para verificar se o inimigo está parado ou se está se movendo
    private void VerificarMovimento()
    {
        float distanciaMovida = Vector3.Distance(transform.position, ultimaPosicao);

        if (distanciaMovida > movimentoThreshold) 
        {
            // Se o inimigo se moveu mais que o limite, ativa a animação Walk
            animator.SetBool("isWalking", true);
        } 
        else 
        {
            // Se não se moveu, ativa a animação Idle
            animator.SetBool("isWalking", false);
        }

        // Atualiza a posição para a próxima verificação
        ultimaPosicao = transform.position;
    }

    // Método para aplicar dano ao inimigo
    public void ApplyDamage(float damage)
    {
        health.TakeDamage(damage); // Chama o método de dano do script Health
    }

    // Método para flipar o sprite do inimigo dependendo da posição do player
    private void FliparSprite()
    {
        // Verifica a posição do player em relação ao inimigo
        if (player.position.x < transform.position.x)
        {
            // Player está à esquerda, inverter o sprite (flip horizontal)
            spriteRenderer.flipX = true;
        }
        else
        {
            // Player está à direita, manter o sprite normal
            spriteRenderer.flipX = false;
        }
    }
}
