using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public bool explosionActive;
    public bool wingsActive;

    GameObject explosion;
    GameObject wings;
    // Start is called before the first frame update
    void Start()
    {
        wings = GameObject.Find("FireWing");
        explosion = GameObject.Find("explosion");
    }

    // Update is called once per frame
    void Update()
    {
        wings.SetActive(wingsActive);
        explosion.SetActive(explosionActive);
    }
}
