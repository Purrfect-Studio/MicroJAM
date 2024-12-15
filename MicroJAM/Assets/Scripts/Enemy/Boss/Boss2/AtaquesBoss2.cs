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
    public float areaDeDistancia; // Raio de distância segura do alvo
    private float distanciaAoAlvo;
    public float aumentoSpeed;

    [Header("Shotgun")]
    public GameObject projetilShotgunPrefab;
    public float danoShotgun;
    public float projetilShotgunSpeed;
    private Vector2 direcaoMoverShotgun;

    [Header("Invocar")]
    public GameObject[] invocarPrefab;
    public int inimigosInvocados;

    // Start is called before the first frame update
    void Start()
    {
        controladorBoss2 = GetComponent<ControladorBoss2>();
        cooldownRestanteAtaque = cooldownAtaque;
        projetilShotgunPrefab.GetComponent<DarDanoNoPlayer>().damage = danoShotgun;
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
                    StartCoroutine(atirarShotgun());
                }
                else if (ataqueAtual == 2)
                {
                    controladorBoss2.usandoExplosao = true;
                    controladorBoss2.usandoAtaque = true;
                }
                else if (ataqueAtual == 3)
                {
                    StartCoroutine(invocar());
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

    IEnumerator atirarShotgun()
    {
        controladorBoss2.usandoAtaque = true;
        controladorBoss2.animator.SetTrigger("Ranged");
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < 8; i++)
        {
            // Gera uma posição aleatória dentro de um círculo de spreadRadius em torno do mouse (somente X e Y)
            Vector2 randomOffset = Random.insideUnitCircle * 6f;
            Vector2 targetPosition = new Vector2(controladorBoss2.player.transform.position.x, controladorBoss2.player.transform.position.y) + randomOffset;
            // Calcula a direção em relação à posição aleatória calculada
            direcaoMoverShotgun = (targetPosition - (Vector2)transform.position).normalized;
            GameObject projetil = Instantiate(projetilShotgunPrefab, transform.position, Quaternion.identity);
            projetil.GetComponent<Rigidbody2D>().velocity = direcaoMoverShotgun * projetilShotgunSpeed;
            Destroy(projetil, 3);
        }
        yield return new WaitForSeconds(2f);
        controladorBoss2.usandoAtaque = false;
        cooldownRestanteAtaque = cooldownAtaque;
        ataqueAtual = 2;
    }

    IEnumerator Explosao()
    {
        controladorBoss2.animator.SetTrigger("Explosion");
        yield return new WaitForSeconds(3f);
        controladorBoss2.usandoAtaque = false;
        controladorBoss2.usandoExplosao = false;
        cooldownRestanteAtaque = cooldownAtaque;
        ataqueAtual = 3;
    }

    IEnumerator invocar()
    {
        controladorBoss2.usandoAtaque = true;
        controladorBoss2.animator.SetTrigger("Ranged");
        yield return new WaitForSeconds(0.8f);
        for(int i =0; i <= inimigosInvocados; i++)
        {
            GameObject inimigo = Instantiate(invocarPrefab[escolherInimigo()], transform.position, Quaternion.identity);
            inimigo.GetComponent<SistemaDeDrop>().enabled = false;
        }
        yield return new WaitForSeconds(2.3f);
        cooldownRestanteAtaque = cooldownAtaque;
        controladorBoss2.usandoAtaque = false;
        ataqueAtual = 1;
    }

    private int escolherInimigo()
    {
        return Random.Range(0, invocarPrefab.Length);
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
