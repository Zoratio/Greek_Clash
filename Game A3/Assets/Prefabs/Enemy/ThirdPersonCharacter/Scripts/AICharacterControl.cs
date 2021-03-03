using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for


        public bool move = true;
        bool lockedMovement = false;
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update() { 
        
            if (!move)
            {
                move = true;
                StartCoroutine(StopMovement());
            }
            if (!lockedMovement)
            {
                if (target != null)
                {
                    agent.SetDestination(target.position);
                }

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                }
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        IEnumerator StopMovement()
        {
            lockedMovement = true;
            yield return new WaitForSeconds(1.5f);
            lockedMovement = false;
        }

    }
}
