using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = 100;
        slider.value = health;
    }
    public void SetHealth(float Life_Player)
    {
        slider.value = Life_Player;
    }

}
