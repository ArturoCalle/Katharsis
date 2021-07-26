using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Controls controls;
    public GameObject escalar;

    //controladores de inputs
    Vector3 inputs;
    float rotation;
    bool escalando;

    //velocidades
    float baseSpeed = 10, rotateSpeed = 2f;
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
        jumpDirection = (transform.forward * inputs.z).normalized;
        jumpSpeed = baseSpeed;
        velocityY = Mathf.Sqrt(-gravity * jumpHeigth);
    }
    void Locomotion()
    {
        Vector3 inputNormalized = inputs;

        //rotation
        Vector3 CharRotation = transform.eulerAngles + new Vector3(0, rotation * rotateSpeed, 0);
        transform.eulerAngles = CharRotation;
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
            velocity = (transform.forward * inputNormalized.z + Vector3.up * velocityY) * baseSpeed;
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
        controller.Move(velocity * Time.deltaTime);

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
        if (Input.GetKey(controls.rotateright))
            rotation = 1*Time.timeScale;

        if (Input.GetKey(controls.rotateleft))
        {
            if (Input.GetKey(controls.rotateright))
                rotation = 0 * Time.timeScale;
            else
                rotation = -1 * Time.timeScale;
        }

        if (!Input.GetKey(controls.rotateright) && !Input.GetKey(controls.rotateleft))
            rotation = 0;

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
            UIManager.instance.panelLateral.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.panelLateral.SetActive(true);
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    
}
