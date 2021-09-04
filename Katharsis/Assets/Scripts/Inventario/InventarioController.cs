using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioController : MonoBehaviour
{
    public static InventarioController instance;
    private Inventario inventario;
    void Start()
    {
        instance = this;
        inventario = new Inventario();
    }
    
    void Update()
    {
        
    }

    public void agregarNota(string nombre, string escena, char tipo, bool recolectado, int numNota)
    {
        Recolectable nuevo = new Recolectable(nombre, escena, tipo, recolectado, numNota);
        inventario.agregarRecolectable(nuevo);
        SceneController.instance.GuardarPartida();
    }

    public List<Recolectable> getRecolectables()
    {
        return inventario.getRecolectables();

    }
}
