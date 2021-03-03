using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2UpDown : MonoBehaviour
{

    public float speed = 2.5f;
    float initY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, initY + Mathf.PingPong(Time.time * speed, 5), transform.position.z);
    }
}
