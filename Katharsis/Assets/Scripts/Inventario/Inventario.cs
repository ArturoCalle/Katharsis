using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventario
{
    private List<Recolectable> recolectables;
    public Inventario()
    {
        recolectables = new List<Recolectable>();
        for (int i = 0; i< 20; i++)
        {
            Recolectable nuevo = new Recolectable();
            recolectables.Add(nuevo);
        }
    }
    public void agregarRecolectable(Recolectable nuevo)
    {
        recolectables[nuevo.getNumNota() - 1]= nuevo;
        Recolectable r = recolectables[nuevo.getNumNota() - 1];
        Debug.Log(r.getNumNota());
        Debug.Log(r.getNombre());
        Debug.Log(r.getEscena());
    }
    public List<Recolectable> getRecolectables()
    {
        return recolectables;
    }
}
