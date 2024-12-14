using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpRarity
{
    Comum,
    Raro,
    Epico
}

[System.Serializable]
public class Power
{
    public String nome;
    public Sprite sprite;
    public String descricao;
    public PowerUpRarity raridade;
}
