using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Health;

public class HealthBoss1 : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject floatingDamagePrefab; // Prefab de dano flutuante

    public Slider healthBar;

    // Evento que será chamado quando a vida mudar
    public delegate void LifeChanged(float newLife);

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        print("entrou no takeDamage");
        currentHealth -= damage; // Subtrai o dano da vida
        healthBar.value = currentHealth;

        // Exibe o dano flutuante
        ShowFloatingDamage(damage);

        // Checa se o inimigo morreu
        if (currentHealth <= 0)
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
