using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codetesting : MonoBehaviour
{

    float timeLeft;
    float speed = 2;
    bool up;
    public List<Vector3> positions = new List<Vector3>();


    void Start()
    {
        timeLeft = 5;   //change this to 30 when done testing
        up = false;
        positions.Add(new Vector3(0, 0, -27));
        positions.Add(new Vector3(-14, 0, -27));
        positions.Add(new Vector3(14, 0, -27));
        positions.Add(new Vector3(0, 0, -41));
        positions.Add(new Vector3(0, 0, -13));
    }

 
    void Update()
    {
        if (timeLeft <= 0)
        {
            if (!up)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Time.deltaTime * speed);
                if (transform.position.y == 0)
                { 
                    Move();
                    up = true;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 5, transform.position.z), Time.deltaTime * speed);
                if (transform.position.y == 5)
                {
                    Debug.Log(timeLeft);
                    timeLeft = 5;   //change this to 30 when done testing
                    up = false;
                }
            }
        }
        else
        {
            timeLeft -= 1 * Time.deltaTime;
            Debug.Log(timeLeft);
        } 
    }

    void Move()
    {
        transform.position = positions[Random.Range(0, positions.Count)];
    }
}
