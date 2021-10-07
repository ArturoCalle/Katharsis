using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Assets.ThirdPerson
{
    public class DistimiaAI : MonoBehaviour
    {
        //NavMesh
        public NavMeshAgent agent;
        public AICharacter character;
        public GameObject cabeza;
        //Ruta
        private int targetIndex = 0;
        private int inicioRuta;
        private int finRuta;
        private float velocidadDePaseo = 0f;
        //Persecución Trompi
        private float chaseSpeed = 0f;
        //Distimia se destruye cuando estas estan en true y termina la ruta
        public bool SalirSala;
        public bool SalirComedor;
        //Variables utiles
        private bool mirarTrompi;
        public bool puedeVerTrompi;
        public float anguloDeBusqueda = 120; //angulo de rango de busqueda trompi. Este angulo debe ser el doble al radio de efecto de HeadAim (de -60 a 60, osea 120 para esta funcionalidad)
        public GameObject Jugador;
        //Cambios de estado
        public float radioBusqueda = 20f;
        public float radioGolpe = 10f;
        //LayerMasks
        public LayerMask targetMask;
        public LayerMask obstructionMask;

        public enum State
        {
            Tranquilo,
            Enfadado,
            Ansioso
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
            Jugador = SceneController.instance.jugador;
            state = State.Tranquilo;
            StartCoroutine("FSM");
            SalirSala = false;
            SalirComedor = false;
            if (SceneController.instance.getCurrentSceneName() == "Sala")
            {
                inicioRuta = 3;
            }else if (SceneController.instance.getCurrentSceneName() == "Comedor")
            {
                inicioRuta = 1;
            }
        }
        //Cambia de estados segun las variables de cambio de estado y la escena en la que se encuentre
        private void Update()
        {
            if (SceneController.instance.getCurrentSceneName() == "Sala")
            {
                if (SceneTriggerController.instance.findTriggerByName("megafono").recolectado)
                {
                    SalirSala = true;
                }
            }
        }
        //corutina
        IEnumerator FSM()
        {
            while (true)
            {
                FieldOfViewCheck();
                switch (state)
                {
                    case State.Tranquilo:
                        Pasear();
                        break;
                    case State.Enfadado:
                        PerseguirTrompi();
                        break;
                    case State.Ansioso:
                        Ansiedad();
                        break;
                }
                yield return null;
            }
        }

        void Pasear()//Al movimiento de trompi se le envian dos variables de estado, en esta funcion, las dos variables estan el false
        {
            agent.speed = velocidadDePaseo;
            //Ruta con NavMesh
            if(Vector3.Distance(this.transform.position, SceneIAController.instance.targets[targetIndex].transform.position ) > 2)
            {
                agent.SetDestination(SceneIAController.instance.targets[targetIndex].transform.position);
                velocidadDePaseo = character.Move(agent.desiredVelocity, false, false);
            }else if (Vector3.Distance(this.transform.position, SceneIAController.instance.targets[targetIndex].transform.position) <= 2)
            {
                targetIndex += 1;
                if (targetIndex == SceneIAController.instance.targets.Length)
                {
                    if (SalirSala)
                    {
                        SceneIAController.instance.destroyDistimia();
                    }
                    else
                    {
                        targetIndex = inicioRuta;
                    }
                }
            }
        }
      
        void PerseguirTrompi()//Al movimiento de trompi se le envian dos variables de estado, en esta funcion, la variable de enojado esta en true
        {
            agent.speed = chaseSpeed;
            chaseSpeed = character.Move(agent.desiredVelocity, true, false);
            agent.SetDestination(Jugador.transform.position);
            RigController.instance.Mirar(Jugador.transform);
            //TODO animaiciones extra, sonidos, demas mecanicas
        }

        void GolpearTrompi()
        {
            //TODO
        }

        void Ansiedad()//Al movimiento de trompi se le envian dos variables de estado, en esta funcion, la variable de ansioso esta en true
        {
            //TODO
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(cabeza.transform.position, radioBusqueda, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - cabeza.transform.position).normalized;

                if (Vector3.Angle(cabeza.transform.forward, directionToTarget) < anguloDeBusqueda / 2)
                {
                    float distanceToTarget = Vector3.Distance(cabeza.transform.position, target.position);
                    if (!Physics.Raycast(cabeza.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        puedeVerTrompi = true;
                        if(SceneController.instance.getCurrentSceneName() != "Sala")
                        {
                            state = State.Enfadado;

                        }
                    }
                    else
                    {
                        puedeVerTrompi = false;
                    }
                }
                else
                {
                    puedeVerTrompi = false;
                }
            }
            else if (puedeVerTrompi)
            {
                puedeVerTrompi = false;
            }
        }
    }
}

