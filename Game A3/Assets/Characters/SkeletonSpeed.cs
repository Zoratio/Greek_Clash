using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpeed : MonoBehaviour
{

    public Rigidbody skelBody;
    public Animator anim;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Debug.Log(skelBody.velocity.magnitude);
        anim.SetFloat("speed", skelBody.velocity.magnitude);
    }
}
