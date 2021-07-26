using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Controls controls;
    public GameObject escalar;

    //controladores de inputs
    Vector3 inputs;
    bool escalando;

    //velocidades
    float baseSpeed = 10, rotateSpeed = 0.1f, turnSmooth;
    float gravity = -30, velocityY = 0, terminalVelocity = -25f;
    Vector3 velocity;

    //jumpng
    bool jumping, jump; // jump controla el input y jumping controla la accion
    float jumpSpeed, jumpHeigth = 3;
    Vector3 jumpDirection;

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
        jumpDirection = inputs.normalized;
        jumpSpeed = baseSpeed;
        velocityY = Mathf.Sqrt(-gravity * jumpHeigth);
    }
    void Locomotion()
    {
        Vector3 inputNormalized = inputs.normalized; //llamese direccion
        float targetAngle = Mathf.Atan2(inputNormalized.x, inputNormalized.z) * Mathf.Rad2Deg; //el angulo entre los inputs, 45 grados por ejemplo si se presiona w y d
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, rotateSpeed); //funcion para suavisar el angulo, necesita una velocidad y una variable (turnSmooth) que necesita la funcion
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Jump
        if (jump && controller.isGrounded)
        {
            Jump();
        }

        if (!controller.isGrounded && velocityY > terminalVelocity && !escalando)
        {
            velocityY += gravity * Time.deltaTime;
        }

        //aplicar inputs
        if (!jumping)
        {
            velocity = (inputNormalized +  Vector3.up * velocityY) * baseSpeed;
        }
        else
        {
            velocity = jumpSpeed * jumpDirection + Vector3.up * velocityY;
        }
         if(escalando)
        {
            velocity = (transform.up * inputNormalized.y) * baseSpeed;       
        }

        //moviendo controlador
        controller.Move(velocity * Time.deltaTime * Time.timeScale);

        if (controller.isGrounded)
        {
            velocityY = 0;
            if (jumping)
                jumping = false;
        }
        AnimatorController.instance.move(inputs, jump);
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
