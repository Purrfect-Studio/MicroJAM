using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Unity.Burst.BurstCompile(DisableDirectCall = true)]
public class Health : MonoBehaviour
{
    public float life; // Vida do inimigo
    public GameObject floatingDamagePrefab; // Prefab de dano flutuante

    // Método para aplicar dano
    public void TakeDamage(float damage)
    {
        print("entrou no takeDamage");
        life -= damage; // Subtrai o dano da vida

        // Exibe o dano flutuante
        ShowFloatingDamage(damage);

        // Checa se o inimigo morreu
        if (life <= 0)
        {
            Die();
        }
    }

    // Exibe o dano flutuante
    void ShowFloatingDamage(float damage)
    {
        print("entrou no ShowFloatingDamage");
        if (floatingDamagePrefab != null)
        {
            // Instancia o texto de dano flutuante perto do inimigo
            GameObject damageText = Instantiate(floatingDamagePrefab, transform.position, Quaternion.identity);

            // Configura o texto do dano e a cor
            damageText.GetComponent<FloatingDamage>().SetDamageText("-" + damage.ToString(), Color.red);
            // Colocar o texto em uma Sorting Layer e uma ordem mais alta
            damageText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
            damageText.GetComponent<MeshRenderer>().sortingOrder = 10;
        }
    }

    // Método de morte
    void Die()
    {
        // Código de morte do inimigo
        Destroy(gameObject);
    }
}
