using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventario
{
    public TextosNotas textosNotas;
    private List<Recolectable> recolectables;
    private List<Recolectable> triggers;
    public Inventario()
    {
        textosNotas = new TextosNotas();
        triggers = new List<Recolectable>();
        recolectables = new List<Recolectable>();
        for (int i = 0; i< textosNotas.textos.Length; i++)
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
    public void agregarTrigger(Recolectable nuevo)
    {
        triggers.Add(nuevo);
    }
    public List<Recolectable> getTriggers()
    {
        return triggers;
    }
    public string getTextoNota(int index) 
    {
        return textosNotas.textos[index];
    }
}
