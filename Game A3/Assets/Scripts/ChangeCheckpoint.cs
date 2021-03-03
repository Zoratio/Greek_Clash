using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCheckpoint : MonoBehaviour
{
    public GameObject newCheckpoint;
    public LavaCol lavaScript;
    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            lavaScript.checkpoint = newCheckpoint;
            Debug.Log("test");
        }
        
    }
}
