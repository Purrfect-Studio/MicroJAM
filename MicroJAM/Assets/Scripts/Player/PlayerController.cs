using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public SpriteRenderer spriteRenderer;
    public Animator playerAnimator;
    private Vector2 playerDirection;
    private Vector2 lastPlayerDirection; // Direção do último movimento
    public float playerSpeed = 5f;
    public float stairSpeed = 2.5f; // Velocidade reduzida nas escadas
    private float currentSpeed; // Velocidade atual
    public float KBForce = 10f;
    public float KBCount = 0f; // Será inicializado na colisão
    public float KBTime = 0.2f; // Duração do knockback
    public bool isKnockRight;
    public float invulnerabilityDuration = 0.1f;
    public bool isInvulnerable = false;

    [Header("Audio Settings")]
    public AudioSource footstepsAudioSource; // AudioSource já atribuído no Inspector
    private bool isFootstepsPlaying = false; // Verifica se o som está tocando

    void Start()
    {   
        playerAnimator = GetComponent<Animator>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpeed = playerSpeed; // Inicia com a velocidade normal

        // Se o áudio de passos não estiver atribuído, emite um aviso
        if (footstepsAudioSource == null)
        {
            Debug.LogWarning("AudioSource de passos não atribuído.");
        }
    }

    void Update()
    {   
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Atualiza a direção do jogador
        playerDirection = new Vector2(moveX, moveY).normalized;

        // Se o jogador está se movendo, atualiza a última direção
        if (playerDirection != Vector2.zero)
        {
            lastPlayerDirection = playerDirection;
        }

        // Define os parâmetros do Blend Tree
        playerAnimator.SetFloat("XInput", lastPlayerDirection.x); 
        playerAnimator.SetFloat("YInput", lastPlayerDirection.y);

        // Indica se o jogador está em movimento ou parado
        bool isMoving = playerDirection.magnitude > 0;
        playerAnimator.SetBool("isMoving", isMoving);

        // Se o jogador estiver se movendo e o áudio ainda não estiver tocando, toca o áudio de passos
        if (isMoving && !isFootstepsPlaying)
        {
            PlayFootsteps();
        }
        // Se o jogador parar de se mover e o áudio estiver tocando, pare o áudio de passos
        else if (!isMoving && isFootstepsPlaying)
        {
            StopFootsteps();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Calcula a velocidade de movimento normal
        Vector2 moveVelocity = playerDirection * currentSpeed;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Reduz a velocidade se entrar em uma área com a tag "Stairs"
        if (other.CompareTag("Stair"))
        {
            currentSpeed = stairSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Restaura a velocidade normal ao sair da área com a tag "Stairs"
        if (other.CompareTag("Stair"))
        {
            currentSpeed = playerSpeed;
        }
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

    // Função para tocar o áudio de passos
    private void PlayFootsteps()
    {
        if (footstepsAudioSource != null)
        {
            footstepsAudioSource.loop = true; // Loop para tocar o áudio continuamente
            footstepsAudioSource.Play();
            isFootstepsPlaying = true;
        }
    }

    // Função para parar o áudio de passos
    private void StopFootsteps()
    {
        if (footstepsAudioSource != null)
        {
            footstepsAudioSource.Stop();
            isFootstepsPlaying = false;
        }
    }
}
