using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps")]
public class PowerUps : ScriptableObject
{
    // Enum para determinar a raridade dos itens
    public enum Raridade
    {
        Comum,
        Raro,
        Lendário
    }

    [Header("Bota de agilidade")]
    public Sprite BotaSprite; // Sprite para a bota
    public string nomeBota; // Nome do item
    public string descricaoBota; // Descrição do item
    public Raridade raridadeBota; // Raridade do item

    [Header("Escudo de Proteção")]
    public Sprite escudoSprite;
    public string nomeEscudo;
    public string descricaoEscudo;
    public Raridade raridadeEscudo;

    [Header("Espada de Fogo")]
    public Sprite espadaSprite;
    public string nomeEspada;
    public string descricaoEspada;
    public Raridade raridadeEspada;

    [Header("Armadura Mística")]
    public Sprite armaduraSprite;
    public string nomeArmadura;
    public string descricaoArmadura;
    public Raridade raridadeArmadura;


    [Header("Anel do Poder")]
    public Sprite anelSprite;
    public string nomeAnel;
    public string descricaoAnel;
    public Raridade raridadeAnel;

    [Header("Arco Relâmpago")]
    public Sprite arcoSprite;
    public string nomeArco;
    public string descricaoArco;
    public Raridade raridadeArco;

    [Header("Cajado Arcano")]
    public Sprite cajadoSprite;
    public string nomeCajado;
    public string descricaoCajado;
    public Raridade raridadeCajado;

    [Header("Máscara das Sombras")]
    public Sprite mascaraSprite;
    public string nomeMascara;
    public string descricaoMascara;
    public Raridade raridadeMascara;

    [Header("Botas de Velocidade")]
    public Sprite botasVelocidadeSprite;
    public string nomeBotasVelocidade;
    public string descricaoBotasVelocidade;
    public Raridade raridadeBotasVelocidade;

    [Header("Itens Personalizados")]
    public Sprite itemExtraSprite;
    public string nomeItemExtra;
    public string descricaoItemExtra;
    public Raridade raridadeItemExtra;

    // Adicione mais itens aqui, se necessário
}
