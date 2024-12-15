using UnityEngine;

public class ItensManager : MonoBehaviour
{
    public Item[] itens; // Array de power-ups disponíveis
    public ItensDisplay itensDisplay; // Referência ao PowerUpDisplay para mostrar os power-ups no Canvas

    public void GanharItem()
    {
        int indice1 = Random.Range(0, itens.Length);
        int indice2 = Random.Range(0, itens.Length);
        int indice3 = Random.Range(0, itens.Length);


        Item item1 = itens[indice1];
        Item item2 = itens[indice2];
        Item item3 = itens[indice3];

        // Envia os power-ups para o PowerUpDisplay
        itensDisplay.ApresentarItens(item1, item2, item3);
        Debug.Log("Itens enviados: "+ item1.nome +"   " + item2.nome + "  " + item3.nome );
    }


    void Update()
    {
        // Exemplos de teste para ativar os poderes
       
        if (Input.GetKeyDown(KeyCode.M)) GanharItem();
    }

}
