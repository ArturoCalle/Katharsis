using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData
{
    public string[] nombre_recolectable;
    public string nombre_escena;
    public string last_checkpoint;

    public SaveData()
    {
        nombre_escena = SceneController.instance.getCurrentScene().name;
        nombre_recolectable = recolectablesRecogidos();
    }

    private string[] recolectablesRecogidos()
    {
        string[] nombres = new string[Inventario.instance.inventario.Count];
        int i = 0;
        foreach (Recolectable recolectable in Inventario.instance.inventario)
        {
            nombres[i] = recolectable.nombre;
            i++;
        }
        return nombres;
    }
}
