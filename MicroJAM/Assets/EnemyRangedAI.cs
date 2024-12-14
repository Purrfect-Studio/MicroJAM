using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TargetsScanner))]
public class EnemyRangedAI : MonoBehaviour
{
    public float speed = 3f; // Velocidade do inimigo
    public float areaDeDistancia = 5f; // Raio de distância segura do alvo

    private TargetsScanner targetsScanner;
    public GameObject target; // Alvo mais próximo
    public Vector2 pontoDestino; // Próximo ponto de destino
    public bool emMovimento = false;
    public float raioMovimentoAleatorio = 10f; // Raio de movimento aleatório


    void Start()
    {
        targetsScanner = GetComponent<TargetsScanner>();
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
            }
            else
            {
                // Se não houver alvo, mover para posição aleatória
                EscolherNovoPontoAleatorio();
            }

            // Move até o ponto de destino
            yield return StartCoroutine(MoverParaPontoDestino());

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
