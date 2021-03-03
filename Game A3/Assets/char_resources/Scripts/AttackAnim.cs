using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim : StateMachineBehaviour
{

    GameObject iceSword;
    GameObject dagger;
    GameObject shield;
    GameObject axe;
    GameObject leftHand;
    GameObject rightHand;

    ItemsCollected itemsScript;
    Attack attackScript;
    float time = 0f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        iceSword = GameObject.Find("Ice Sword (Player)");
        dagger = GameObject.Find("Dagger (Player)");
        shield = GameObject.Find("Shield (Player)");

        animator.ResetTrigger("ExitAttack");
        time = 0f;

        attackScript = GameObject.Find("Low Poly Warrior").GetComponent<Attack>();
        attackScript.triggerActive = false;

        animator.ResetTrigger("Attack");

        axe = GameObject.Find("axe");
        leftHand = GameObject.Find("hand.L");
        rightHand = GameObject.Find("hand.R");
        if (animator.GetBool("WeaponDrawn"))
        {
            if (iceSword != null) { 
                iceSword.tag = "damage35";
            }
            else
            {
                axe.tag = "damage20";
            }
        }
        else
        {
            if (dagger != null)
            {
                dagger.tag = "damage10";
            }
            else { 
                rightHand.tag = "damage5";
            }
            if (shield != null)
            {
                shield.tag = "damage10";
            }
            else
            {
                leftHand.tag = "damage5";
            }
             
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        time += Time.deltaTime;
        if(time > 0)
        {
            animator.SetTrigger("ExitAttack");
        } 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("Attack")) { 
            if(axe != null) { 
                axe.tag = "Untagged";
            }
            leftHand.tag = "Untagged";
            rightHand.tag = "Untagged";
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
