using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (AICharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public AICharacter character { get; private set; }                         // the character we are controlling
        public Transform target;                                                   // target to aim for

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
            }
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false);
            }
            if (agent.remainingDistance == agent.stoppingDistance)
            {
                //TODO cuando llega al destino 1
            }
            else
            {
                character.Move(Vector3.zero, false);
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
