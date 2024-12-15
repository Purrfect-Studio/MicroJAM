using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Slider sliderVida; // Referência automática ao Slider da barra de vida
    public GameObject sliderObject;
    public Vector3 offset = new Vector3(0, 2f, 0); // Ajuste da posição acima do jogador

    private Camera mainCamera; // Referência à câmera principal
    public CurrentPlayerStats currentPlayerStats;
    public GameObject canvasGameOver;
    private bool estaMorto;

    void Start()
    {
        estaMorto = false;
        maxHealth = currentPlayerStats.maxHealth;
        currentHealth = maxHealth;
        // Procura o Slider na cena pela tag "HealthBar"
        sliderObject = GameObject.FindGameObjectWithTag("HealthBar");

        if (sliderObject != null)
        {
            sliderVida = sliderObject.GetComponent<Slider>();
            sliderVida.maxValue = maxHealth;
            sliderVida.value = currentHealth;
        }
        else
        {
            Debug.LogError("Slider com a tag 'HealthBar' não foi encontrado na cena. Certifique-se de que o GameObject está com a tag 'HealthBar'.");
        }

        // Captura a câmera principal para referência
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (sliderVida != null)
        {
            HealthLogic();
            CheckDeath();
            if(Time.timeScale != 0)
            {
                Envenenado();
            }
        }
    }

    void Envenenado()
    {
        currentHealth -= Time.deltaTime;
    }

    void HealthLogic()
    {
        sliderVida.value = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void CheckDeath()
    {
        if (currentHealth <= 0 && !estaMorto)
        {
            canvasGameOver.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Player morreu");
            estaMorto = true;
            gameObject.SetActive(false);
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Jogador tomou dano: " + damage);
    }

    public void heal(float healAmount)
    {
        currentHealth += healAmount;
        Debug.Log("Jogador curou: " + healAmount);
    }
}
