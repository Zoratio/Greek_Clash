using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetainStatValues : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat("CurrentHP") != 0f) { 
            this.transform.GetChild(0).GetComponent<Slider>().value = PlayerPrefs.GetFloat("CurrentHP");
            this.transform.GetChild(1).GetComponent<Slider>().value = PlayerPrefs.GetFloat("CurrentArmour");
            this.transform.GetChild(2).GetComponent<Slider>().value = PlayerPrefs.GetFloat("CurrentMana");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("CurrentHP", this.transform.GetChild(0).GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("CurrentArmour", this.transform.GetChild(1).GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("CurrentMana", this.transform.GetChild(2).GetComponent<Slider>().value);
    }
}
