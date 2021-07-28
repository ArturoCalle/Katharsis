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
    bool escalando;

    //velocidades
    float baseSpeed = 10f, rotateSpeed = 0.1f, turnSmooth;
    float gravity = -9.81f, terminalVelocity = -25f;
    Vector3 movDir;
    Vector3 velocity;

    //jumpng
    bool jumping, jump; // jump controla el input y jumping controla la accion
    float jumpHeigth = 5;

    //Direccion
    Vector3 direction;

    //referencia a componente
    CharacterController controller;
    public static PlayerControls instance;


    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        instance = this;
    }

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        getInputs();
        Locomotion();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    void Jump()
    {
        if (!jumping)
        {
            jumping = true;
        }
        velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeigth);
    }
    void Locomotion()
    {
        direction = inputs.normalized;
        isGrounded = groundCheck.GetComponent<GroundCheck>().isGrounded();
        
        //moviendo controlador en eje x, z
        if (direction.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, rotateSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(movDir.normalized * baseSpeed * Time.deltaTime * Time.timeScale);
        }

        //Jump
        if (jump && isGrounded)
        {
            Jump();
        }
        //fall
        if (!controller.isGrounded && velocity.y > terminalVelocity && !escalando)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //apply inputs
        if (!jumping)
        {
            velocity = (movDir + Vector3.up * velocity.y) * baseSpeed;
        }
        else
        {
            velocity = baseSpeed * movDir + Vector3.up * velocity.y;
        }
         if(escalando)
        {
            velocity = (Vector3.up * direction.y) * baseSpeed;       
        }


        if (isGrounded)
        {
            velocity.y = 0;
            if (jumping)
                jumping = false;
        }
        controller.Move(velocity * Time.deltaTime * Time.timeScale);
        AnimatorController.instance.move(inputs, velocity.y, isGrounded, jump);
    }
    void getInputs()
    {
        //Controles hacia adelante, hacia atras, cancelar movimiento y sin movimiento en y
        if (Input.GetKey(controls.forwards))
        {
            checkMouse();
            if(escalando)
            {
                inputs.y = 1;
            }
            else
            {
                inputs.z = 1;
            }
        }

        if (Input.GetKey(controls.backwards))
        {
            if (Input.GetKey(controls.forwards))
                inputs.z = 0;
            else
                inputs.z = -1;
        }

        if (!Input.GetKey(controls.forwards) && !Input.GetKey(controls.backwards))
            inputs.z = 0;

        //Controles rotacion derecha, izquierda, cancelar movimiento y sin movimiento en x
        if (Input.GetKey(controls.right))
            inputs.x = 1;

        if (Input.GetKey(controls.left))
        {
            if (Input.GetKey(controls.right))
                inputs.x = 0;
            else
                inputs.x = -1;
        }

        if (!Input.GetKey(controls.right) && !Input.GetKey(controls.left))
            inputs.x = 0;

        //verifica el estado de los colisionadores de escaladao adelante y atras que en combinacion con la tecla click izquierdo permiten activar el escalado de objetos
        
        //Al soltar el boton de agarre vuelve a aplicar gravedad al jugador para que vuelva a caer
        if(Input.GetMouseButtonUp(0))
        {
            escalando = false;
        }
        //Jumping
        jump = Input.GetKey(controls.jump);
    }
    void checkMouse()
    {
        if (Input.GetMouseButton(0))
        {
            //recupera el script del gameObject escalar para validar el estado de la colision
            if (escalar.GetComponent<Escalar>().isActive())
            {
                escalando = true;
            }
            else
            {
                escalando = false;
            }

        }
    }
    public void PauseUnpause()
    {
        if(UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    
}
