using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public List<Recolectable> inventario = new List<Recolectable>();
    public GameObject prefab;

    List<Boton> items = new List<Boton>();

    // Start is called before the first frame update
    void Start()
    {
        crearInventario();
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

    public void cargarInventario()
    {

    }

    public void guardarInventario()
    {

    }
}
