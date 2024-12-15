using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensAtivados : MonoBehaviour
{

    public ItensLoja itensLoja;
    public GameObject torreta;
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
}
