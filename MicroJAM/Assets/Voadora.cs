using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voadora : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidade = 100f; // Velocidade de movimento
    public float tempo = 5f; // Tempo até o objeto se autodestruir

    private GameObject alvo; // Referência ao alvo encontrado
    private Vector2 direcao; // Direção para seguir o alvo

    private TargetsScanner scanner; // Scanner para encontrar inimigos

    void Start()
    {
        //Physics2D.IgnoreLayerCollision(3, 9, true);


        // Obtém o componente TargetsScanner para varrer a área
        scanner = GetComponent<TargetsScanner>();

        if (scanner != null)
        {
            // Escaneia a área para encontrar o inimigo mais próximo
            scanner.ScanArea();
            alvo = scanner.GetClosestTarget();

            if (alvo != null)
            {
                // Calcula a direção do movimento em linha reta para o alvo
                direcao = (alvo.transform.position - transform.position).normalized;
            }
            else
            {
                Debug.LogWarning("Nenhum alvo encontrado! O objeto não irá se mover.");
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("TargetsScanner não encontrado no objeto!");
        }

        // Inicia a destruição do objeto após 'tempo' segundos
        Destroy(gameObject, tempo);
    }

    void Update()
    {
        // Move o objeto em direção ao alvo se ele existir
        if (alvo != null)
        {
            transform.position += (Vector3)direcao * velocidade * Time.deltaTime;
        }
    }
}
