using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Controls controls;
    //controladores de inputs
    Vector2 inputs;
    float rotation;
    //velocidades
    float baseSpeed = 10, rotateSpeed = 1f;
    float gravity = -30, velocityY = 0, terminalVelocity = -25f;
    Vector3 velocity;
    //jumpng
    bool jumping, jump;
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
            jumping = true;
        jumpDirection = (transform.forward * inputs.y).normalized;
        jumpSpeed = baseSpeed;
        velocityY = Mathf.Sqrt(-gravity * jumpHeigth);
    }
    void Locomotion()
    {
        Vector2 inputNormalized = inputs;

        //rotation
        Vector3 CharRotation = transform.eulerAngles + new Vector3(0, rotation * rotateSpeed, 0);
        transform.eulerAngles = CharRotation;
        //Jump
        if (jump && controller.isGrounded)
            Jump();

        if (!controller.isGrounded && velocityY > terminalVelocity)
            velocityY += gravity * Time.deltaTime;

        //aplicar inputs
        if (!jumping)
            velocity = (transform.forward * inputNormalized.y + Vector3.up * velocityY) * baseSpeed;
        else
            velocity = jumpSpeed * jumpDirection + Vector3.up * velocityY;
        //moviendo controlador
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
            if (jumping)
                jumping = false;
        }
    }
    void getInputs()
    {
        //Controles hacia adelante, hacia atras, cancelar movimiento y sin movimiento en y
        if (Input.GetKey(controls.forwards))
            inputs.y = 1;

        if (Input.GetKey(controls.backwards)){
            if (Input.GetKey(controls.forwards))
                inputs.y = 0;
            else
                inputs.y = -1;
        }

        if (!Input.GetKey(controls.forwards) && !Input.GetKey(controls.backwards))
            inputs.y = 0;

        //Controles rotacion derecha, izquierda, cancelar movimiento y sin movimiento en x
        if (Input.GetKey(controls.rotateright))
            rotation = 1;

        if (Input.GetKey(controls.rotateleft)){
            if (Input.GetKey(controls.rotateright))
                rotation = 0;
            else
                rotation = -1;
        }

        if (!Input.GetKey(controls.rotateright) && !Input.GetKey(controls.rotateleft))
            rotation = 0;

        //Jumping
        if( Input.GetKey(controls.jump))
        {
            Jump();
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
