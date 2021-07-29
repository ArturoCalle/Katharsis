using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour
{
    public List<Boton> botones = new List<Boton>();
    public Text titulo;

    int seleccion;
    int maxSelection;
    bool locked;
    private void Start()
    {
        seleccion = 0;
        locked = true;
        Reset();
        
    }
    private void Update()
    {
        mostrarSeleccion();
    }
    public void Reset()
    {

        cambiarTitulo(" ");
        maxSelection = 0;
        for(int i =0; i< botones.Count;i++)
        {
            botones[i].actualizarTexto(" ");
        }
    }
    public void agregarBoton(string texto)
    {
        maxSelection++;
        botones[maxSelection - 1].actualizarTexto(texto);
    }
    public void cambiarTitulo(string nuevoTitulo)
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

}
