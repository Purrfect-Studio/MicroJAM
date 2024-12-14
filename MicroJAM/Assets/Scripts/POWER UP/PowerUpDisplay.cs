using UnityEngine;
using UnityEngine.UI;

public class PowerUpDisplay : MonoBehaviour
{
    public Image imagemPoder1;
    public Image imagemPoder2;
    public Text nomePoder1;
    public Text nomePoder2;
    public Text descricaoPoder1;
    public Text descricaoPoder2;
    public Text raridadePoder1;
    public Text raridadePoder2;

    public void ApresentarPoderes(Power poder1, Power poder2)
    {
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
}
