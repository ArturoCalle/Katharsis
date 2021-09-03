using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Persistencia: MonoBehaviour
{
    public void savePartida(string name, )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/partida" + name;
        FileStream stream = new FileStream(path, FileMode.Create);

        Partida partida = new Partida();


    }

    public void cargarCheckpoint()
    {
        InventarioController.instance.
    }
    public void cargarInventario()
    {

    }
}
