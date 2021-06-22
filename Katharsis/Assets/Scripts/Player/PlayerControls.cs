using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Controls controls;
    Vector2 inputs;
    float rotation;
    public float baseSpeed = 4, rotateSpeed = 0.5f;

    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        Locomotion();
    }

    void Locomotion()
    {
        Vector2 inputNormalized = inputs;
        Vector3 CharRotation = transform.eulerAngles + new Vector3(0, rotation * rotateSpeed, 0);
        Vector3 velocity = transform.forward * inputNormalized.y * baseSpeed * Time.deltaTime;
        transform.eulerAngles = CharRotation;
        controller.Move(velocity);
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

        if (Input.GetKey(controls.rotateleft))
        {
            if (Input.GetKey(controls.rotateright))
                rotation = 0;
            else
                rotation = -1;
        }

        if (!Input.GetKey(controls.rotateright) && !Input.GetKey(controls.rotateleft))
            rotation = 0;
    }

    
}
