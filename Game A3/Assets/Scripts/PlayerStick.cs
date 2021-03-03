using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
            PlatformStick[] scripts = GetComponentsInChildren<PlatformStick>();
            foreach (PlatformStick s in scripts)
            {
                s.Player = player;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
