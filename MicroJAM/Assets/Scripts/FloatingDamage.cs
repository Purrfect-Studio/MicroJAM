using UnityEngine;
using TMPro; // Ou UnityEngine.UI se for um texto de UI

public class FloatingDamage : MonoBehaviour
{
    public TextMeshPro damageText; // Referência ao componente de texto
    public float destroyTime = 1f; // Tempo até o dano sumir
    public float speed = 1f;

    public void SetDamageText(string damage, Color color)
    {
        damageText.text = damage; // Define o texto do dano
        damageText.color = color; // Define a cor do texto
        Destroy(gameObject, destroyTime); // Destroi o dano flutuante após X segundos
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed; // Faz o texto subir levemente
    }
}
