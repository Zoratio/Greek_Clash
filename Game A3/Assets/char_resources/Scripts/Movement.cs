using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    public Camera mainCam;

    [Header("Axe Settings")]
    public Transform axe;
    public Transform axeEquipped, axeUnequipped, hand, back;
    public float equipTime, unequipTime;
    [Space(10)]

    [Header("Movement")]
    public float accelSpeed = 20f;
    public float maxRunSpeed = 20f;
    public float backpedalSlow = 2f;
    public float strafeMoveSlow = 2f;
    public float weaponDrawnMult = 1f;
    public float mouseSens = 100f;
    public float strafeAccelSpeed = 10f;
    public float maxStrafeSpeed = 15f;
    public float jumpForce = 1f;
    public float minRotation, maxRotation;
    public bool invertMouseY = false;
    public bool airControl = true;
    [Space(10)]
    public float maxStepHeight = 0.4f;
    public float stepSearchOvershoot = 0.01f;
    [Space(10)]

    [Header("Gravity")]
    public bool useGravity = true;
    public float gravityStrength = 1f;

    private bool grounded;
    private float trueMaxRunSpeed;
    private List<ContactPoint> allCPs = new List<ContactPoint>();

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Idling", true);
        anim.SetBool("Strafing", false);

        rb = GetComponent<Rigidbody>();

        trueMaxRunSpeed = maxRunSpeed;
    }

    void Update()
    {
        Jump();
        Equip();
        if(grounded || airControl) { 
            Move();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        allCPs.AddRange(collision.contacts);
    }

    private void OnCollisionStay(Collision collision)
    {
        allCPs.AddRange(collision.contacts);
    }
    void FixedUpdate()
    {
        rb.useGravity = false;
        rb.AddForce(Physics.gravity * gravityStrength * rb.mass );


        ContactPoint groundCP = default(ContactPoint);
        grounded = FindGround(out groundCP, allCPs);
        

        Vector3 stepUpOffset = default(Vector3);
        bool stepUp = false;
        if (grounded)
        {
            stepUp = FindStep(out stepUpOffset, allCPs, groundCP);
        }
        if (stepUp)
        {
            Debug.Log(stepUpOffset);
            this.GetComponent<Rigidbody>().position += stepUpOffset;
        }

        allCPs.Clear();
    }

    public void Move()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float translationX = Input.GetAxis("Horizontal") * strafeAccelSpeed;
        float translationZ = Input.GetAxis("Vertical") * accelSpeed;
        

        if(anim.GetBool("WeaponDrawn"))
        {
            translationX *= weaponDrawnMult;
            translationZ *= weaponDrawnMult;
        }

        maxRunSpeed = ForwardPenalties(translationX, translationZ, trueMaxRunSpeed);

        if(transform.InverseTransformDirection(rb.velocity).z > maxRunSpeed && !(transform.InverseTransformDirection(rb.velocity).z < -(maxRunSpeed/backpedalSlow)))
        {
            translationZ = maxRunSpeed;
        }
        else if(transform.InverseTransformDirection(rb.velocity).z < -(maxRunSpeed / backpedalSlow))
        {
            translationZ = -maxRunSpeed / backpedalSlow;
        }

        if(Mathf.Abs(transform.InverseTransformDirection(rb.velocity).x) > maxStrafeSpeed)
        {
            translationX = Mathf.Clamp(translationX, -maxStrafeSpeed, maxStrafeSpeed);
        }

        rb.AddRelativeForce(new Vector3(translationX * Time.deltaTime, 0f, translationZ * Time.deltaTime), ForceMode.Impulse);

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
    public void FootR(){}
    public void FootL(){}
    public void WeaponSwitch(){}
    public void Land(){}

    public void Jump()
    {
        anim.SetBool("Grounded", grounded);
        if (Input.GetKey(KeyCode.Space) && anim.GetBool("Grounded"))
        {
            anim.SetTrigger("JumpTrigger");
            rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base.2Hand-Sword-Jump"))
        {
            anim.ResetTrigger("JumpTrigger");
        }

        anim.SetFloat("VertSpeed", rb.velocity.y);
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

    private bool FindGround(out ContactPoint groundCP, List<ContactPoint> allCPs)
    {
        groundCP = default(ContactPoint);
        bool found = false;
        foreach (ContactPoint cp in allCPs)
        {
            if (cp.normal.y > 0.0001f && (found == false || cp.normal.y > groundCP.normal.y))
            {
                groundCP = cp;
                found = true;
            }
        }
        return found;
    }

    private bool FindStep(out Vector3 stepUpOffset, List<ContactPoint> allCPs, ContactPoint groundCP)
    {
        stepUpOffset = default(Vector3);

        foreach (ContactPoint cp in allCPs)
        {
            bool test = ResolveStepUp(out stepUpOffset, cp, groundCP);
            if (test) { return test; }
        }
        return false;
    }

    private bool ResolveStepUp(out Vector3 stepUpOffset, ContactPoint stepTestCP, ContactPoint groundCP)
    {
        stepUpOffset = default(Vector3);
        Collider stepCol = stepTestCP.otherCollider;


        if (Mathf.Abs(stepTestCP.normal.y) >= 0.01f)
        {
            ;
            return false;
        }

        if (!(stepTestCP.point.y - groundCP.point.y < maxStepHeight))
        {
            return false;
        }

        RaycastHit hitInfo;
        float stepHeight = groundCP.point.y + maxStepHeight + 0.0001f;
        Vector3 stepTestInvDir = new Vector3(-stepTestCP.normal.x, 0, -stepTestCP.normal.z).normalized;
        Vector3 origin = new Vector3(stepTestCP.point.x, stepHeight, stepTestCP.point.z) + (stepTestInvDir * stepSearchOvershoot);
        Vector3 direction = Vector3.down;
        if (!(stepCol.Raycast(new Ray(origin, direction), out hitInfo, maxStepHeight)))
        {
            return false;
        }

        Vector3 stepUpPoint = new Vector3(stepTestCP.point.x, hitInfo.point.y + 0.0001f, stepTestCP.point.z) + (stepTestInvDir * stepSearchOvershoot);
        Vector3 stepUpPointOffset = stepUpPoint - new Vector3(stepTestCP.point.x, groundCP.point.y, stepTestCP.point.z);

        stepUpOffset = stepUpPointOffset;
        Debug.Log("4");
        return true;
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

            while(timeSpent < totalTime)
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
