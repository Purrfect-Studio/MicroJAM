using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody2D;
    public SpriteRenderer spriteRenderer;
    //public AudioSource footStepAudioSource;
    //public AudioClip[] soundEffects;
    public Animator playerAnimator;
    private Vector2 playerDirection;
    private Vector2 lastPlayerDirection;
    public float playerSpeed;
    public float KBForce;
    public float KBCount;
    public float KBTime;
    public bool isKnockRight;
    public float invulnerabilityDuration = 2f;
    public bool isInvulnerable = false;
    //public float stepVolume = 0.05f;
    public bool canMove = true;

    void Start()
    {   
        playerAnimator = GetComponent<Animator>();
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        if ( moveX == 0 && moveY == 0 && (playerDirection.x != 0 || playerDirection.y != 0)) {

            lastPlayerDirection = playerDirection;
        }

        playerDirection = new Vector2(moveX, moveY).normalized;
      
    }

    void FixedUpdate()
    {
        KnockLogic();
    }

    void KnockLogic(){
        if (KBCount < 0)
        {
            Move();
        } else
        {
            if (isKnockRight)
            {
                playerRigidBody2D.velocity = new Vector2(-KBCount, KBForce);
            } else
            {
                playerRigidBody2D.velocity = new Vector2(+KBCount, KBForce);
            }
        }
        KBCount -= Time.deltaTime;
    }


    void Move(){

        playerRigidBody2D.MovePosition(playerRigidBody2D.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
        
        if(true){ //footStepAudioSource != null
            if (playerDirection.sqrMagnitude > 0 && !footStepAudioSource.isPlaying)
            {
                //footStepAudioSource.clip = soundEffects[0];
                //footStepAudioSource.loop = true;
                //footStepAudioSource.Play();
            } 

            if(playerDirection.sqrMagnitude <= 00)//&& footStepAudioSource.isPlaying
            {
                //footStepAudioSource.Stop();
            }
        }
    }



    public IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red; // Troca a cor para vermelho ou qualquer outra cor de sua escolha

        yield return new WaitForSeconds(invulnerabilityDuration);

        spriteRenderer.color = originalColor; // Retorna à cor original
        isInvulnerable = false;
    }
}
