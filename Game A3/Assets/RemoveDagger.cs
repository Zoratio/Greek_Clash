using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDagger : MonoBehaviour
{
    public MeshRenderer dagger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dagger.enabled)
        {
            this.gameObject.SetActive(false);
        }
    }
}
