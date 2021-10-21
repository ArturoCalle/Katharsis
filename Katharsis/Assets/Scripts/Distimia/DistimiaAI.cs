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
        public GameObject cabeza;
        //Ruta
        private int targetIndex = 0;
        private int inicioRuta;
        private int finRuta;
        private float velocidadDePaseo = 0f;
        //Persecución Trompi
        private float chaseSpeed = 0f;
        //Distimia se destruye cuando estas estan en true y termina la ruta
        private bool SalirSala;
        private bool SalirComedor;
        //Variables utiles
        private bool mirarTrompi;
        private bool puedeVerTrompi;
        private float anguloDeBusqueda = 120; //angulo de rango de busqueda trompi. Este angulo debe ser el doble al radio de efecto de HeadAim (de -60 a 60, osea 120 para esta funcionalidad)
        private GameObject Jugador;
        //Variables de configuración puño y patada
        public float LimiteGolpeAltoyBajo;
        //Cambios de estado
        private float radioBusqueda = 20f;
        private float radioGolpe = 10f;
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
            cabeza = transform.GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(0).gameObject;
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
            if (!puedeVerTrompi)
            {
                if(LastState == State.Enfadado)
                {
                    state = State.Ansioso;
                }
                else
                {
                    state = State.Tranquilo;
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

        void Pasear(bool ansioso)//Al movimiento de trompi se le envian dos variables de estado, en esta funcion, Distimia puede estar tranquilo o ansioso
        {   
            if(RigController.instance != null)
            {
                RigController.instance.DejarDeMirar();
            }
            agent.SetDestination( SceneIAController.instance.targets[targetIndex].transform.position );
            agent.speed = velocidadDePaseo;
            velocidadDePaseo = character.Move(agent.desiredVelocity, false, false, false);
            //Ruta con NavMesh
            if (Vector3.Distance(transform.position, SceneIAController.instance.targets[targetIndex].transform.position) > 2)
            {
                agent.SetDestination(SceneIAController.instance.targets[targetIndex].transform.position);
                velocidadDePaseo = character.Move(agent.desiredVelocity, false, false, false);
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
      
        void PerseguirTrompi()//Al movimiento de trompi se le envian dos variables de estado, en esta funcion, la variable de enojado esta en true
        {
            agent.speed = chaseSpeed;
            chaseSpeed = character.Move(agent.desiredVelocity, true, false, false);
            agent.SetDestination(Jugador.transform.position);
            RigController.instance.Mirar(Jugador.transform);
            if(Vector3.Distance(transform.position, Jugador.transform.position) <= radioGolpe)
            {
                if (Jugador.transform.position.y > LimiteGolpeAltoyBajo)
                {
                    chaseSpeed = character.Move(agent.desiredVelocity, true, true, false);
                }
                else
                {
                    chaseSpeed = character.Move(agent.desiredVelocity, true, false, true);
                }
            }
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
                            LastState = State.Enfadado;
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

