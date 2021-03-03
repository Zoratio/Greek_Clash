using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodWater : MonoBehaviour
{
    public GameObject ocean;
    public Renderer rend;
    public Material materialBlue;
    public Material materialRed;

    public Slider posiedonHealth;

    bool redwater = false;

    private void Start()
    {
    }

    void Update()
    {
        if(posiedonHealth.value <= 50 && !redwater)
        {
            redwater = true;
            StartCoroutine(FadeToRed());
        }
    }

    IEnumerator FadeToRed()
    {
        ocean.tag = "damage5";
        float elapsedTime = 0;
        float waitTime = 3f;
        while (elapsedTime < waitTime)
        {
            rend.material.Lerp(materialBlue, materialRed, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
