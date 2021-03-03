using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public Camera thirdPersonCam;
    public Camera firstPersonCam;

    GameObject character;
    bool thirdPerson = true;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Low Poly Warrior");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SwitchCam"))
        {
            thirdPerson = !thirdPerson;
        }

        if (thirdPerson)
        {
            thirdPersonCam.gameObject.SetActive(true);
            character.GetComponent<Movement2>().mainCam = thirdPersonCam;
            firstPersonCam.gameObject.SetActive(false);
        }
        else
        {
            firstPersonCam.gameObject.SetActive(true);
            character.GetComponent<Movement2>().mainCam = firstPersonCam;
            thirdPersonCam.gameObject.SetActive(false);
        }
    }
}
