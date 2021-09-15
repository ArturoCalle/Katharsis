using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Persistencia
{
    public static void GuardarPartida(string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/partida" + name;
        FileStream stream = new FileStream(path, FileMode.Create);

        Partida partida = new Partida(InventarioController.instance.getRecolectables(), SceneController.instance.ultimoCheckPoint, SceneController.instance.getCurrentSceneName(), AICharacterControl.instance.getLastPersistencia());
        formatter.Serialize(stream, partida);
        stream.Close();
    }  

    public static Partida CargarPartida(string name)
    {
        string path = Application.persistentDataPath + "/partida" + name;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Partida partida = formatter.Deserialize(stream) as Partida;
            stream.Close();

            return partida;
        }
        else
        {
            Debug.LogError("save file not found in" + path);
            return null;
        }
    }
}
