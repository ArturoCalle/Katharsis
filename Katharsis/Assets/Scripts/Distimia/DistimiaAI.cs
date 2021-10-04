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
        private float velocidadDePaseo = 0f;

        private float chaseSpeed = 0f;
        public GameObject jugador;

        public bool isAlive;
        private bool mirarTrompi;

        public float radioBusqueda = 20f;
        public float golpeBusqueda = 10f;

        public enum State
        {
            pasear,
            buscarTrompi,
            golpearTrompi
        }
        public State state;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<AICharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = State.pasear;
            isAlive = true;

            StartCoroutine("FSM");

            jugador = SceneController.instance.jugador;
        }

        private void Update()
        {
            float distance = Vector3.Distance(jugador.transform.position, transform.position);
            if (SceneController.instance.getCurrentSceneName() != "Sala")
            {
                if(distance <= radioBusqueda)
                {
                    //TODO chase speed
                    state = State.buscarTrompi;
                }
                if (distance <= radioGolpe)
                {
                    //TODO chase speed
                    state = State.golpearTrompi;
                }

            }
            else
            {
                mirarTrompi = true;
            }
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
                    case State.golpearTrompi:
                        GolpearTrompi();
                        break;
                }
                yield return null;
            }
        }


        void Pasear()
        {
            if (mirarTrompi)
            {
                RigController.instance.setRigWeightToOne(jugador.transform);
            }
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
        }
      
        void BuscarTrompi()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(jugador.transform.position);
            chaseSpeed = character.Move(agent.desiredVelocity, false);
            RigController.instance.setRigWeightToOne(jugador.transform);
        }

        void GolpearTrompi()
        {
            //TODO
        }

    }
}

