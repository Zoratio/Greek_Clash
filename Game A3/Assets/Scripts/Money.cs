using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //added
using UnityEngine.UI;//added

public class Money : MonoBehaviour
{
    public int count = 0; //added
    public Text countText; //added
    public Item item;
    public Slider health;

    bool reset = false;
    int earnedThisLevel = 0;

    void Start()
    {
        count = PlayerPrefs.GetInt("Money");
        setCountText();
    }

    void Update()
    {
        if (health.value == 0 && !reset)
        {
            reset = true;
            count -= earnedThisLevel;
        }
        PlayerPrefs.SetInt("Money", count);
    }



    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "SilverCluster":
                other.gameObject.SetActive(false);
                count = count + 15;
                earnedThisLevel += 15;
                setCountText();
                break;
            case "GoldCluster":
                other.gameObject.SetActive(false);
                count = count + 75;
                earnedThisLevel += 75;
                setCountText();
                break;
            case "Silver":
                other.gameObject.SetActive(false);
                count = count + 5;
                earnedThisLevel += 5;
                setCountText();
                break;
            case "Gold":
                other.gameObject.SetActive(false);
                count = count + 25;
                earnedThisLevel += 25;
                setCountText();
                break;
        }

    }

    void setCountText()
    {
        countText.text = "Gold: " + count.ToString();
    }

   



}
