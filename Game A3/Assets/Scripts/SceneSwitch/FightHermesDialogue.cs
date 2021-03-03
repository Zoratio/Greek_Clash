﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightHermesDialogue : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") { 
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("FightHermesDialogue");
        }
    }
}
