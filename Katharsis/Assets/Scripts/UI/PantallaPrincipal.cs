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
    public ScreenFader fader;
    bool fadein;

    int seleccion;
    public bool locked;
    List<Boton> botones = new List<Boton>();
    // Start is called before the first frame update
    void Start()
    {
        fadein = false;
        instance = this;
        botones.Add(nuevoJuego.GetComponent<Boton>());
        botones.Add(cargarJuego.GetComponent<Boton>());
        botones.Add(opciones.GetComponent<Boton>());
        botones.Add(salir.GetComponent<Boton>());

        nuevoJuego.GetComponent<Boton>().actualizarTexto("Nueva Partida");
        cargarJuego.GetComponent<Boton>().actualizarTexto("Cargar Partida");
        opciones.GetComponent<Boton>().actualizarTexto("Creditos");
        salir.GetComponent<Boton>().actualizarTexto("Salir");

        seleccion = 0;
        locked = false;

       
    }

    // Update is called once per frame
    void Update()
    {
        if(!fadein)
        {
            fadein = fader.fadeIn();
        }
        else
        {
            mostrarSeleccion();
            getInput();
        }

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
            if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
            {
          
                cambiarSeleccion(-1);
            
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
           
                 cambiarSeleccion(1);
            
            }
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
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
        
        switch (seleccion)
        {
            case 0:
                SceneController.instance.nuevaPartida();
                StartCoroutine(fader.fadeOutCorutine());
                break;
            case 1:
                SceneController.instance.CargarPartida();
                StartCoroutine(fader.fadeOutCorutine());

                break;
            case 2:
                SceneController.instance.cambiarEscena("Creditos");
                StartCoroutine(fader.fadeOutCorutine());
                break;
            case 3:
                Application.Quit();
                break;

        }
    }
}
