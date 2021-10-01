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
        float velocidadDePaseo = 5f;

        public float chaseSpeed = 1f;
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
                character.Move(agent.desiredVelocity, false);
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
                character.Move(Vector3.zero, false);
            }
        }

        void BuscarTrompi()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(SceneController.instance.getCurrentSceneName() != "Sala")
            {
                if (other.tag == "player")
                {
                    target = other.gameObject;
                    state = DistimiaAI.State.buscarTrompi;
                }

            }
        }
    }
}

