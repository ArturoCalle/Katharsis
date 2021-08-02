using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuPausa : MonoBehaviour
{
    public GameObject botonReanudar;
    public GameObject botonNotas;
    public GameObject botonOpciones;
    public GameObject botonVolverMenuPrincipal;
   

    List<Boton> botones;
    int seleccion = 0;
    bool locked = false;
    GameObject panelOpciones;

    // Start is called before the first frame update
    void Start()
    {
        panelOpciones = UIController.instance.panelOpciones;

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
        Boton notas = botonNotas.GetComponent<Boton>();
        Boton opciones = botonOpciones.GetComponent<Boton>();
        Boton volver = botonVolverMenuPrincipal.GetComponent<Boton>();
        //inicializa los textos
        reanudar.actualizarTexto("Reanudar Partida");
        notas.actualizarTexto("Notas");
        opciones.actualizarTexto("Opciones");
        volver.actualizarTexto("Volver al Menú Principal");
        //guarda los botones en la lista de botone

        botones.Add(reanudar);
        botones.Add(notas);
        botones.Add(opciones);
        botones.Add(volver);
    }
    public int getSeleccion()
    {
        return seleccion;
    }
    public bool isLocked()
    {
        return locked;
    }
    public void setLock(bool value)
    {
        locked = value;
    }
    public void seleccionar()
    {
        switch (seleccion)
        {
            case 0:
                Debug.Log("reanudar partida");
                PlayerControls.instance.PauseUnpause();
                break;
            case 1:
                Debug.Log("ver notas");
                break;
            case 2:
                Debug.Log("ver opciones");
                PanelOpciones po = panelOpciones.GetComponent<PanelOpciones>();
                panelOpciones.SetActive(true);
                po.reiniciarBotones();
                setLock(true);
                panelOpciones.SetActive(true);
                po.setLock(false);

                break;
            case 3:
                Debug.Log("volver al menú principal");
                PlayerControls.instance.PauseUnpause();
                SceneController.instance.cambiarEscena("Pantalla Principal");

                break;
            default:
                break;
        }
    }
}
