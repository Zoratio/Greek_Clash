using UnityEngine;
using System.Collections;

public class CameraCollisionHandler : MonoBehaviour
{
    public Transform playerTarget;

    float zPos;
    Vector3 lastFinalPos;
    private bool camMoving;
    private float initCamDist;
    private Vector3 initCamPos;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        zPos = transform.localPosition.z;
        lastFinalPos = transform.localPosition;
        camMoving = false;
        initCamPos = transform.localPosition;
        initCamDist = (playerTarget.localPosition - transform.localPosition).magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MoveCam(Vector3.forward));
    }

    private void Update()
    {
        if (!camMoving) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerTarget.position - transform.position, out hit, initCamDist)) {
                //Debug.Log(hit.transform.name);
                if (hit.collider.CompareTag("Player") && hit.distance < initCamDist) {
                    StartCoroutine(MoveCam(Vector3.back));
                }
            }
        }
    }

    IEnumerator MoveCam(Vector3 inVec)
    {
        camMoving = true;
        Vector3 finalPos = lastFinalPos + inVec;
        if ((finalPos - playerTarget.localPosition).magnitude < initCamDist)
            lastFinalPos = finalPos;
        else
            lastFinalPos = initCamPos;

        float elapsedTime = 0;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = finalPos;
        camMoving = false;
        yield return null;
    }
}