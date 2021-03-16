using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradientColour;
    public Image fill;

    public void SetHealt(int health)
    {
        slider.value = health;
        fill.color = gradientColour.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHelth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradientColour.Evaluate(1f);


    }
}
