using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour
{
    public GameObject botonVideo;
    public GameObject botonAudio;
    public GameObject botonControles;
    public GameObject botonAtras;

    List<Boton> botones = new List<Boton>();
    public Text titulo;

    int seleccion;
    bool locked;
    private void Start()
    {
        reiniciarBotones();
        seleccion = 0;
        locked = false;
        
    }
    private void Update()
    {
        mostrarSeleccion();
    }
    public void reiniciarBotones()
    {
        cambiarTitulo("Opciones ");
        botones = new List<Boton>();
        Boton video = botonVideo.GetComponent<Boton>();
        Boton audio = botonAudio.GetComponent<Boton>();
        Boton controles = botonControles.GetComponent<Boton>();
        Boton volver = botonAtras.GetComponent<Boton>();
        //inicializa los textos
        video.actualizarTexto("Video");
        audio.actualizarTexto("Audio");
        controles.actualizarTexto("Controles");
        volver.actualizarTexto("Atras");
        //guarda los botones en la lista de botones

        botones.Add(video);
        botones.Add(audio);
        botones.Add(controles);
        botones.Add(volver);
    }
    void cambiarTitulo(string nuevoTitulo)
    {
        titulo.text = nuevoTitulo;
    }
    public void cambiarSeleccion(int s)
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
    void mostrarSeleccion()
    {
        for (int i = 0; i < botones.Count; i++)
        {
            Boton actual = botones[i].getBoton();
            if (i == seleccion)
            {
                actual.setActive(true);
            }
            else
            {
                actual.setActive(false);
            }
        }
    }
    public bool isLocked()
    {
        return locked;
    }
    public void setLock(bool value)
    {
        locked = value;
    }
    public void seleccionar(menuPausa mp)
    {
        switch(seleccion)
        {
            case 0:
                setLock(true);
                mp.setLock(false);
                //TO DO video 
                break;
            case 1:
                setLock(true);
                mp.setLock(false);
                //TO DO audio 
                break;
            case 2:
                setLock(true);
                mp.setLock(false);
                //TO DO controles 
                break;
            case 3:
                //TO DO atras 
                setLock(true);
                mp.setLock(false);
                UIController.instance.hidePanel("opciones");
                break;
        }
    }
}
