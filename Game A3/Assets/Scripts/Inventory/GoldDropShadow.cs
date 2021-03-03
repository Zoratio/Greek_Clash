using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldDropShadow : MonoBehaviour
{
    public Text gold;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = gold.text;
    }
}
