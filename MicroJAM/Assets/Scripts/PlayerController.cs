using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public SpriteRenderer spriteRenderer;
    public Animator playerAnimator;
    private Vector2 playerDirection;
    private Vector2 lastPlayerDirection;
    public float playerSpeed = 5f;
    public float KBForce = 10f;
    public float KBCount = 0f; // Será inicializado na colisão
    public float KBTime = 0.2f; // Duração do knockback
    public bool isKnockRight;
    public float invulnerabilityDuration = 0.5f;
    public bool isInvulnerable = false;

    void Start()
    {   
        playerAnimator = GetComponent<Animator>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX == 0 && moveY == 0 && (playerDirection.x != 0 || playerDirection.y != 0)) 
        {
            lastPlayerDirection = playerDirection;
        }

        playerDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Calcula a velocidade de movimento normal
        Vector2 moveVelocity = playerDirection * playerSpeed;

        // Se ainda estiver em knockback, adiciona o knockback à velocidade
        if (KBCount > 0) 
        {
            float knockbackX = isKnockRight ? -1 : 1; // Define a direção do knockback (esquerda ou direita)
            Vector2 knockbackVelocity = new Vector2(knockbackX * KBForce, 0);
            moveVelocity += knockbackVelocity; // Soma a velocidade do knockback
            KBCount -= Time.fixedDeltaTime; // Diminui o tempo de knockback
        }

        // Move o jogador com a soma da velocidade do movimento e do knockback
        playerRigidBody2D.velocity = moveVelocity;
    }

    public IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red; // Indica que o player está invulnerável

        yield return new WaitForSeconds(invulnerabilityDuration);

        spriteRenderer.color = originalColor; // Retorna a cor original
        isInvulnerable = false;
    }
}
