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
    Vector3 inputs;
    Vector3 direction;
    //escalar
    bool escalando = false;
    bool colision = false;
    public bool corner = false;
    private Escalar esc;
    //velocidades
    float baseSpeed = 10f, rotateSpeed = 0.1f, turnSmooth, climbSpeed = 5f;
    float gravity = -9.81f, terminalVelocity = -25f;
    Vector3 velocity;
    //jumpng
    bool jumping, jump; // jump controla el input y jumping controla la accion
    float jumpHeigth = 5f;
    //referencia a componente
    CharacterController controller;
    public static PlayerControls instance;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        instance = this;
        esc = escalar.GetComponent<Escalar>();
    }

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        checkClimbStatus();
        checkMouse();
        getInputs();
        Locomotion();
    }

    void Locomotion()
    {
        direction = inputs.normalized;
        isGrounded = groundCheck.GetComponent<GroundCheck>().isGrounded();
        Vector3 movDir = new Vector3();

        if (isGrounded || escalando)
        {
            velocity.y = 0;
            if (jumping)
            {
                jumping = false;
            }
        }

        if (direction.magnitude > 0.1)
        {
            if (escalando)
            {
                if (inputs.z == 1)
                {
                    movDir = Vector3.up;
                }
                else if (inputs.z == -1)
                {
                    movDir = Vector3.down;
                }
                controller.Move(movDir.normalized * climbSpeed * Time.deltaTime * Time.timeScale);
            }
            else
            {
                //target angle is the angle it will move towards with the keyboard inputs and mouse
                //angle smooth the rotation of the character
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, rotateSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                //x, z movemnt
                movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(movDir.normalized * baseSpeed * Time.deltaTime * Time.timeScale);
            }
        }
        //fall
        if (!controller.isGrounded && velocity.y > terminalVelocity && !escalando)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //Jump
        if (jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-gravity * jumpHeigth);
            jumping = true;
        }
        //apply gravity and jump motion to controller
        controller.Move(velocity * Time.deltaTime * Time.timeScale);
        //change animator parameters in animator controller instance
        AnimatorController.instance.move(inputs, velocity.y, isGrounded, jumping, escalando, corner);
    }
    void getInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
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
    void checkMouse()
    {
        if (Input.GetKey(controls.climb))
        {
            if (colision)
            {
                escalando = true;
                RotateTowardsXZ(esc.getTarget());
            }
            else
            {
                escalando = false;
            }
        }
        else
        {
            escalando = false;
            corner = false;
        }
        
    }
    public void PauseUnpause()
    {
        if(UIController.instance.pauseScreen.activeInHierarchy)
        {
            UIController.instance.desactivarPaneles();
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIController.instance.pausar();
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void freeze(bool congelar)
    {
        if(congelar)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void checkClimbStatus()
    {
        colision = esc.isActive(); 
        corner = esc.isCorner();
    }

    private void RotateTowardsXZ(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion temp = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.Euler(0, temp.eulerAngles.y, 0);
        transform.rotation = rotation;
    }


}
