using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin_RenderOrder : MonoBehaviour
{
    public int topLayers = 2001;
    public int skinLayer = 2000;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        foreach(Material m in rend.materials)
        {
            if (m.name.Contains("skin"))
            {
                m.renderQueue = skinLayer;
            }
            else
            {
                m.renderQueue = topLayers;
            } 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
