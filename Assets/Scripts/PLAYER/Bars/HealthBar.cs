using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //COMPONENTS
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    //SETTING VALUES
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        //NORMALIZED BECAUSE GRADIENT GOES FROM 0-1;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ShowHealthGone(float health)
    {
        slider.value -= health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
