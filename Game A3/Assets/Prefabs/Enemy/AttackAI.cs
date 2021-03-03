using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public Transform target;                                    // target to aim for
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, target.position);
        anim.SetFloat("Distance", distanceToPlayer);
    }
}
