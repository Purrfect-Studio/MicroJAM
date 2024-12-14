using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public Slider xpBar;
    public float currentXP = 0;
    public float maxXP = 10;

    void Start()
    {
        xpBar.maxValue = maxXP;
        xpBar.value = currentXP;
    }

    void Update()
    {
        updateXpBar();
    }

    void updateXpBar()
    {
        if(currentXP >= maxXP)
        {
            currentXP -= maxXP;
            maxXP *= 1.2f;
            xpBar.maxValue = maxXP;
            xpBar.value = currentXP;
        }
    }

    public void gainXp(float xpGain)
    {
        currentXP += xpGain;
        xpBar.value = currentXP;
    }
}
