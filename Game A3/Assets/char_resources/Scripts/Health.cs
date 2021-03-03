using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    Slider health;

    GameObject player;
    Transform camera;

    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        health = this.GetComponent<Slider>();
        player = GameObject.Find("Low Poly Warrior");
        camera = player.transform.GetChild(2);
    }

    private void Update()
    {
        if (health.value == 0)
        {
            Death();
        }
    }

    void Death()
    {
        playerAnimator.SetTrigger("Death");
        player.GetComponent<Movement2>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = true;

        StartCoroutine(MoveCam());
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        PlayerPrefs.SetFloat("CurrentHP", 100);
        PlayerPrefs.SetFloat("CurrentArmour", 50);
        float time = 3f;
        while (time > 0)
        {
            //Debug.Log(time);
            time -= Time.deltaTime;
            yield return null;
        }

        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);

        PlayerPrefs.SetString("ShopExit", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Shop");

        yield return null;
    }

    IEnumerator MoveCam()
    {
        float elapsedTime = 0;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            camera.localEulerAngles = Vector3.Lerp(camera.localEulerAngles, new Vector3(20, 0, 0), (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        camera.localEulerAngles = new Vector3(20, 0, 0);
        yield return null;
    }
}
