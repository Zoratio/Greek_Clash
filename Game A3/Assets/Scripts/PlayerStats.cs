using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxArmour = 100;
    public int currentArmour;
    public int maxMana = 100;
    public float currentMana;

    public HealthBar healthBar;
    public ArmourBar armourBar;
    public ManaBar manaBar;

    /*void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentArmour = maxArmour;
        armourBar.SetMaxArmour(maxArmour);
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }*/


    /*void Update()
    {
        if (currentMana < maxMana)
        {
            currentMana += 1 * Time.deltaTime;
            Debug.Log(1 * Time.deltaTime);
            manaBar.SetMana(currentMana);
        }
        if (Input.GetKeyDown(KeyCode.S))    //normal damage example
        {
            TakeDamage(30);
        }
        if (Input.GetKeyDown(KeyCode.B))    //big damage example
        {
            TakeDamage(150);
        }
        if (Input.GetKeyDown(KeyCode.A))    //armour increase example
        {
            if (currentArmour + 50 > maxArmour)
            {
                currentArmour = 100;
            }
            else
            {
                currentArmour += 50;
            }
            armourBar.SetArmour(currentArmour);
        }
        if (Input.GetKeyDown(KeyCode.M))    //use mana example
        {
            UseMana(30);
        }
        if (Input.GetKeyDown(KeyCode.P))    //mana increase example
        {
            if (currentMana + 50 > maxMana)
            {
                currentMana = 100;
            }
            else
            {
                currentMana += 50;
            }
            manaBar.SetMana(currentMana);
        }
    }*/


    void TakeDamage(int damage)
    {
        if (currentHealth + currentArmour < damage)
        {
            currentHealth = 0;
            currentArmour = 0;
            Debug.Log("DEAD");
        }
        else if (currentArmour > damage)
        {
            currentArmour -= damage;           
        }
        else //doesnt have enough armour but has enough health to survive
        {
            currentArmour -= damage;
            currentHealth += currentArmour;
            currentArmour = 0;
        }
        //healthBar.SetHealth(currentHealth);
        armourBar.SetArmour(currentArmour);
    }

    void UseMana(int mana)
    {
        if (currentMana > mana)
        {
            currentMana -= mana;
            manaBar.SetMana(currentMana);
        }
        else
        {
            Debug.Log("Not enough mana");
        }
    }
}
