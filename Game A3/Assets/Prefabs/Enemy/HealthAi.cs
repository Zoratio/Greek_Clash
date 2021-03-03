using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class HealthAi : MonoBehaviour
{

    Slider health;

    public GameObject enemy;

    public Animator Animator;

    float prevHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        health = this.GetComponent<Slider>();
    }

    private void Update()
    {

        if (health.value == 0)
        {
            Death();
        }
        else
        {
            if (health.value != prevHealth)
            {
                Animator.SetTrigger("Damage");
            }
        }
        prevHealth = health.value;
    }

    void Death()
    {
        if(health.value == 0)
        {
            Animator.SetTrigger("Death");
            Animator.SetBool("Dead", true);
            enemy.GetComponent<AICharacterControl>().enabled = false;
            enemy.GetComponent<AttackAI>().enabled = false;
        }
    }
}
