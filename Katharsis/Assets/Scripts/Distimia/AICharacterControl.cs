using System;
using UnityEngine;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (AICharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public AICharacter character { get; private set; }                         // the character we are controlling
        private Transform target;                                                   // target to aim for

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<AICharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }else{
                target = TargetController.instance.getLast();
                agent.SetDestination(target.position);
            }
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false);
            }
            if (agent.remainingDistance == agent.stoppingDistance)
            {
                target = TargetController.instance.getNext();
            }
            else
            {
                character.Move(Vector3.zero, false);
            }
        }
    }
}
