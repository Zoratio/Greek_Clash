
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    private void OnCollisionEnter(Collision Collision)  //CHANGE THIS TO ONTRIGGERENTER WHEN ITS SET UP FOR WEAPONS THAT WOULD GO THROUGH IT
    {
        Debug.Log("collision with player");
        switch (Collision.gameObject.tag)
        {
            case ("Player"): Debug.Log("collided"); currentHealth -= 10; break;  
            case ("damage20"): currentHealth -= 20; break;
            case ("damage30"): currentHealth -= 30; break;
        }
        //healthBar.SetHealth(currentHealth);
        if (currentHealth < 0)
        {
            Debug.Log("killed enemy");
            this.gameObject.SetActive(false);
        }
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //healthBar.SetHealth(currentHealth);
    }
}
