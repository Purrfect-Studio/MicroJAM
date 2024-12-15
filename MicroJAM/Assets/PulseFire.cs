using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseFire : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject prefab;       // Prefab a ser instanciado
    public float cooldown = 1f;     // Tempo entre instâncias
    public bool filho = true;

    private float tempoRestante;    // Contador para o cooldown

    void Start()
    {
        tempoRestante = cooldown;   // Inicializa o tempoRestante com o valor do cooldown
    }

    void Update()
    {
        // Reduz o tempo restante baseado no tempo decorrido
        tempoRestante -= Time.deltaTime;

        // Quando o tempo restante chegar a 0 ou menos, instancia o prefab
        if (tempoRestante <= 0f)
        {
            InstanciarPrefab();
            tempoRestante = cooldown; // Reinicia o cooldown
        }
    }

    void InstanciarPrefab()
    {
        if (prefab != null)
        {
           GameObject instancia = Instantiate(prefab, transform.position, transform.rotation);

            // Define o objeto instanciado como filho do objeto atual
            if (filho)
            {
                instancia.transform.SetParent(transform);

            }
        }
        else
        {
            Debug.LogError("Prefab não atribuído no PulseFire!");
        }
    }
}
