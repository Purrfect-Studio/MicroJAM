using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocarSFX : MonoBehaviour
{

    public GameObject audioPrefab; // Prefab com o AudioSource
    public AudioClip[] sfx;

     public void tocar()
    {
        int indiceAleatorio = Random.Range(0, sfx.Length);
        AudioClip clipSelecionado = sfx[indiceAleatorio];

        // Criar o objeto tempor�rio para reproduzir o som
        GameObject audioObj = Instantiate(audioPrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = audioObj.GetComponent<AudioSource>();

        // Configura o AudioSource com o clip selecionado
        audioSource.clip = clipSelecionado;
        audioSource.Play();

        // Destroi o objeto ap�s a reprodu��o do som
        Destroy(audioObj, clipSelecionado.length);
    }
}
