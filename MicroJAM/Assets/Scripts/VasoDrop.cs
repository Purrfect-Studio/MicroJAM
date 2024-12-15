using UnityEngine;

public class VasoDrop : MonoBehaviour
{
    [Header("Arraste o SistemaDeDrop aqui")]
    public SistemaDeDrop sistemaDeDrop; // Agora você pode arrastar no Inspector
    public AudioSource audioSource; // Referência ao AudioSource

    void Start()
    {
        // Obtém o AudioSource anexado ao mesmo objeto
        
    }

    void OnDestroy()
    {
        // Verifica se o SistemaDeDrop foi atribuído
        if (sistemaDeDrop != null)
        {
            print("Entrei no on destroy");
            sistemaDeDrop.Dropar();
        }
        else
        {
            Debug.LogWarning("SistemaDeDrop não foi atribuído ao objeto: " + gameObject.name);
        }

        // Toca o áudio, se o AudioSource estiver presente
        if (audioSource != null)
        {
            // Ativa o AudioSource temporariamente se ele estiver desativado
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Toca o áudio quando o vaso quebra
            }
        }
    }
}
