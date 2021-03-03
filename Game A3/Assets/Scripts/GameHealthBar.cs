using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHealthBar : MonoBehaviour
{
	public Transform cam;

    void LateUpdate()
    {
		transform.LookAt(transform.position + cam.forward);
    }
}
