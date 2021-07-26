using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public List<Boton> botones = new List<Boton>();
    public Text titulo;

    int maxSelection;
    private void Start()
    {
        Reset();
        
    }
    private void Update()
    {
        
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

}
