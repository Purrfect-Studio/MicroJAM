using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquesBoss2 : MonoBehaviour
{
    private ControladorBoss2 controladorBoss2;

    [Header("Ataques")]
    public int ataqueAtual = 1;
    public float cooldownAtaque;
    private float cooldownRestanteAtaque;

    [Header("Explosao")]
    public float danoExplosao;
    public float areaDeDistancia = 3f; // Raio de distância segura do alvo
    public float distanciaAoAlvo;
    public float aumentoSpeed;

    [Header("Ranged")]
    public float danoRanged1;

    // Start is called before the first frame update
    void Start()
    {
        controladorBoss2 = GetComponent<ControladorBoss2>();
        cooldownRestanteAtaque = cooldownAtaque;
    }

    // Update is called once per frame
    void Update()
    {
        if (controladorBoss2.podeAtacar && !controladorBoss2.usandoAtaque)
        {
            if (cooldownRestanteAtaque <= 0)
            {
                if (ataqueAtual == 1)
                {
                    controladorBoss2.usandoExplosao = true;
                    controladorBoss2.usandoAtaque = true;
                }
                else if (ataqueAtual == 2)
                {
                    //StartCoroutine(atirar());
                }
                else if (ataqueAtual == 3)
                {
                    //StartCoroutine(invocar());
                }
            }
            else
            {
                if(cooldownRestanteAtaque > -1)
                {
                    cooldownRestanteAtaque -= Time.deltaTime;
                }
            }
        }

        distanciaAoAlvo = Vector2.Distance(transform.position, controladorBoss2.player.transform.position);
        if (controladorBoss2.usandoExplosao)
        {
            if (distanciaAoAlvo < areaDeDistancia)
            {
                StartCoroutine(Explosao());
                controladorBoss2.usandoExplosao = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, controladorBoss2.player.transform.position, (controladorBoss2.speed + aumentoSpeed) * Time.deltaTime);
            }
        }
    }

    IEnumerator Explosao()
    {
        controladorBoss2.animator.SetTrigger("Explosion");
        yield return new WaitForSeconds(3f);
        controladorBoss2.usandoAtaque = false;
        controladorBoss2.usandoExplosao = false;
        cooldownRestanteAtaque = cooldownAtaque;
        ataqueAtual = 2;
    }

    public void aumentarDanoColisao()
    {
        controladorBoss2.danoOnHit.damage += danoExplosao;
    }

    public void diminuirDanoColisao()
    {
        controladorBoss2.danoOnHit.damage -= danoExplosao;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaDeDistancia);
    }

}
