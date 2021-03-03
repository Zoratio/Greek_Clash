using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1BeforeBossFight : MonoBehaviour
{
    public void Done()
    {
        SceneManager.LoadScene("Level1BeforeBossFight");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Level1BeforeBossFight");
        }
    }
}
