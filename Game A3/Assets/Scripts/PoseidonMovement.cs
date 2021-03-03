using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonMovement : MonoBehaviour
{
    float timeLeft;
    float speed = 2;
    bool up;
    [SerializeField]
    private PoseidonAttack attackScr;

    [SerializeField]
    private float maxLowPos;

    [SerializeField]
    private float maxHighPos;

    public List<Vector3> positions = new List<Vector3>();

    private bool shooting;

    void Awake() {
        for (int i = 0; i < positions.Capacity; i++) {
            positions[i] = new Vector3(positions[i].x, maxLowPos, positions[i].z);
        }
    }

    void Start()
    {
        timeLeft = 5;   //change this to 30 when done testing
        up = false;
        shooting = false;

        transform.position = positions[0];
    }


    void FixedUpdate()
    {
        if (!up)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, maxLowPos, transform.position.z), Time.deltaTime * speed);
            if (transform.position.y == maxLowPos)
            {
                ChangeLocation();
            }
        }
        else if (up && !shooting)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, maxHighPos, transform.position.z), Time.deltaTime * speed);
            if (transform.position.y == maxHighPos)
            {
                shooting = true;
                StartCoroutine(waitToShoot(12f));
            }
        }
    }

    void ChangeLocation()
    {
        transform.position = positions[Random.Range(0, positions.Count)];
        up = true;
    }

    IEnumerator waitToShoot(float waitTime) {
        attackScr.trackPlayer = true;
        yield return new WaitForSeconds(waitTime);
        up = false;
        shooting = false;
    }
}
