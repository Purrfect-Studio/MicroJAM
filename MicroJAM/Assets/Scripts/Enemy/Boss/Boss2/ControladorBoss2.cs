using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBoss2 : MonoBehaviour
{
    public GameObject player;
    private TargetsScanner targetsScanner;
    public GameObject target; // Alvo mais próximo
    public Rigidbody2D rigidbody2d;
    public DarDanoNoPlayer danoOnHit;
    private HealthBoss2 healthBoss;
    private AtaquesBoss2 ataquesBoss;

    public Animator animator;
    SpriteRenderer sprite;
    private bool olhandoDireita;
    public bool podeAtacar;
    public bool usandoAtaque = false;
    public bool usandoExplosao = false;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        targetsScanner = GetComponent<TargetsScanner>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        danoOnHit = GetComponent<DarDanoNoPlayer>();
        ataquesBoss = GetComponent<AtaquesBoss2>();

        player = GameObject.FindGameObjectWithTag("Player");
        ataquesBoss.aumentoSpeed = player.GetComponent<PlayerController>().playerSpeed;

        Physics2D.IgnoreLayerCollision(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        targetsScanner.ScanArea();
        target = targetsScanner.GetClosestTarget();
        flip();

        if (!usandoAtaque && !usandoExplosao)
        {
            if (target != null)
            {
                podeAtacar = true;
                animator.SetBool("estaAndando", false);
            }
            else
            {
                podeAtacar = false;
                animator.SetBool("estaAndando", true);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }

    void flip()
    {
        if (transform.position.x - player.transform.position.x > 0 && olhandoDireita || transform.position.x - player.transform.position.x < 0 && !olhandoDireita)
        {
            olhandoDireita = !olhandoDireita;
            sprite.flipX = !sprite.flipX;
        }
    }
}
