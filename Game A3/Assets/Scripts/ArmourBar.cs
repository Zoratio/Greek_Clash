using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArmourBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetMaxArmour(int armour)
    {
        slider.maxValue = armour;
        slider.value = armour;

    }

    public void SetArmour(int armour)
    {
        slider.value = armour;

    }
}
