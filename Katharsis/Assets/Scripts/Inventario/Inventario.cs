using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventario
{
    private List<Recolectable> recolectables;
    private int numeroTotalNotas= 15;
    public Inventario()
    {
        recolectables = new List<Recolectable>();
        for (int i = 0; i< numeroTotalNotas; i++)
        {
            Recolectable nuevo = new Recolectable();
            recolectables.Add(nuevo);
        }
    }
    public void agregarRecolectable(Recolectable nuevo)
    {
        recolectables[nuevo.getNumNota()]= nuevo;
        Recolectable r = recolectables[nuevo.getNumNota()];
        
    }
    public List<Recolectable> getRecolectables()
    {
        return recolectables;
    }
}
