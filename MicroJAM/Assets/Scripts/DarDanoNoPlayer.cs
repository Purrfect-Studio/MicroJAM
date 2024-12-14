using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarDanoNoPlayer : MonoBehaviour
{
    public PlayerHealthSystem playerHealthSystem;
    public PlayerController playerController;
    public int damage;
    //public Animator animator;
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player") && !playerController.isInvulnerable) //&& !playerController.isInvulnerable
        {
            playerController.KBCount = playerController.KBTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.isKnockRight = true;
            } else
            {
                playerController.isKnockRight = false;
            }


            playerHealthSystem.vida -= damage;

            //animator.SetTrigger("attack");

            //StartCoroutine(playerController.InvulnerabilityCoroutine());
        }
    }
}
