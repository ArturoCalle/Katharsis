using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNotas : MonoBehaviour
{
    private List<Recolectable> inventario = new List<Recolectable>();
    public GameObject notaUiPrefab;
    public GameObject botonPrefab;
    public GameObject scroll;

    public static PanelNotas instance;

    List<Boton> items = new List<Boton>();
    int seleccion;
    bool locked;
    public bool mostrandoNota;

    // Start is called before the first frame update
    void Start()
    {
        
        crearInventario();

        mostrandoNota = false;
        seleccion = 0;
        locked = false;
        instance = this;
        //TO DO cargar lista de recolectables 
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].getRecolectado())
            {
                items[i].actualizarTexto(inventario[i].getNombre());
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
        cargarInventario();
        GameObject nuevoItem;
        for(int i = 0; i<inventario.Count;i++)
        {
            nuevoItem = (GameObject)Instantiate(botonPrefab, transform);
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
        Debug.Log(scrollValue());
    }
    public float scrollValue()
    {

        float value = 1-((float)seleccion/((float)items.Count-1));
        return value;
    }

    public void seleccionar()
    {
        cargarInventario();
        if(inventario[seleccion].getRecolectado())
        {
            UIController.instance.NotaUI.SetActive(true);
            UIController.instance.NotaUI.GetComponent<NotaUIMenu>().actualizarNota(inventario[seleccion]);

            mostrandoNota = true;
            locked = true;
            
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
    public void cargarInventario()
    {
        inventario = InventarioController.instance.getRecolectables();
    }
}
