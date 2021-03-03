using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Camera mainCam;
    public Interactable focus;
    public CharacterController controller;
    private int count; 
    public Text countText; 
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Interactable inter = hit.collider.GetComponent<Interactable>();
        if (inter != null)
        {
            SetFocus(inter);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // We create a ray
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }


    void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
                                //motor.FollowTarget(newFocus);	// Follow the new focus
        }

        newFocus.OnFocused(transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }

    }

    void setCountText()
    {
        countText.text = "Gold: " + count.ToString();
    }

}
