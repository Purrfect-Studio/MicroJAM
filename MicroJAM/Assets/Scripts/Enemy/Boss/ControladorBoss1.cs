using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBoss1 : MonoBehaviour
{
    private TargetsScanner targetsScanner;
    public GameObject target; // Alvo mais próximo
    public GameObject player;
    public Rigidbody2D rigidbody2d;
    SpriteRenderer sprite;


    public float speed;

    private bool olhandoDireita;

    public bool usandoAtaque;
    public bool podeAtacar;

    // Start is called before the first frame update
    void Start()
    {
        targetsScanner = GetComponent<TargetsScanner>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreLayerCollision(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        targetsScanner.ScanArea();
        target = targetsScanner.GetClosestTarget();
        flip();
        if(!usandoAtaque)
        {
            if (target != null)
            {
                podeAtacar = true;
            }
            else
            {
                podeAtacar = false;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }

    void flip()
    {
        if(transform.position.x - player.transform.position.x > 0 && olhandoDireita || transform.position.x - player.transform.position.x < 0 && !olhandoDireita) 
        {
            olhandoDireita = !olhandoDireita;
            sprite.flipX = !sprite.flipX;
            //float x = transform.localScale.x;
            //x *= -1;
            //transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }
    }
}
