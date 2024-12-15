using UnityEngine;

[RequireComponent(typeof(TargetsScanner))]
public class EnemyAI : MonoBehaviour
{
    GameObject player;
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreLayerCollision(10, 7);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    /*[Header("Configurações de Movimento")]
    public float moveSpeed = 3f; // Velocidade de movimento padrão
    public float maxMoveSpeed = 5f; // Velocidade máxima que o inimigo pode atingir
    public float acceleration = 2f; // Taxa de aceleração
    public float deceleration = 5f; // Taxa de desaceleração quando o inimigo é empurrado
    public float stopDistance = 1f; // Distância mínima antes de parar

    private float currentSpeed; // Velocidade atual do inimigo
    private TargetsScanner scanner; // Referência ao script de varredura
    private GameObject target; // Alvo atual
    private float targetDistance; // Distância até o alvo atual
    private Vector2 lastPosition; // Última posição para verificar se o inimigo foi empurrado
    private Vector2 velocity; // Velocidade acumulada

    void Start()
    {
        scanner = GetComponent<TargetsScanner>();
        currentSpeed = moveSpeed; // Começa com a velocidade inicial
    }

    void Update()
    {
        // Atualiza o alvo mais próximo
        scanner.ScanArea();
        target = scanner.GetClosestTarget();
        targetDistance = scanner.GetClosestDistance();

        // Verifica se o inimigo foi empurrado ou saiu da rota
        CheckForMovement();

        // Move-se em direção ao alvo com aceleração
        FollowTarget();
    }

    void FollowTarget()
    {
        if (target == null || targetDistance <= stopDistance)
        {
            return; // Não faz nada se não houver alvo ou já estiver próximo o suficiente
        }

        // Calcula a direção do inimigo até o alvo
        Vector2 targetPosition = target.transform.position;

        // Move o inimigo gradualmente em direção ao alvo com aceleração
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Acumula a velocidade do inimigo com aceleração
        velocity += direction * acceleration * Time.deltaTime;

        // Limita a velocidade para não ultrapassar o máximo
        velocity = Vector2.ClampMagnitude(velocity, currentSpeed);

        // Atualiza a posição do inimigo
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    void CheckForMovement()
    {
        // Se o inimigo se moveu, acelera até a velocidade máxima
        if ((Vector2)transform.position != lastPosition)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxMoveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // Se o inimigo não se moveu, desacelera até a velocidade inicial
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, deceleration * Time.deltaTime);
        }

        // Atualiza a última posição
        lastPosition = transform.position;
    }*/
}
