using System.Collections.Generic;
using UnityEngine;

public class PoderesPlayer : MonoBehaviour
{
    public List<Power> poderesDisponiveis = new List<Power>(); // Lista de poderes do jogador

    public void AdicionarPoder(Power novoPoder)
    {
        if (novoPoder != null && !poderesDisponiveis.Contains(novoPoder))
        {
            poderesDisponiveis.Add(novoPoder); // Adiciona o poder à lista
            Debug.Log($"Poder {novoPoder.nome} foi adicionado ao jogador!");
        }
    }
}
