using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
