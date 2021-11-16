using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Esta clase es la entidad de datos que administra los objetos recolectables que son guardados en persistencia.
 */
public class Inventario
{
    public TextosNotas textosNotas; 
    private List<Recolectable> recolectables;
    private List<Recolectable> triggers;
    /**
     * constructor que inicializa las notas con sus respectivos texos
     */
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
    /**
     * Agrega un un nuevo recolectable a la lista
     */
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
