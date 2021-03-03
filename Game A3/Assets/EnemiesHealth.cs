using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemiesHealth : MonoBehaviour
{
    Slider[] sliders;
    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        sliders = this.GetComponentsInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 60)
        {
            timer = 0;
            int count = 0;
            foreach (Slider s in sliders)
            {
                if (s.value == 0)
                {
                    count++;
                }
            }

            if (count == 24)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                SceneManager.LoadScene("FinalDialogue");
            }
            Debug.Log(count);
        }
        timer++;
    }
}
