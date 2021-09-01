using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNotas : MonoBehaviour
{
    public List<Nota> inventario = new List<Nota>();
    public GameObject prefab;
    public GameObject scroll;

    public static PanelNotas instance;

    List<Boton> items = new List<Boton>();
    int seleccion;
    bool locked;
    bool mostrandoNota;

    // Start is called before the first frame update
    void Start()
    {
        mostrandoNota = false;
        crearInventario();
        seleccion = 0;
        locked = true;
        instance = this;
        //TO DO cargar lista de recolectables 
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].isCollected())
            {
                items[i].actualizarTexto(inventario[i].nombre);
            }
            else
            {
                items[i].actualizarTexto("???");
            }
        }
        mostrarSeleccion();
    }

    private void crearInventario()
    {
        GameObject nuevoItem;
        for(int i = 0; i<inventario.Count;i++)
        {
            nuevoItem = (GameObject)Instantiate(prefab, transform);
            Boton actual = nuevoItem.GetComponent<Boton>();
            items.Add(actual);
            
        }
    }
    void mostrarSeleccion()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Boton actual = items[i].getBoton();
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
    public void cambiarSeleccion(int s)
    {
        if (seleccion + s <= -1)
        {
            seleccion = items.Count - 1;
        }
        else if (seleccion + s >= items.Count)
        {
            seleccion = 0;
        }
        else
        {
            seleccion = seleccion + s;
        }
        scroll.GetComponent<ScrollRect>().verticalNormalizedPosition = scrollValue();
    }
    public float scrollValue()
    {

        float value = 1-(float)seleccion/(float)items.Count;
        return value;
    }

    public void seleccionar()
    {
        
        if(inventario[seleccion].isCollected())
        {
            
            inventario[seleccion].mostrarNota();      
            
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
    public int getSeleccion()
    {
        return seleccion;
    }
}
