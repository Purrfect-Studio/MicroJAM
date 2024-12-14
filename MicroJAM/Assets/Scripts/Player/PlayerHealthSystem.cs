using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public float currentHealth = 10;
    public float maxHealth = 10;
    public Slider sliderVida;

    void Start()
    {
        sliderVida.maxValue = maxHealth;
        sliderVida.value = currentHealth;
    }

    void Update()
    {
        die();
        HealthVogic();
    }

    void HealthVogic()
    {
        sliderVida.value = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void die()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("morreu");
            //ReloadScene();
        }
    }

    public void takeDamage(float damage)
    {
        Debug.Log("jogador tomou dano");
        if(currentHealth - damage < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }
    }

    public void heal(float heal)
    {
        if(currentHealth + heal > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }
    }


    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
