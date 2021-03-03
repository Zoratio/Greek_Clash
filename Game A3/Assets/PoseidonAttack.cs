using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PoseidonAttack : MonoBehaviour
{
    public LineRenderer beamLine;
    public Transform target;

    [SerializeField]
    private Transform tridentPoint;

    public Transform firePoint;

    [SerializeField]
    private GameObject waterBall;

    [SerializeField]
    public float shotVolume;

    [HideInInspector]
    public bool trackPlayer;

    public AudioMixerGroup audioMixer;

    private bool aim;
    private bool shot;
    private int shotCount;

    private bool activeAttack = false; //Change to private later - testing var

    private Transform drawTarget;
    private float timeToFire;

    private Gradient beamGradient;
    private Gradient aimGradient;

    private Animator anim;

    void Awake() {
        setupBeamGradients();
    }

    void Start() {
        beamLine.enabled = false;
        anim = GetComponent<Animator>();
        timeToFire = 0f;
        aim = false;
        shot = false;
        shotCount = 0;
    }

    void Update() {
        LockOnTarget(target.position);

        if (trackPlayer) {
            trackPlayer = false;
            aim = true;
            timeToFire = 5f;
            beamLine.colorGradient = aimGradient;
            beamLine.endWidth = 1f;
            beamLine.enabled = true;
        }

        if (aim) {
            if (timeToFire > 0) {
                timeToFire -= Time.deltaTime;
                RaycastHit hit;
                if (Physics.Raycast(tridentPoint.position, target.position - tridentPoint.position, out hit, Mathf.Infinity)) {
                    beamLine.SetPosition(0, tridentPoint.position);
                    beamLine.SetPosition(1, hit.point);
                }
                if (beamLine.endWidth > beamLine.startWidth) {
                    beamLine.endWidth -= Time.deltaTime / 7f;
                }
            } else if (timeToFire <= 0) {
                aim = false;
                beamLine.enabled = false;
                StartCoroutine(PreShotWait(0.1f));
            }
        }

        //For water bolt attack
        if (activeAttack) {
            activeAttack = false;
            StartCoroutine(WaterballCoroutine(0.8f));
        }

    }

    void LockOnTarget(Vector3 targetPos) {
        Vector3 dir = targetPos - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 3f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void DrawWaterBeam(Transform drawTarget) {
        if (!beamLine.enabled)
            beamLine.enabled = true;
    }

    void setupBeamGradients() {
        beamGradient = beamLine.colorGradient;
        aimGradient = new Gradient();
        GradientColorKey[] tempck = new GradientColorKey[2];
        tempck[0].color = Color.red;
        tempck[0].time = 0f;
        tempck[1].color = Color.red;
        tempck[1].time = 1f;
        aimGradient.SetKeys(tempck, beamGradient.alphaKeys);
    }

    IEnumerator PreShotWait(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        activeAttack = true;
        beamLine.colorGradient = beamGradient;
        beamLine.endWidth = beamLine.startWidth;
    }

    // Water bolt attack
    IEnumerator WaterballCoroutine(float waitTime) {
        anim.SetTrigger("Fireball");
        shotCount++;

        waitTime -= 0.8f;

        yield return new WaitForSeconds(0.5f);

        Vector3 dir = target.position - firePoint.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        GameObject waterballInstantiated = GameObject.Instantiate(waterBall, new Vector3(firePoint.position.x + dir.x / 20, firePoint.position.y, firePoint.position.z + dir.z / 20), lookRotation);
        waterballInstantiated.transform.GetChild(1).tag = "damage10";

        foreach (AudioSource a in waterballInstantiated.GetComponents<AudioSource>()) {
            a.volume = shotVolume;
            a.outputAudioMixerGroup = audioMixer;
        }

        yield return new WaitForSeconds(1f);

        if (shotCount < 3) {
            activeAttack = true;
        } else {
            shotCount = 0;
            aim = false;
        }
    }
}
