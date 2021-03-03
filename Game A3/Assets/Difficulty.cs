using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public int difficulty;
    // Start is called before the first frame update
    public void Click()
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }
}
