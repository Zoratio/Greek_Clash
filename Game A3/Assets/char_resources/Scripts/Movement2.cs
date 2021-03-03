using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    Animator anim;


    [Header("Axe Settings")]
    public Transform axe;
    public Transform axeEquipped, axeUnequipped, hand, back;
    public float equipTime, unequipTime;
    [Space(10)]

    [Header("Movement")]
    public float runSpeed = 5f;
    public float strafeSpeed = 3f;
    public float backpedalSlow = 2f;
    public float strafeMoveSlow = 2f;
    public float weaponDrawnSlow = 1f;
    public float jumpForce = 1f;
    public bool airControl = true;
    [Space(10)]

    [Header("Camera")]
    public Camera mainCam;
    public float mouseSens = 100f;
    public float minRotation, maxRotation;
    public bool invertMouseY = false;
    [Space(10)]

    [Header("Gravity")]
    public float gravityStrength = 1f;

    private CharacterController controller;
    private float translationY = 0;
    private float maxRunSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Idling", true);
        anim.SetBool("Strafing", false);

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (this.GetComponent<ItemsCollected>().axe) { 
            Equip();
        }
        if (controller.isGrounded || airControl)
        {
            Move();
        }
    }


    public void Move()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float translationX = Input.GetAxis("Horizontal") * strafeSpeed;
        float translationZ = Input.GetAxis("Vertical");
        maxRunSpeed = ForwardPenalties(translationX, translationZ, runSpeed);
        translationZ *= maxRunSpeed;

        if (anim.GetBool("WeaponDrawn"))
        {
            translationX /= weaponDrawnSlow;
            translationZ /= weaponDrawnSlow;
        }

        Jump(out translationY);
        translationY -= gravityStrength * Time.deltaTime;

        controller.Move(transform.TransformDirection(new Vector3(translationX, translationY, translationZ) * Time.deltaTime));

        Camera();
        Strafe(translationX);
        anim.SetFloat("SpeedMult", translationZ);
        anim.SetBool("Idling", (translationZ == 0 && translationX == 0));

    }

    public float ForwardPenalties(float x, float z, float maxSpeed)
    {
        if (z < 0)
        {
            maxSpeed /= backpedalSlow;
        }
        if (x != 0)
        {
            maxSpeed /= strafeMoveSlow;
        }
        return maxSpeed;
    }


    //animation events
    public void FootR() { }
    public void FootL() { }
    public void WeaponSwitch() { }
    public void Land() { }

    public void Jump(out float jumpOutput)
    {
        anim.SetBool("Grounded", controller.isGrounded);
        if (Input.GetKey(KeyCode.Space) && anim.GetBool("Grounded"))
        {
            anim.SetTrigger("JumpTrigger");
            jumpOutput = jumpForce;
        }
        else
        {
            jumpOutput = translationY;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base.2Hand-Sword-Jump"))
        {
            anim.ResetTrigger("JumpTrigger");
        }

        anim.SetFloat("VertSpeed", controller.velocity.y);
    }

    public void Camera()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        if (invertMouseY)
        {
            mouseY = -mouseY;
        }
        mainCam.transform.localRotation *= Quaternion.Euler(new Vector3(mouseY, 0f, 0f));

        float curXRot = mainCam.transform.localEulerAngles.x;
        if (curXRot > 40)
        {
            curXRot = 0f;
        }
        float xRot = Mathf.Clamp(curXRot, minRotation, maxRotation);
        mainCam.transform.localEulerAngles = new Vector3(xRot, 0f, 0f);
    }

    public void Strafe(float translationX)
    {
        anim.SetBool("Strafing", ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || translationX != 0)));

        anim.SetFloat("SideMove", translationX);
        if ((Input.GetKey(KeyCode.A) && anim.GetFloat("SideMove") > 0) || (Input.GetKey(KeyCode.D) && anim.GetFloat("SideMove") < 0))
        {
            anim.SetBool("ChangeStrafe", true);
        }
        else
        {
            anim.SetBool("ChangeStrafe", false);
        }
    }

    public void Equip()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //checks if a sheath/unsheath animation is playing
            if (!anim.GetCurrentAnimatorStateInfo(1).IsName("Sheathing.2Hand-Sword-Unsheath-Back-Unarmed") && !anim.GetCurrentAnimatorStateInfo(1).IsName("Sheathing.2Hand-Sword-Sheath-Back-Unarmed"))
            {
                anim.SetBool("WeaponDrawn", !anim.GetBool("WeaponDrawn"));
                anim.SetTrigger("TabPress");


                StartCoroutine(EquipDelay(anim.GetBool("WeaponDrawn")));
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            anim.ResetTrigger("TabPress");
        }
    }

    IEnumerator EquipDelay(bool equip)
    {

        if (equip)
        {
            yield return new WaitForSeconds(equipTime);
            axe.position = axeEquipped.position;
            axe.rotation = axeEquipped.rotation;
            axe.parent = hand;
        }
        else
        {
            yield return new WaitForSeconds(unequipTime);
            axe.parent = back;
            float timeSpent = 0f;
            float totalTime = 0.5f;

            while (timeSpent < totalTime)
            {
                axe.position = Vector3.Lerp(axe.position, axeUnequipped.position, (timeSpent / totalTime));
                axe.rotation = Quaternion.Lerp(axe.rotation, axeUnequipped.rotation, (timeSpent / totalTime));

                timeSpent += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

        }

        //stops the axe position breaking on equip spam
        if (axe.position != axeUnequipped.position && axe.position != axeEquipped.position)
        {
            if (anim.GetBool("Weapon Drawn"))
            {
                axe.position = axeUnequipped.position;
                axe.rotation = axeUnequipped.rotation;
                axe.parent = back;

            }
            else
            {
                axe.position = axeEquipped.position;
                axe.rotation = axeEquipped.rotation;
                axe.parent = hand;
            }
        }

    }


}
