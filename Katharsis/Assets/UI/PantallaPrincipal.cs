using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaPrincipal : MonoBehaviour
{
    public static PantallaPrincipal instance;
    public GameObject nuevoJuego;
    public GameObject cargarJuego;
    public GameObject opciones;
    public GameObject salir;

    int seleccion;
    public bool locked;
    List<Boton> botones = new List<Boton>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        botones.Add(nuevoJuego.GetComponent<Boton>());
        botones.Add(cargarJuego.GetComponent<Boton>());
        botones.Add(opciones.GetComponent<Boton>());
        botones.Add(salir.GetComponent<Boton>());
        Debug.Log(botones.Count);
        seleccion = 0;
        locked = false;

       
    }

    // Update is called once per frame
    void Update()
    {
        nuevoJuego.GetComponent<Boton>().actualizarTexto("Nueva Partida");
        cargarJuego.GetComponent<Boton>().actualizarTexto("Cargar Partida");
        opciones.GetComponent<Boton>().actualizarTexto("Opciones");
        salir.GetComponent<Boton>().actualizarTexto("Salir");

        mostrarSeleccion();
        getInput();

    }

    void mostrarSeleccion()
    {
        for(int i =0; i<botones.Count; i++)
        {
            if(i == seleccion)
            {
                botones[i].setActive(true);
            }
            else
            {
                botones[i].setActive(false);
            }
        }
    }
    
    void getInput()
    {
        if(!locked)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
          
                cambiarSeleccion(-1);
            
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
           
                 cambiarSeleccion(1);
            
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                seleccionar();

            }
        }

    }
    void cambiarSeleccion(int s)
    {
        if (seleccion + s <= -1)
        {
            seleccion = botones.Count - 1;
        }
        else if (seleccion + s >= botones.Count)
        {
            seleccion = 0;
        }
        else
        {
            seleccion = seleccion + s;
        }
    }
    void seleccionar()
    {
        switch(seleccion)
        {
            case 0:
                SceneController.instance.cambiarEscena("Cocina");
                //TO DO crear nueva partida
                break;
            case 1:
                //TO DO crear cargar partida
                break;
            case 2:
                //TO DO crear opciones
                break;
            case 3:
                Application.Quit();
                break;

        }
    }
}
