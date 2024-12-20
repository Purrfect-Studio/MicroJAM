using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItensDisplay : MonoBehaviour
{
    public GameObject canvas; // Canvas do display
    public Image imagemItem1;
    public Image imagemItem2;
    public Image imagemItem3;

    public TMP_Text nomeItem1; // TextMeshPro para o nome do Item 1
    public TMP_Text nomeItem2; // TextMeshPro para o nome do Item 2

    public TMP_Text nomeItem3; // TextMeshPro para o nome do Item 2

    public TMP_Text descricaoItem1; // TextMeshPro para a descrição do Item 1
    public TMP_Text descricaoItem2; // TextMeshPro para a descrição do Item 2

    public TMP_Text descricaoItem3; // TextMeshPro para a descrição do Item 2

    public TMP_Text custoItem1; // TextMeshPro para a raridade do poder 1
    public TMP_Text custoItem2; // TextMeshPro para a raridade do poder 2
    public TMP_Text custoItem3; // TextMeshPro para a raridade do poder 2
    public TMP_Text VidaMaxima; // TextMeshPro para a raridade do poder 1


    private Item itemEscolhido1; // Referência para o primeiro poder sorteado
    private Item itemEscolhido2; // Referência para o segundo poder sorteado
    private Item itemEscolhido3; // Referência para o segundo poder sorteado
    public ItensLoja itensLoja;
    public ItensAtivados itensAtivados;
    private PlayerExperience playerExperience;

    private PlayerHealthSystem playerHealthSystem;

    public GameObject painel1;
        public GameObject painel2;
    public GameObject painel3;




    private void Awake()
    {
        itensAtivados = FindObjectOfType<ItensAtivados>();
        playerExperience = FindObjectOfType<PlayerExperience>();
        playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
    }

    public void atualizarVida()
    {

        VidaMaxima.text = playerHealthSystem.maxHealth.ToString() + " Max Health";

    }
    public void ApresentarItens(Item item1, Item item2, Item item3)
    {

        painel1.SetActive(true);
        painel2.SetActive(true);
        painel3.SetActive(true);


        canvas.SetActive(true);
        atualizarVida();
        playerExperience.pausar();




        // Armazena os poderes sorteados
        itemEscolhido1 = item1;
        itemEscolhido2 = item2;
        itemEscolhido3 = item3;

        // Exibe as informações do primeiro poder
        imagemItem1.sprite = item1.sprite;
        nomeItem1.text = item1.nome;
        descricaoItem1.text = item1.descricao;
        custoItem1.text = item1.preco.ToString();

        // Exibe as informações do segundo poder
        imagemItem2.sprite = item2.sprite;
        nomeItem2.text = item2.nome;
        descricaoItem2.text = item2.descricao;
        custoItem2.text = item2.preco.ToString();

        imagemItem3.sprite = item3.sprite;
        nomeItem3.text = item3.nome;
        descricaoItem3.text = item3.descricao;
        custoItem3.text = item3.preco.ToString();


    }

    public void EscolherItem1()
    {
                
        LigarItem(nomeItem1.text.ToString());
        atualizarVida();
        painel1.SetActive(false);

    }

    public void EscolherPoder2()
    {
       LigarItem(nomeItem2.text.ToString());
        atualizarVida();
        painel2.SetActive(false);

    }

     public void EscolherPoder3()
    {
       LigarItem(nomeItem3.text.ToString());
        atualizarVida();
        painel3.SetActive(false);

    }

    public void Pular()
    {
        playerExperience.despausar();
        FecharCanvas();
        
    }

    public void LigarItem(String nomeItem)
    {
        switch (nomeItem)
        {
            case ("Pulse Fire"):
            itensLoja.possuiDash = true;
            itensAtivados.ligarPulseFire();
            break;


            case("Turret"):
            itensLoja.possuiTorreta = true;
            Debug.Log("Ligando torreta");
            itensAtivados.ligarTorreta();
            break;

            case("Bat"):
            Debug.Log("Ligando morcego");
            itensLoja.possuiMorcego = true;
            itensAtivados.ligarMorcego();
            break;

            
            default:
            Debug.Log("Tem esse item ai nao meo: " + nomeItem);

            break;
        }
    }

    private void FecharCanvas()
    {
        canvas.SetActive(false); // Fecha o Canvas
    }
}
