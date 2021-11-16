using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Controls controls;
    public GameObject escalar;
    public GameObject groundCheck;
    public Transform cam;
    public bool isGrounded;

    //controladores de inputs
    private Vector3 inputs;
    private Vector3 direction;
    private Vector3 movDir;

    //escalar
    private bool escalando = false;
    private bool colision = false;
    private bool corner = false;
    private Escalar esc;
    private bool DoingCorner = false;
    public Vector3 pos = Vector3.zero;
    private Vector3 moveDir;

    //velocidades
    private float baseSpeed = 10f, rotateSpeed = 0.1f, turnSmooth, climbSpeed = 5f;
    private float gravity = -9.81f, terminalVelocity = -25f;
    private Vector3 velocity;

    //jumpng
    private bool jumping, jump; // jump controla el input y jumping controla la accion
    private float jumpHeigth = 5f;

    //referencia a componente
    private CharacterController controller;
    public static PlayerControls instance;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        instance = this;
        esc = escalar.GetComponent<Escalar>();
    }
    void Update()
    {
        checkClimbStatus();
        checkMouse();
        AnimatorController.instance.move(inputs, velocity.y, isGrounded, jumping, escalando, DoingCorner);
    }
    private void FixedUpdate() //el movimiento debe hacerse en late update para mejorar la visualizaci�n de las animaciones
    {
        Locomotion();
    }
    void Locomotion()
    {
        direction = inputs.normalized;
        isGrounded = groundCheck.GetComponent<GroundCheck>().isGrounded();
        movDir = new Vector3();

        //Tocar piso o escalar
        if (isGrounded || escalando)
        {
            StopYvelocity();
        }
        //Hace el movimiento de corner
        if (DoingCorner)
        {
            direction = Vector3.zero;
            DoCorner();
        }
        //Movimiento con inputs
        if (direction.magnitude > 0.1)
        {
            if (escalando)
            {
                Climb();
            }
            else
            {
                if (!DoingCorner)
                {
                    MoveHorizontal();
                }
            }
        }
        //Fall
        if (!controller.isGrounded && velocity.y > terminalVelocity && !escalando && !DoingCorner)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        //Jump
        if (jump && isGrounded && !escalando)
        {
            velocity.y = Mathf.Sqrt(-gravity * jumpHeigth);
            jumping = true;
        }
        //Apply Vertical Velocity
        controller.Move(velocity * Time.deltaTime * Time.timeScale);
    }
    private void MoveHorizontal()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, rotateSpeed);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(movDir.normalized * baseSpeed * Time.deltaTime * Time.timeScale);
    }
    /**
     * recibe los inputs para subir y bajar, tambien se encarga de rotar al jugador hacia el objeto escalable
     * rotar al rededor de la cuerda est� pendiente
     */
    private void Climb()
    {
        if (corner)
        {
            if (inputs.z == 1)
            {
                controller.Move(Vector3.up.normalized * climbSpeed * Time.deltaTime * Time.timeScale);
            }
            else if (inputs.z == -1)
            {
                controller.Move(Vector3.down.normalized * climbSpeed * Time.deltaTime * Time.timeScale);
            }
            if (inputs.x == 1)
            {
                transform.RotateAround(esc.getTarget().transform.position, Vector3.up, 2000 * Time.deltaTime);

            }
            else if (inputs.x == -1)
            {
                transform.RotateAround(esc.getTarget().transform.position, Vector3.up, -2000 * Time.deltaTime);
            }
            RotateTowardsXZ(esc.getTarget().transform);
        }
        else // si esta escalando pero no tiene colision con el objeto corner, comienza a escalar la esquina
        {
            if (!DoingCorner)
            {
                DoingCorner = true;
            }
        }
    }
    /**
     * se encarga de levantar a trompi hasta que el objeto checkgrounded colisione.
     */
    private void DoCorner()
    {
        direction = Vector3.zero;
        if(pos == Vector3.zero)
        {//Se mueve en direcci�n al objeto corner hasta llegar a su posici�n
            pos = this.gameObject.transform.GetChild(2).transform.GetChild(3).position - transform.position;
        }
        if (!isGrounded)
        {
            controller.Move(pos * Time.deltaTime * Time.timeScale);
        }
        else
        {
            escalando = false;
            DoingCorner = false;
            pos = Vector3.zero;
            corner = false;
        }
    }
    public void playArepa()
    {
        AnimatorController.instance.playArepa();
        
    }
    private void StopYvelocity()
    {
        velocity.y = 0;
        if (jumping)
        {
            jumping = false;
        }
    }
    public void getInputs()
    {
        if (!SceneController.instance.pausa)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneController.instance.MenuPausa();
            }
            //Controles hacia adelante, hacia atras, cancelar movimiento y sin movimiento en y
            if (Input.GetKey(controls.forwards))
            {
                inputs.z = 1;
            }

            if (Input.GetKey(controls.backwards))
            {
                if (Input.GetKey(controls.forwards))
                    inputs.z = 0;
                else
                    inputs.z = -1;
            }
            if (!Input.GetKey(controls.forwards) && !Input.GetKey(controls.backwards))
            {
                inputs.z = 0;
            }
            //Controles rotacion derecha, izquierda, cancelar movimiento y sin movimiento en x
            if (Input.GetKey(controls.right))
            {
                inputs.x = 1;
            }

            if (Input.GetKey(controls.left))
            {
                if (Input.GetKey(controls.right))
                {
                    inputs.x = 0;
                }
                else
                {
                    inputs.x = -1;
                }
            }

            if (!Input.GetKey(controls.right) && !Input.GetKey(controls.left))
            {
                inputs.x = 0;
            }
            //Jumping
            jump = Input.GetKey(controls.jump);
        }
    }

    private void checkMouse()
    {
        if (Input.GetKey(controls.climb))
        {
            if (colision)
            {
                escalando = true;
            }
            else
            {
                escalando = false;
            }
        }
        else
        {
            escalando = false;
            if (DoingCorner)
            {
                DoingCorner = false;
                pos = Vector3.zero;
            }
        }
    }
    public void checkClimbStatus()
    {
        colision = esc.isActive();
        corner = esc.isCorner();
    }
    /**
     * gira sobre el plano xz hacia el target recibido como parametro
     */
    private void RotateTowardsXZ(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion temp = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.Euler(0, temp.eulerAngles.y, 0);
        transform.rotation = rotation;
    }
}