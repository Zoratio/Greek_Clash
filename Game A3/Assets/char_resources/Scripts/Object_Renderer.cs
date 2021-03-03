using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Renderer : MonoBehaviour
{
    public int layer = 2001;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.renderQueue = layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
