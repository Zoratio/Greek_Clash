using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    Animator anim;
    GameObject camera;

    public GameObject fireball;
    public GameObject mana;

    public bool triggerActive = false;
    public bool hasMana = true;
    public bool casting = false;
    public float volume;

    public AudioMixerGroup audioMixer;

    Quaternion characterRotation;
    Vector3 Pos;


    private void Start()
    {
        anim = GetComponent<Animator>();
        camera = GameObject.Find("Camera");
        mana = GameObject.Find("ManaBar");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject character = GameObject.Find("Low Poly Warrior");
        Pos = new Vector3(
                    character.transform.localPosition.x,
                    character.transform.localPosition.y + 1f, 
                    character.transform.localPosition.z
                    );
        Pos += character.transform.forward*3;

        if (camera.activeSelf) { 
            characterRotation = GameObject.Find("Camera").transform.rotation;
        }
        else
        {
            characterRotation = GameObject.Find("Camera (1)").transform.rotation;
        }

        if (!triggerActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
                triggerActive = true;
            }
        }
        Slider manaBar = mana.GetComponent<Slider>();

        if (manaBar.value >= 30)
        {
            hasMana = true;
        }
        else
        {
            hasMana = false;
        }

        if (hasMana && !casting)
        {
            if (Input.GetMouseButtonDown(1))
            {
                manaBar.value -= 30;
                StartCoroutine(FireballCoroutine());
            }
        }
    }

    IEnumerator FireballCoroutine()
    {
        anim.SetTrigger("Fireball");
        casting = true;


        yield return new WaitForSeconds(0.5f);

        GameObject fireballInstantiated = GameObject.Instantiate(fireball, Pos, characterRotation);
        fireballInstantiated.transform.localEulerAngles += new Vector3(-10, 0, 0);
        fireballInstantiated.transform.GetChild(1).tag = "damage10";

        foreach (AudioSource a in fireballInstantiated.GetComponents<AudioSource>())
        {
            a.volume = volume;
            a.outputAudioMixerGroup = audioMixer;
        }
    }
}
