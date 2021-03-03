using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2BeforeTower : MonoBehaviour
{
    public void Done()
    {
        SceneManager.LoadScene("Level2BeforeTower");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Level2BeforeTower");
        }
    }
}
