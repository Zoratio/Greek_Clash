using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaCol : MonoBehaviour
{
    /*public string stage;

    void Start ()
    {
        stage = "Level1P1"; 
    }
  
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(stage);
    }*/

    public GameObject checkpoint;
    public GameObject character;

    private void OnTriggerEnter(Collider other)
    {
        character.transform.position = checkpoint.transform.position;
    }
}
