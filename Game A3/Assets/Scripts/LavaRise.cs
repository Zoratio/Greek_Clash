using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRise : MonoBehaviour
{
    float speed = 0.5f;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
