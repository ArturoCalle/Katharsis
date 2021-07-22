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
    int seleccion = 0;

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        reiniciarBotones();
        mostrarSeleccion();
    }
    void mostrarSeleccion()
    {
        for(int i =0; i<botones.Count;i++)
        {
            Boton actual = botones[i].getBoton();
            if(i == seleccion)
            {
                actual.setActive(true);
            }
            else
            {
                actual.setActive(false);
            }
        }
        
        
    }
    //aumenta o decrece la selecion del menu recibe 1 o -1
    public void cambiarSeleccion(int s)
    {
        if(seleccion + s <=-1)
        {
            seleccion = botones.Count-1;
        }
        else if(seleccion + s >= botones.Count )
        {
            seleccion = 0;
        }
        else
        {
            seleccion = seleccion + s;
        }
    }
    void reiniciarBotones()
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
}
