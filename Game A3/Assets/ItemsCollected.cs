using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsCollected : MonoBehaviour
{

    Inventory inventory;

    public bool axe = true;
    public bool iceSword = false;
    public bool dagger = false;
    public bool shield = false;

    public GameObject iceSwordGO;
    public GameObject daggerGO;
    public GameObject shieldGO;
    public GameObject axeGO;
    int count = 10;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (count == 10) {
            count = 0;
            foreach (Item i in inventory.items)
            {
                switch (i.name)
                {
                    case "HolySword":
                        iceSword = true;
                        break;
                    case "Shield":
                        shield = true;
                        break;
                    case "Dagger":
                        dagger = true;
                        break;
                }
            }
            Animator anim = GameObject.Find("Low Poly Warrior").GetComponent<Animator>();
            if (!axe)
            {
                axeGO.SetActive(false);
                anim.SetBool("WeaponDrawn", false);
            }
            else
            {
                axeGO.SetActive(true);
            }
            if (iceSword)
            {
                axeGO.GetComponent<MeshRenderer>().enabled = false;
                iceSwordGO.SetActive(true);
            }
            if (dagger)
            {
                daggerGO.SetActive(true);
            }
            if (shield)
            {
                shieldGO.SetActive(true);
                Damage damage = GameObject.Find("Low Poly Warrior").GetComponent<Damage>();
                damage.damageReduction = 20;
            }
        }
        count++;
    }
}
