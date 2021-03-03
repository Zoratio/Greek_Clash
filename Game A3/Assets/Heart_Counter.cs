using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class Heart_Counter : MonoBehaviour
{
    public int health;
    private int shownHearts = 3;
    public GameObject heartCont;
    // Start is called before the first frame update
    void Start()
    {
    }

    
    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            StartCoroutine(death());
        }
        if(shownHearts > health)
        {
            loseHeart();
        }
    }

    private void loseHeart()
    {
        shownHearts--;
        heartCont.transform.GetChild(shownHearts).gameObject.SetActive(false);
    }

    IEnumerator death()
    {
        GameObject knight = GameObject.Find("Meshtint Free Knight");
        knight.GetComponent<Animator>().SetBool("Death", true);
        knight.GetComponent<ThirdPersonUserControl>().enabled = false;
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
