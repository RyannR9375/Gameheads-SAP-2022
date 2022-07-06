using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbBar : MonoBehaviour
{
    //COMPONENTS
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    //SETTING VALUES
    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetValue(float value)
    {
        slider.value = value;

        //NORMALIZED BECAUSE GRADIENT GOES FROM 0-1;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
