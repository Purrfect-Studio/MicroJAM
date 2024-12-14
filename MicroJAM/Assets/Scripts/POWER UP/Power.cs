using UnityEngine;

public enum PowerUpRarity
{
    Comum,
    Raro,
    Epico
}

[System.Serializable]
public class Power
{
    public string nome;
    public Sprite sprite;
    public string descricao;
    public PowerUpRarity raridade;
}
