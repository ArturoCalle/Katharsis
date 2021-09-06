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
        NotasSceneController.instance.verificarNotas();
        SceneController.instance.GuardarPartida();
    }

    public void cargarInventario(bool[] recolectados)
    {
        for(int i =0; i<recolectados.Length;i++)
        {
            if(recolectados[i])
            {
                Debug.Log("nota: "+ i + " recogida");
            }
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
}
