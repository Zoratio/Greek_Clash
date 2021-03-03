using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Anim_Hair: MonoBehaviour
{
    Renderer rend;
    public float speed = 0.1f;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        //time based hair animation
        AnimateHair();
    }

    void FixedUpdate()
    {
        //framerate based hair animation
        //AnimateHair();
    }

    void AnimateHair()
    {
        float newOffsetX = (rend.materials[6].GetTextureOffset("_MainTex").x - (speed * Random.Range(0.0f, 0.07f)));
        float newOffsetY = (rend.materials[6].GetTextureOffset("_MainTex").y - (speed * Random.Range(0.0f, 0.07f)));

        rend.materials[6].SetTextureOffset("_MainTex", new Vector2(newOffsetX, newOffsetY));
    }
}
