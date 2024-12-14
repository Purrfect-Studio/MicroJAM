using UnityEngine;

public class HealthListener : MonoBehaviour
{
    public Health playerHealth; // Referência ao componente Health do jogador
    public UnityEngine.UI.Text lifeText; // Referência ao texto UI para exibir a vida no Canvas

    void Start()
    {
        if (playerHealth != null)
        {
            // Inscrever-se para ouvir mudanças na vida do jogador
            playerHealth.onLifeChanged += UpdateLifeUI;
        }
    }

    // Método que será chamado quando a vida do jogador mudar
    void UpdateLifeUI(float newLife)
    {
        if (lifeText != null)
        {
            // Atualiza o texto no Canvas com a nova vida do jogador
            lifeText.text = "Vida: " + newLife.ToString("F0");
        }
    }

    void OnDestroy()
    {
        // Desinscrever-se do evento quando o objeto for destruído
        if (playerHealth != null)
        {
            playerHealth.onLifeChanged -= UpdateLifeUI;
        }
    }
}
