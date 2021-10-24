using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Assets.ThirdPerson
{
    public class DistimiaAI : MonoBehaviour
    {
        //NavMesh
        private NavMeshAgent agent;
        private AICharacter character;
        //RayCast
        public GameObject cabeza;
        public GameObject Hombro;
        private float distanciaHombro = 10f;
        //Ruta
        private int targetIndex = 0;
        private int inicioRuta;
        private int finRuta;
        private float velocidadDePaseo = 0f;
        //Persecución Trompi
        private float chaseSpeed = 0f;
        private float radioGolpe = 20f;
        private float alturagolpe = 5f;
        //Distimia se destruye cuando estas estan en true y termina la ruta
        private bool SalirSala;
        private bool SalirComedor;
        //Variables utiles
        private bool puedeVerTrompi;
        private float anguloDeBusqueda = 120; //angulo de rango de busqueda trompi. Este angulo debe ser el doble al radio de efecto de HeadAim (de -60 a 60, osea 120 para esta funcionalidad)
        private GameObject Jugador;
        //Cambios de estado
        private float radioBusqueda = 40f;
        //LayerMasks
        public LayerMask targetMask;
        public LayerMask obstructionMask;
        public enum State
        {
            Tranquilo,
            Enfadado,
            Ansioso,
            Catarsis
        }
        //Control estado
        private State state;
        private State LastState;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<AICharacter>();
            agent.updatePosition = true;
            agent.updateRotation = false;
            Jugador = SceneController.instance.jugador;
            state = State.Tranquilo;
            LastState = State.Tranquilo;
            StartCoroutine("FSM");
            SalirSala = false;
            SalirComedor = false;

            if (SceneController.instance.getCurrentSceneName() == "Sala")
            {
                inicioRuta = 3;
            }else if (SceneController.instance.getCurrentSceneName() == "Comedor")
            {
                inicioRuta = 5;
            }else if (SceneController.instance.getCurrentSceneName() == "Cocina")
            {
                inicioRuta = 0;
            }
        }
        //Cambia de estados según las variables de cambio de estado y la escena en la que se encuentre
        private void Update()
        {
            if (SceneController.instance.getCurrentSceneName() == "Sala")
            {
                if (SceneTriggerController.instance.findTriggerByName("megafono").recolectado)
                {
                    SalirSala = true;
                }
            }
            else if (SceneController.instance.getCurrentSceneName() == "Comedor")
            {
                if (SceneTriggerController.instance.findTriggerByName("megafono").recolectado)
                {
                    SalirComedor = true;
                }
            }
            if (!puedeVerTrompi)
            {
                if(LastState == State.Enfadado)
                {
                    state = State.Ansioso;
                    LastState = State.Ansioso;
                }
                else if(LastState == State.Catarsis)
                {
                    state = State.Tranquilo;
                    LastState = State.Tranquilo;
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
                        Pasear(false);
                        break;
                    case State.Enfadado:
                        PerseguirTrompi();
                        break;
                    case State.Ansioso:
                        Pasear(true);
                        break;
                }
                yield return null;
            }
        }

        void Pasear(bool ansioso)//Move(Vector3 move, bool enojado, bool ansioso, bool golpearArriba, bool golpearAbajo)
        {   
            if(RigController.instance != null)
            {
                RigController.instance.DejarDeMirar();
            }
            agent.SetDestination( SceneIAController.instance.targets[targetIndex].transform.position );
            agent.speed = velocidadDePaseo;
            velocidadDePaseo = character.Move(agent.desiredVelocity, false, ansioso, false, false);
            //Ruta con NavMesh
            if (Vector3.Distance(transform.position, SceneIAController.instance.targets[targetIndex].transform.position) > 2)
            {
                agent.SetDestination( SceneIAController.instance.targets[targetIndex].transform.position );
                velocidadDePaseo = character.Move(agent.desiredVelocity, false, ansioso, false, false);
            }else if (Vector3.Distance(transform.position, SceneIAController.instance.targets[targetIndex].transform.position) <= 2)
            { 
                targetIndex += 1;
                if (ansioso)
                {
                    character.PlayAnsiedad();
                }
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
      
        void PerseguirTrompi()//Move(Vector3 move, bool enojado, bool ansioso, bool golpearArriba, bool golpearAbajo)
        {
            agent.speed = chaseSpeed;
            chaseSpeed = character.Move(agent.desiredVelocity, true, false, false, false);
            agent.SetDestination(Jugador.transform.position);
            RigController.instance.Mirar(Jugador.transform);
            if (Vector3.Distance(Hombro.transform.position, Jugador.transform.position) <= radioGolpe)
            {
                if ((Jugador.transform.position.y - transform.position.y)  >= alturagolpe)
                {
                    character.Move(agent.desiredVelocity, true, false, true, false) ;
                }
                else
                {
                    character.Move(agent.desiredVelocity, true, false, false, true);
                }
                LastState = State.Catarsis;
            }
        }

        /*
         * Si esta traquilo y ve a trompi pasa a estado enfadado. 
         * Si trompi escapa (raycast pega en un objeto "obstructionMask") pasa a estar en estado ansioso (Validacion en update)
         * Si por otro lado, Distimia lanza un golpe a trompi, así no le dé, dejará en la variable LastState el estado Catarsis
         * El estado catarsis permite que si trompi escapa esta vez Distimia volverá a estar en estado Tranquilo, repitiendo asi el ciclo.
         */
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
                            if (LastState == State.Tranquilo)
                            {
                                state = State.Enfadado;
                                LastState = State.Enfadado;
                                anguloDeBusqueda = 360f;
                            }
                            else if(LastState == State.Ansioso)
                            {
                                state = State.Enfadado;
                                LastState = State.Enfadado;
                                anguloDeBusqueda = 120f;
                            }
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

