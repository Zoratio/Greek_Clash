using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    [Range(0.02f,100f)]
    public float flickerDelay = 0.2f;

    [SerializeField]
    private AudioMixerGroup mixer;

    public Sound[] thunderSounds = new Sound[5];

    private bool isFlickering = false;
    private float timeDelay;

    private AudioSource audioSrc;

    void Awake() {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.clip = thunderSounds[0].clip;
        audioSrc.volume = 1f;
        audioSrc.pitch = thunderSounds[0].pitch;
        audioSrc.spatialBlend = 0.7f;
        audioSrc.outputAudioMixerGroup = mixer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false) {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight() {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.01f, flickerDelay);
        yield return new WaitForSeconds(timeDelay);
        int randSndIndex = Random.Range(0, thunderSounds.Length);
        audioSrc.clip = thunderSounds[randSndIndex].clip;
        audioSrc.pitch = thunderSounds[randSndIndex].pitch;
        float thndIntens = Random.Range(1f, 10f);
        this.gameObject.GetComponent<Light>().intensity = thndIntens;
        audioSrc.volume = Mathf.Clamp(thndIntens,0f, 1f);
        this.gameObject.GetComponent<Light>().enabled = true;
        if (!audioSrc.isPlaying) {
            audioSrc.Play();
        }
        timeDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.01f, 0.1f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().intensity = Random.Range(1f, 10f);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
