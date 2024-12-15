using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AtaquesBoss1 : MonoBehaviour
{
    public ControladorBoss1 controladorBoss1;

    [Header("Ataque")]
    public int ataqueAtual = 1; // 1=dash / 2=atirar / 3=invocar
    public float cooldownAtaque;
    private float cooldownRestanteAtaque;

    [Header("Dash")]
    public float speedDash = 6;
    private Vector2 direcaoDash;

    [Header("Atirar")]
    public GameObject projetilPrefab;
    private Vector2 direcaoTiro;
    public float projetilSpeed;

    [Header("Invocar")]
    public GameObject[] invocarPrefab;

    void Start()
    {
        controladorBoss1 = GetComponent<ControladorBoss1>();
        cooldownRestanteAtaque = cooldownAtaque;
    }

    void Update()
    {
        if (controladorBoss1.podeAtacar && !controladorBoss1.usandoAtaque)
        {
            if (cooldownRestanteAtaque <= 0)
            {
                if (ataqueAtual == 1)
                {
                    StartCoroutine(dash());
                }
                else if(ataqueAtual == 2)
                {
                    StartCoroutine(atirar());
                }
                else if(ataqueAtual == 3)
                {
                    StartCoroutine(invocar());
                }
            }
            else
            {
                cooldownRestanteAtaque -= Time.deltaTime;
            }
        }
    }


    IEnumerator dash()
    {
        controladorBoss1.usandoAtaque = true;
        direcaoDash = (controladorBoss1.player.transform.position - this.transform.position);
        yield return new WaitForSeconds(0.2f);
        controladorBoss1.rigidbody2d.velocity = direcaoDash * speedDash;
        yield return new WaitForSeconds(1.5f);
        cooldownRestanteAtaque = cooldownAtaque;
        controladorBoss1.usandoAtaque = false;
        ataqueAtual = 2;
    }

    IEnumerator atirar()
    {
        controladorBoss1.usandoAtaque = true;
        direcaoTiro = (controladorBoss1.player.transform.position - this.transform.position);
        GameObject projetil = Instantiate(projetilPrefab, transform.position, Quaternion.identity);
        projetil.GetComponent<Rigidbody2D>().velocity = direcaoTiro * projetilSpeed;
        yield return new WaitForSeconds(3f);
        cooldownRestanteAtaque = cooldownAtaque;
        controladorBoss1.usandoAtaque = false;
        ataqueAtual = 3;
    }

    IEnumerator invocar()
    {
        controladorBoss1.usandoAtaque = true;
        GameObject inimigo = Instantiate(invocarPrefab[escolherInimigo()], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        cooldownRestanteAtaque = cooldownAtaque;
        controladorBoss1.usandoAtaque = false;
        ataqueAtual = 1;
    }

    public int escolherInimigo()
    {
        return Random.Range(0, invocarPrefab.Length);
    }
}