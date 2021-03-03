using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStick : MonoBehaviour
{
    public GameObject Player;
    public Transform move;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.gameObject.name);
        if (collision.gameObject == Player)
        {
            Debug.Log("stick");
            Player.transform.parent = move;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("not stick");
        if (collision.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }


}
