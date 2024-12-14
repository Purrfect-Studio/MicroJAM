using UnityEngine;

[RequireComponent(typeof(TargetsScanner))]
public class EnemyAI : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float moveSpeed = 3f; // Velocidade de movimento
    public float stopDistance = 1f; // Distância mínima antes de parar

    private TargetsScanner scanner; // Referência ao script de varredura
    private GameObject target; // Alvo atual
    private float targetDistance; // Distância até o alvo atual

    void Start()
    {
        scanner = GetComponent<TargetsScanner>();
    }

    void Update()
    {
        // Atualiza o alvo mais próximo
        scanner.ScanArea();
        target = scanner.GetClosestTarget();
        targetDistance = scanner.GetClosestDistance();

        // Move-se em direção ao alvo, se aplicável
        FollowTarget();
    }

    void FollowTarget()
    {
        if (target == null || targetDistance <= stopDistance)
        {
            return; // Não faz nada se não houver alvo ou já estiver próximo o suficiente
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}
