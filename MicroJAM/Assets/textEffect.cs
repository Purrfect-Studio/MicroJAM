using System.Collections;
using UnityEngine;
using TMPro;

public class textEffect : MonoBehaviour
{
    [TextArea(3, 10)]
    public string fullText; // Texto completo a ser exibido
    public TMP_Text tmpText; // Componente TextMeshPro onde o efeito será aplicado
    public float typingSpeed = 0.05f; // Velocidade de digitação (em segundos)
    
    public AudioSource typingSound; // Componente AudioSource para o som de digitação
    public AudioClip[] typingSounds; // Array de clips de som (caso queira sons diferentes)

    private string currentText = ""; // Texto em exibição
    private int soundIndex = 0; // Para escolher o próximo som da lista

    private void Start()
    {
        StartCoroutine(PlayTypewriterEffect());
    }

    public IEnumerator PlayTypewriterEffect()
    {
        tmpText.text = ""; // Certifique-se de que o texto começa vazio

        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            tmpText.text = currentText; // Atualiza o texto no TMP

            // Verifica se o caractere não é um espaço e toca o som
            if (typingSound != null && fullText[i] != ' ')
            {
                // Toca um som, podendo variar entre os sons se você tiver múltiplos
                if (typingSounds.Length > 0)
                {
                    typingSound.PlayOneShot(typingSounds[soundIndex]);
                    soundIndex = (soundIndex + 1) % typingSounds.Length; // Cicla entre os sons
                }
                else
                {
                    typingSound.Play(); // Caso não tenha múltiplos sons, usa o som padrão
                }
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Método público para reiniciar o efeito
    public void RestartEffect()
    {
        StopAllCoroutines();
        tmpText.text = "";
        StartCoroutine(PlayTypewriterEffect());
    }
}
