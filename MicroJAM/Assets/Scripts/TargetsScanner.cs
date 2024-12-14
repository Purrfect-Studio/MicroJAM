using System.Collections.Generic;
using UnityEngine;

public class TargetsScanner : MonoBehaviour
{
    [Header("Configurações")]
    public string targetTag = "enemy"; // Tag para encontrar alvos
    public float scanRadius = 15f; // Raio de varredura

    [Header("Estado")]
    public GameObject closestTarget; // Alvo mais próximo
    public float closestDistance = 0; // Distância ao alvo mais próximo

    public void ScanArea()
    {
        closestTarget = null;
        closestDistance = float.MaxValue;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, scanRadius); // Varredura na área
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = collider.gameObject;
                }
            }
        }
    }

    public GameObject GetClosestTarget()
    {
        return closestTarget;
    }

    public float GetClosestDistance()
    {
        return closestDistance;
    }

    // Corrigido: Garantir que o método OnDrawGizmosSelected exista apenas uma vez
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
