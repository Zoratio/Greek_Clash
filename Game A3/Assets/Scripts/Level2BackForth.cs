using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BackForth : MonoBehaviour
{

    public float speed = 2.5f;
    float initX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        initX = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(initX + Mathf.PingPong(Time.time * speed, 10), transform.position.y, transform.position.z);
    }
}

