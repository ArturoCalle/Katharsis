using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public float altura = 1.75f;
    float MaxInclinacion = 90;
    [Range(0, 4)]
    public float velocidad = 5;
    float xrot, yrot = 15, distancia = 6;
    PlayerControls player;
    public Transform inclinacion;
    Camera mainCam;

    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        mainCam = Camera.main;

        transform.position = player.transform.position + Vector3.up * altura;
        transform.rotation = player.transform.rotation;

        inclinacion.eulerAngles = new Vector3(yrot, transform.eulerAngles.y, transform.eulerAngles.z);

        mainCam.transform.position += inclinacion.forward * -distancia;
    }
    void Update()
    {
            xrot += Input.GetAxis("Mouse X") * velocidad;
            yrot -= Input.GetAxis("Mouse Y") * velocidad;
            yrot = Mathf.Clamp(yrot, -MaxInclinacion, MaxInclinacion);
    }

    void LateUpdate()
    {
        cameraTransforms();
    }

    void cameraTransforms()
    {
        
        //xrot = player.transform.eulerAngles.y;

        transform.position = player.transform.position + Vector3.up * altura;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xrot, transform.eulerAngles.z);
        inclinacion.eulerAngles = new Vector3(yrot, inclinacion.eulerAngles.y, inclinacion.eulerAngles.z);

        mainCam.transform.position = transform.position + inclinacion.forward * -distancia;

    }
}
