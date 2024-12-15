using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float speed; // Velocidade do projétil
    public float lifeTime; // Tempo de vida antes de ser destruído
    public TargetsScanner targetsScanner;
    private Vector2 direction;
    
    public GameObject closestTarget;

    void Start()
    {
       
        targetsScanner.ScanArea();
        closestTarget = targetsScanner.GetClosestTarget();

        if (closestTarget != null)
        {
            Vector2 targetPosition = closestTarget.transform.position;
            direction = (targetPosition - (Vector2)transform.position).normalized;
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move o projétil na direção fornecida
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
