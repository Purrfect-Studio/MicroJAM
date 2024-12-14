using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool jogoAtivo = true;

    void Start()
    {
        // Aqui podemos colocar a lógica de inicialização do jogo, se necessário
    }

    void Update()
    {
        if (!jogoAtivo)
        {
            PararTudoNoJogo();
        }
    }

    public void PararTudoNoJogo()
    {
        // Aqui podemos parar o jogo, desabilitar inputs, animações, etc.
        Time.timeScale = 0; // Isso pausa o jogo
        // Outras ações para parar o jogo (se necessário)
    }
}
