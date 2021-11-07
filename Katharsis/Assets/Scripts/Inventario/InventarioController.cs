using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioController : MonoBehaviour
{
    public static InventarioController instance;
    private Inventario inventario;
    /*
     * Carga el inventario cuando entra a una escena jugable
     */
    void Start()
    {
        instance = this;
        inventario = new Inventario();
        if (SceneController.instance.getCurrentSceneName() != "Pantalla Principal")
        {
            SceneController.instance.cargarInventario();
        }
    }
    
    public void vaciarInventario()
    {
        inventario = new Inventario();
    }
    // funcion para cargar una nota nueva en el inventario, recibe los parametros y por dentro crea el recolectable
    public void agregarNota(string nombre, string escena, char tipo, bool recolectado, int numNota)
    {
        Recolectable nuevo = new Recolectable(nombre, escena, tipo, recolectado, numNota);
        inventario.agregarRecolectable(nuevo);
        SceneController.instance.GuardarPartida();
    }
    public void cargarNota(string nombre, string escena, char tipo, bool recolectado, int numNota)
    {
        Recolectable nuevo = new Recolectable(nombre, escena, tipo, recolectado, numNota);
        inventario.agregarRecolectable(nuevo);
    }
    /*
     * Carga las notas recogidas y los triggers al inventario 
     */
    public void cargarInventario(Partida p)
    {
        for (int i = 0; i < p.notasRecogidas.Length; i++)
        {
            if (p.notasRecogidas[i])
            {
                cargarNota(p.nombreNotas[i], p.escenaNotas[i], p.tipoNotas[i], p.notasRecogidas[i], i);
            }
        }
        for(int i = 0; i<p.sceneTriggerNum.Length; i++)
        {
            Recolectable r = new Recolectable(p.sceneTriggerNombre[i], p.sceneTriggerEscena[i], p.sceneTriggerRecolectados[i], p.sceneTriggerNum[i]);
            inventario.agregarTrigger(r);
        }

    }

    public List<Recolectable> getRecolectables()
    {
        return inventario.getRecolectables();

    }
    public Recolectable getRecolectable(int index)
    {
        List<Recolectable> rs = inventario.getRecolectables();
        return rs[index];
    }
    public void agregarTrigger(Recolectable r)
    {
        inventario.agregarTrigger(r);
        SceneController.instance.GuardarPartida();
    }
    public List<Recolectable> getTriggers()
    {
        return inventario.getTriggers();
    }
    public string getTextoNota(int index)
    {
        return inventario.getTextoNota(index);
    }
}
