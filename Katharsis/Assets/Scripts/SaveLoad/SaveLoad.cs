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
        string[] nombres = new string[PanelNotas.instance.inventario.Count];
        int i = 0;
        foreach (Nota recolectable in PanelNotas.instance.inventario)
        {
            nombres[i] = recolectable.notaUI.nombre;
            i++;
        }
        return nombres;
    }
}
