using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public Power[] poderesDisponiveis; // Array de power-ups disponíveis
    public PowerUpDisplay powerUpDisplay; // Referência ao PowerUpDisplay para mostrar os power-ups no Canvas

    public void GanharPoder()
    {
        // Sorteia dois power-ups aleatórios
        int indice1 = Random.Range(0, poderesDisponiveis.Length);
        int indice2 = Random.Range(0, poderesDisponiveis.Length);

        Power poder1 = poderesDisponiveis[indice1];
        Power poder2 = poderesDisponiveis[indice2];

        // Envia os power-ups para o PowerUpDisplay
        powerUpDisplay.ApresentarPoderes(poder1, poder2);
    }
}
