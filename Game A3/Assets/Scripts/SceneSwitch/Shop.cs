using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetString("ShopExit", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Shop");
        }
    }
}
