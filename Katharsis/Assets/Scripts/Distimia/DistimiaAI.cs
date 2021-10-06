using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;

namespace UnityStandardAssets.Assets.ThirdPerson
{
    public class DistimiaAI : MonoBehaviour
    {
        //NavMesh
        public NavMeshAgent agent;
        public AICharacter character;
        //Ruta
        private int targetIndex = 0;
        private int inicioRuta;
        private int finRuta;
        private float velocidadDePaseo = 0f;
        //Persecución Trompi
        private float chaseSpeed = 0f;
        public GameObject jugador;
        //Variables utiles
        public bool isAlive;
        private bool mirarTrompi;
        private bool escondido;
        //Cambios de estado
        public float radioBusqueda = 20f;
        public float radioGolpe = 10f;

        public enum State
        {
            pasear,
            buscarTrompi,
            golpearTrompi
        }
        //Control estado
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
        //Cambia de estados segun las variables de cambio de estado y la escena en la que se encuentre
        private void Update()
        {
            float distance = Vector3.Distance(jugador.transform.position, transform.position);
            if (SceneController.instance.getCurrentSceneName() != "Sala")
            {

                if (true) //TODO cono de visión
                {
                    mirarTrompi = true;
                }
                if(distance <= radioBusqueda)
                {
                    state = State.buscarTrompi;
                }
                if (distance <= radioGolpe)
                {
                    state = State.golpearTrompi;
                }

            }
        }
        //corutina
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
                        PerseguirTrompi();
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
                RigController.instance.Mirar(jugador.transform);
            }
            agent.speed = velocidadDePaseo;
            //Ruta con NavMesh
            if(Vector3.Distance(this.transform.position, SceneIAController.instance.targets[targetIndex].transform.position ) > 2)
            {
                agent.SetDestination(SceneIAController.instance.targets[targetIndex].transform.position);
                velocidadDePaseo = character.Move(agent.desiredVelocity, false);
            }else if (Vector3.Distance(this.transform.position, SceneIAController.instance.targets[targetIndex].transform.position) <= 2)
            {
                //Pasa al siguiente target TODO sistema de rutas
                targetIndex += 1;
                if (targetIndex == SceneIAController.instance.targets.Length)
                {
                    SceneIAController.instance.destroyDistimia();
                }
            }
        }
      
        void PerseguirTrompi()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(jugador.transform.position);
            chaseSpeed = character.Move(agent.desiredVelocity, true);
            RigController.instance.Mirar(jugador.transform);
        }

        void GolpearTrompi()
        {
            //TODO
        }

    }
}

