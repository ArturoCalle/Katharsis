using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuPausa : MonoBehaviour
{
    public GameObject botonReanudar;
    public GameObject botonOpciones;
    public GameObject botonVolverMenuPrincipal;

    List<Boton> botones;
    int seleccion = 1;

    // Start is called before the first frame update
    void Start()
    {
        botones = new List<Boton>();
        Boton reanudar = botonReanudar.GetComponent<Boton>();
        Boton opciones = botonOpciones.GetComponent<Boton>();
        Boton volver = botonVolverMenuPrincipal.GetComponent<Boton>();
        //inicializa los textos
        reanudar.actualizarTexto("Reanudar Partida");
        opciones.actualizarTexto("Opciones");
        volver.actualizarTexto("Volver al Menú Principal");
        //guarda los botones en la lista de botone

        botones.Add(reanudar);
        botones.Add(opciones);
        botones.Add(volver);

    }

    // Update is called once per frame
    void Update()
    {
        mostrarSeleccion();
    }
    void mostrarSeleccion()
    {
        Boton actual = botones[seleccion].getBoton();
        actual.setActive(true);
        
    }
    //aumenta o decrece la selecion del menu recibe 1 o -1
    void cambiarSeleccion(int s)
    {
        if(seleccion + s ==-1)
        {
            seleccion = botones.Count;
        }
        else if(seleccion + s >botones.Count )
        {
            seleccion = 0;
        }
        else
        {
            seleccion = seleccion + s;
        }
    }
}
