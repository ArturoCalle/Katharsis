using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;

namespace UnityStandardAssets.Assets.ThirdPerson
{
    public class DistimiaAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        public AICharacter character;

        private int targetIndex = 0;
        public float velocidadDePaseo = 0f;

        public float chaseSpeed = 10f;
        public GameObject target;

        public bool isAlive;
        
        public enum State
        {
            pasear,
            buscarTrompi
        }
        public State state;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<AICharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = DistimiaAI.State.pasear;
            isAlive = true;

            StartCoroutine("FSM");
        }
        IEnumerator FSM()
        {
            while (isAlive)
            {
                switch (state)
                {
                    case State.pasear:
                        Pasear();
                        break;
                    case State.buscarTrompi:
                        BuscarTrompi();
                        break;
                }
                yield return null;
            }
        }


        void Pasear()
        {
            agent.speed = velocidadDePaseo;
            if(Vector3.Distance(this.transform.position, SceneIAController.instance.targets[targetIndex].transform.position ) >= 2)
            {
                agent.SetDestination(SceneIAController.instance.targets[targetIndex].transform.position);
                velocidadDePaseo = character.Move(agent.desiredVelocity, false);
            }else if (Vector3.Distance(this.transform.position, SceneIAController.instance.targets[targetIndex].transform.position) <= 2)
            {
                targetIndex += 1;
                if (targetIndex == SceneIAController.instance.targets.Length)
                {
                    SceneIAController.instance.destroyDistimia();
                }
            }
            else
            {
                velocidadDePaseo = character.Move(Vector3.zero, false);
            }
            if (velocidadDePaseo == 8f && character.getTarget() != null)
            {
                target = character.getTarget();
                state = DistimiaAI.State.buscarTrompi;
            }
        }
      
        void BuscarTrompi()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false);
        }
    }
}

