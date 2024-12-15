using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpDisplay : MonoBehaviour
{
    public GameObject canvas; // Canvas do display
    public Image imagemPoder1;
    public Image imagemPoder2;
    public TMP_Text nomePoder1; // TextMeshPro para o nome do poder 1
    public TMP_Text nomePoder2; // TextMeshPro para o nome do poder 2
    public TMP_Text descricaoPoder1; // TextMeshPro para a descrição do poder 1
    public TMP_Text descricaoPoder2; // TextMeshPro para a descrição do poder 2
    public TMP_Text raridadePoder1; // TextMeshPro para a raridade do poder 1
    public TMP_Text raridadePoder2; // TextMeshPro para a raridade do poder 2

    private Power poderEscolhido1; // Referência para o primeiro poder sorteado
    private Power poderEscolhido2; // Referência para o segundo poder sorteado

    public PoderesPlayer poderesPlayer; // Referência ao script PoderesPlayer

    private void Awake()
    {
        // Busca automaticamente o componente PoderesPlayer no jogador
        poderesPlayer = FindObjectOfType<PoderesPlayer>();
    }

    public void ApresentarPoderes(Power poder1, Power poder2)
    {
        canvas.SetActive(true);

        // Armazena os poderes sorteados
        poderEscolhido1 = poder1;
        poderEscolhido2 = poder2;

        // Exibe as informações do primeiro poder
        imagemPoder1.sprite = poder1.sprite;
        nomePoder1.text = poder1.nome;
        descricaoPoder1.text = poder1.descricao;
        raridadePoder1.text = poder1.raridade.ToString();

        // Exibe as informações do segundo poder
        imagemPoder2.sprite = poder2.sprite;
        nomePoder2.text = poder2.nome;
        descricaoPoder2.text = poder2.descricao;
        raridadePoder2.text = poder2.raridade.ToString();
    }

    public void EscolherPoder1()
    {
        if (poderesPlayer != null)
        {
            // Adiciona o primeiro poder aos poderes do jogador
            poderesPlayer.AdicionarPoder(poderEscolhido1.nome);
        }

        FecharCanvas();
    }

    public void EscolherPoder2()
    {
        if (poderesPlayer != null)
        {
            // Adiciona o segundo poder aos poderes do jogador
            poderesPlayer.AdicionarPoder(poderEscolhido2.nome);
        }

        FecharCanvas();
    }

    private void FecharCanvas()
    {
        canvas.SetActive(false); // Fecha o Canvas
    }
}
