using UnityEngine;
using System.Collections.Generic;

public class PoderesPlayer : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerHealthSystem playerHealthSystem;
    private Atirar atirar;
    public CurrentPlayerStats currentPlayerStats;

    public List<string> poderesAtivos = new List<string>(); // Lista para armazenar os poderes ativos

    void Start()
    {
        // Encontrando os scripts necessários
        playerController = FindObjectOfType<PlayerController>();
        playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        atirar = FindObjectOfType<Atirar>();

        if (playerController == null || playerHealthSystem == null || atirar == null)
        {
            Debug.LogError("Não foi possível encontrar os scripts necessários.");
        }
    }

    // Método para adicionar poderes
    public void AdicionarPoder(string poder)
    {
        if (!poderesAtivos.Contains(poder)) // Evitar duplicatas
        {
            poderesAtivos.Add(poder); // Adiciona à lista de poderes ativos
            Debug.Log($"Poder {poder} ativado!");
        

        switch (poder)
        {
            case "Velocidade":
                playerController.playerSpeed += 3f; // Aumenta a velocidade do jogador
                currentPlayerStats.playerSpeed = playerController.playerSpeed;
                break;

            case "Saude":
                playerHealthSystem.maxHealth += 15f; // Aumenta a saúde máxima
                playerHealthSystem.currentHealth += 5f; // Cura o jogador em relação ao aumento
                currentPlayerStats.maxHealth = playerHealthSystem.maxHealth;
                break;

            case "Munição":
                atirar.maxAmmo += 5; // Aumenta a capacidade máxima de munição
                atirar.currentAmmo += 5; // Recarrega a munição atual
                currentPlayerStats.maxAmmo = atirar.maxAmmo;
                break;

            case "FogoRapido":
                atirar.fireRate /= 1.5f; // Reduz o tempo entre disparos
                currentPlayerStats.fireRate = atirar.fireRate;
                break;

            default:
                Debug.LogWarning("Poder não reconhecido: " + poder);
                break;
        }

        MostrarPoderesAtivos();
        }
    }

    // Método para remover poderes
    public void RemoverPoder(string poder)
    {
        if (poderesAtivos.Contains(poder)) // Verifica se o poder está ativo
        {
            poderesAtivos.Remove(poder); // Remove da lista
            Debug.Log($"Poder {poder} removido!");
        

        switch (poder)
        {
            case "Velocidade":
                playerController.playerSpeed -= 3f; // Aumenta a velocidade do jogador
            break;

            case "Saude":
                playerHealthSystem.maxHealth -= 15f; // Aumenta a saúde máxima
                playerHealthSystem.currentHealth -= 5f; // Cura o jogador em relação ao aumento
            break;

            case "Munição":
                atirar.maxAmmo -= 5; // Aumenta a capacidade máxima de munição
                atirar.currentAmmo -= 5; // Recarrega a munição atual
                break;

            case "FogoRapido":
                atirar.fireRate *= 1.5f; // Reduz o tempo entre disparos
                break;

            default:
                Debug.LogWarning("Poder não reconhecido: " + poder);
                break;
        }

        MostrarPoderesAtivos();
        }
    }

    void MostrarPoderesAtivos()
    {
        string poderes = poderesAtivos.Count > 0 
            ? string.Join(", ", poderesAtivos) 
            : "Nenhum poder ativo";
        Debug.Log("Poderes ativos: " + poderes);
    }


    void Update()
    {
        // Exemplos de teste para ativar os poderes
        /*if (Input.GetKeyDown(KeyCode.Alpha1)) AdicionarPoder("Velocidade");
        if (Input.GetKeyDown(KeyCode.Alpha2)) AdicionarPoder("Saude");
        if (Input.GetKeyDown(KeyCode.Alpha3)) AdicionarPoder("Munição");
        if (Input.GetKeyDown(KeyCode.Alpha4)) AdicionarPoder("FogoRapido");

        // Exemplos de teste para remover os poderes
        if (Input.GetKeyDown(KeyCode.P)) RemoverPoder("Velocidade");
        if (Input.GetKeyDown(KeyCode.L)) RemoverPoder("Saude");
        if (Input.GetKeyDown(KeyCode.K)) RemoverPoder("Munição");
        if (Input.GetKeyDown(KeyCode.M)) RemoverPoder("FogoRapido");*/
    }

}
