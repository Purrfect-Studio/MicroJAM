using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public int vida = 10;
    public int vidaMaxima = 10;
    public Slider sliderVida;

    void Start()
    {
        sliderVida.maxValue = vidaMaxima;
        sliderVida.value = vida;
    }

    void Update()
    {
        morrer();
        HealthVogic();
    }

    void HealthVogic()
    {
        sliderVida.value = vida;
        vida = Mathf.Clamp(vida, 0, vidaMaxima);
    }

    void morrer()
    {
        if (vida <= 0)
        {
            Debug.Log("morreu");
            //ReloadScene();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
