using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<Slider>().value += 15f*Time.deltaTime;
    }
}
