using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent(typeof(TargetsScanner))]
public class EnemyRangedAI : MonoBehaviour
{
    public float speed = 3f; // Velocidade do inimigo
    public float areaDeDistancia = 10f; // Raio de distância segura do alvo

    public TargetsScanner targetsScanner;
    public GameObject target; // Alvo mais próximo
    public Vector2 pontoDestino; // Próximo ponto de destino
    public bool emMovimento = false;
    public float raioMovimentoAleatorio = 10f; // Raio de movimento aleatório

    private bool followPlayer = true;
    private GameObject player;

    [Header("Tipo de inimigo")]
    public bool sniper;
    public bool shotgun;

    [Header("Projetil Sniper")]
    public GameObject projetilSniperPrefab;
    public float cooldownAtirarSniper;
    private Vector2 direcaoMoverSniper;
    public float projetilSniperSpeed;

    [Header("Projetil Shotgun")]
    public GameObject projetilShotgunPrefab;
    public float cooldownAtirarShotgun;
    private Vector2 direcaoMoverShotgun;
    public float projetilShotgunSpeed;

    void Start()
    {
        targetsScanner = GetComponent<TargetsScanner>();
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreLayerCollision(3, 7);

        StartCoroutine(AcoesInimigo());
    }


    IEnumerator AcoesInimigo()
    {
        while (true)
        {
            // Atualiza o alvo mais próximo
            targetsScanner.ScanArea();
            target = targetsScanner.GetClosestTarget();

            if (target != null)
            {
                if(followPlayer)
                {
                    GetComponent<EnemyAI>().enabled = false;
                    followPlayer = false;
                }
                float distanciaAoAlvo = Vector2.Distance(transform.position, target.transform.position);

                if (distanciaAoAlvo < areaDeDistancia)
                {
                    // Alvo está dentro da área de distância: mover para longe
                    AfastarDoAlvo();
                }
                else
                {
                    // Alvo está fora da área de distância: mover para posição aleatória
                    EscolherNovoPontoAleatorio();
                }

                if (sniper)
                {
                    GameObject projetil = Instantiate(projetilSniperPrefab, transform.position, Quaternion.identity);
                    direcaoMoverSniper = (player.transform.position - this.transform.position);
                    projetil.GetComponent<Rigidbody2D>().velocity = direcaoMoverSniper * projetilSniperSpeed;
                    Destroy(projetil, 4);
                    yield return new WaitForSeconds(cooldownAtirarSniper);
                }

                if(shotgun)
                {

                    for(int i=0; i<6; i++)
                    {
                        // Gera uma posição aleatória dentro de um círculo de spreadRadius em torno do mouse (somente X e Y)
                        Vector2 randomOffset = Random.insideUnitCircle * 6f;
                        Vector2 targetPosition = new Vector2(player.transform.position.x, player.transform.position.y) + randomOffset;
                        // Calcula a direção em relação à posição aleatória calculada
                        direcaoMoverShotgun = (targetPosition - (Vector2)transform.position).normalized;
                        GameObject projetil = Instantiate(projetilShotgunPrefab, transform.position, Quaternion.identity);
                        projetil.GetComponent<Rigidbody2D>().velocity = direcaoMoverShotgun * projetilShotgunSpeed;
                        Destroy(projetil, 2);
                    }
                    yield return new WaitForSeconds(cooldownAtirarShotgun);
                }

            }
            
            if (!followPlayer && target == null)
            {
                GetComponent<EnemyAI>().enabled = true;
                followPlayer = true;
            }
            // Se não houver alvo, mover para posição aleatória
            //EscolherNovoPontoAleatorio();


            // Move até o ponto de destino
            if (!followPlayer)
            {
                yield return StartCoroutine(MoverParaPontoDestino());
            }

            // Aguarda um intervalo antes de executar a próxima ação
            yield return new WaitForSeconds(speed);
        }
    }

    void AfastarDoAlvo()
    {
        if (target == null) return;

        // Calcula a direção oposta ao alvo
        Vector2 direcaoOposta = (Vector2)transform.position - (Vector2)target.transform.position;
        float scanRadius = targetsScanner.scanRadius;

        // Calcula o ponto de destino dentro do raio permitido
        pontoDestino = CalcularPontoDentroDoRaio((Vector2)transform.position + direcaoOposta.normalized * areaDeDistancia, scanRadius);
    }

    void EscolherNovoPontoAleatorio()
    {
        float scanRadius = targetsScanner.scanRadius;

        // Ajusta o raio de movimento aleatório dinamicamente para nunca ultrapassar o scanRadius
        float raioPermitido = Mathf.Min(scanRadius, raioMovimentoAleatorio);

        // Gera um ponto aleatório dentro do raio permitido
        Vector2 posicaoAleatoria = (Vector2)transform.position + Random.insideUnitCircle * raioPermitido;

        // Calcula o ponto final garantindo que ele esteja dentro do scanRadius
        pontoDestino = CalcularPontoDentroDoRaio(posicaoAleatoria, scanRadius);
    }

    Vector2 CalcularPontoDentroDoRaio(Vector2 pontoDesejado, float raioMaximo)
    {
        // Verifica se o ponto está fora do raio máximo permitido
        if (Vector2.Distance(transform.position, pontoDesejado) > raioMaximo)
        {
            // Recalcula o ponto para ficar dentro do raio
            Vector2 direcao = (pontoDesejado - (Vector2)transform.position).normalized;
            pontoDesejado = (Vector2)transform.position + direcao * (raioMaximo * 0.9f); // Mantém ligeiramente dentro do limite
        }

        return pontoDesejado;
    }

    IEnumerator MoverParaPontoDestino()
    {
        emMovimento = true;

        while (Vector2.Distance(transform.position, pontoDestino) > 0.1f)
        {
            // Move o inimigo gradualmente em direção ao ponto de destino
            transform.position = Vector2.MoveTowards(transform.position, pontoDestino, speed * Time.deltaTime);
            yield return null; // Espera o próximo frame
        }

        emMovimento = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaDeDistancia);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetsScanner != null ? targetsScanner.scanRadius : 10f);

        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, pontoDestino);
            Gizmos.DrawSphere(pontoDestino, 0.3f);
        }
    }
}
