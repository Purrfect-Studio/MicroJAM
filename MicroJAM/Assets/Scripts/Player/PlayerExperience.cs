using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public Slider xpBar;
    public TextMeshProUGUI levelText;
    public float currentXP = 0;
    public float maxXP = 10;
    private int currentLevel = 0;
    public PowerUpManager powerUpManager;


    void Start()
    {

        powerUpManager = FindObjectOfType<PowerUpManager>();

        xpBar.maxValue = maxXP;
        xpBar.value = currentXP;
        levelText.text = "Level: " + currentLevel.ToString();
    }

    void Update()
    {
        levelUp();
    }

    void levelUp()
    {
        if(currentXP >= maxXP)
        {
            currentXP -= maxXP;
            maxXP *= 1.2f;
            xpBar.maxValue = maxXP;
            xpBar.value = currentXP;
            currentLevel += 1;
            levelText.text = "Level: " + currentLevel.ToString();
            Time.timeScale = 0;
            powerUpManager.GanharPoder();

        }
    }

    public void gainXp(float xpGain)
    {
        currentXP += xpGain;
        xpBar.value = currentXP;
    }

    public void despausar()
    {
        Time.timeScale = 1;
    }
}
