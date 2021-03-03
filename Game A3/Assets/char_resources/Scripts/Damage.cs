using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    public GameObject armour;
    public GameObject health;
    Slider armourSlider;
    Slider healthSlider;
    Collider col;

    List<GameObject> lastHits = new List<GameObject>();
    //GameObject[] lastHits = new GameObject[30];

    [Range(0f, 100f)]
    public float damageReduction;

    bool waterDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<Collider>();

        if(armour != null) { 
            armourSlider = armour.GetComponent<Slider>();
        }

        healthSlider = health.GetComponent<Slider>();
    }

    private void Update()
    {
        if (this.gameObject.name == "PoseidonChar")
        {
            if (healthSlider.value <= 0)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                this.gameObject.GetComponent<Animator>().SetTrigger("Death");
                StartCoroutine(NextSceneWait(5f));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ocean" || other.name == "lavadamage")
        {
            if (this.tag == "Player" && !waterDamage)
            {
                waterDamage = true;
                StartCoroutine(WaterDamage(other.tag));
            }
        }
        else
        {
            if (this.tag == "Player" && (other.name == "axe" || other.name == "hand.L" || other.name == "hand.R" || other.name == "Ice Sword (Player)" || other.name == "Dagger (Player)" || other.name == "Shield (Player)"))
            {

            }
            else
            {
                if (other.tag.Contains("damage"))
                {
                    DealDamage(other.tag, null, other);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (this.tag == "Player" && (other.name == "Ocean" || other.name == "lavadamage"))
        {
            waterDamage = false;
        }
    }


    IEnumerator WaterDamage(string tag)
    {
        while (waterDamage)
        {
            DealDamage(tag, null, null);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag.Contains("damage"))
        {
            DealDamage(collision.transform.tag, collision, null);
        }
    }

    private void DealDamage(string tag, Collision collision, Collider other)
    {
        string damageAmount = tag;
        damageAmount = damageAmount.Substring(6, damageAmount.Length - 6);

        //if (collision != null ) { collision.transform.tag = "Untagged"; }
        //if (other != null ) { other.tag = "Untagged"; }

        float damageRecieved = float.Parse(damageAmount);

        Debug.Log(PlayerPrefs.GetInt("Difficulty"));
        

        damageRecieved = damageRecieved * (1-(damageReduction/100));    //apply damage reduction

        if (this.tag == "Player")
        {
            switch (PlayerPrefs.GetInt("Difficulty"))
            {
                case 1:
                    damageRecieved *= 0.8f;
                    break;
                case 3:
                    damageRecieved *= 1.2f;
                    break;
            }
        }
        else
        {
            switch (PlayerPrefs.GetInt("Difficulty"))
            {
                case 1:
                    damageRecieved *= 1.2f;
                    break;
                case 3:
                    damageRecieved *= 0.8f;
                    break;
            }
        }

        Debug.Log(damageRecieved);

        if(collision != null) { 
            if(lastHits.Contains(collision.gameObject))
            {
                damageRecieved = 0f;
            }
            else
            {
                StartCoroutine(DamageTaken(collision.gameObject));
            }
        }
        if (other != null)
        {
            if (lastHits.Contains(other.gameObject))
            {
                damageRecieved = 0f;
            }
            else
            {
                StartCoroutine(DamageTaken(other.gameObject));
            }
        }

        if (armour != null)
        {
            if (armourSlider.value < damageRecieved)
            {
                damageRecieved -= armourSlider.value;       //if we take more damage than have armour reduce damage dealt by that amount
                armourSlider.value = 0;                     //remove armour
            }
            else
            {
                armourSlider.value -= damageRecieved;
                damageRecieved = 0;
            }
        }
        healthSlider.value -= damageRecieved;
    }

    IEnumerator DamageTaken(GameObject GO)
    {
        lastHits.Add(GO);
        yield return new WaitForSeconds(0.5f);
        lastHits.Remove(GO);
    }

    IEnumerator NextSceneWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("HermesDialogue");
    }
}
