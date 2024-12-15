using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensAtivados : MonoBehaviour
{

    public ItensLoja itensLoja;
    public GameObject torreta;
    public GameObject pulsefire;

    private GameObject player;
    private PlayerHealthSystem playerHealthSystem;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();

    }
   
   public void ligarTorreta()
   {

        if (player != null && torreta != null)
        {
            // Spawna a torreta na posição do jogador e sem rotação

            playerHealthSystem.maxHealth -=15;
            playerHealthSystem.currentHealth-=15;
            Vector3 spawnPosition = player.transform.position;
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(torreta, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogWarning("Player ou Torreta não está configurado!");
        }
   }

          public void ligarPulseFire()
{
    Debug.Log("Ligando Pulsefire");

    if (pulsefire != null && playerHealthSystem != null)
    {
        // Reduz os valores de vida
        playerHealthSystem.maxHealth -= 10;
        playerHealthSystem.currentHealth -= 15;

        // Instancia o PulseFire na posição do objeto atual (transform) com rotação padrão
        GameObject instancia = Instantiate(pulsefire, player.transform.position, Quaternion.identity);

        // Define o objeto instanciado como filho deste objeto
        instancia.transform.SetParent(player.transform);

        Debug.Log("Pulsefire foi instanciado e configurado como filho.");
    }
    else
    {
        Debug.LogWarning("Pulsefire ou PlayerHealthSystem não está configurado!");
    }
}

   }
