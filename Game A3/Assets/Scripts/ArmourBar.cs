using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArmourBar : MonoBehaviour
{
    public Slider slider;
    //public Gradient gradient;
    public Image fill;

    //could change this whenever they level up, increase armour etc?
    public void SetMaxArmour(int armour)
    {
        slider.maxValue = armour;
        slider.value = armour;

        //fill.color = gradient.Evaluate(1f);
    }

    public void SetArmour(int armour)
    {
        slider.value = armour;

        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
