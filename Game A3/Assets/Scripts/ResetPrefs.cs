using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
