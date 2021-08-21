using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    void Start()
    {
        instance = this;
    }

    public void cambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
    public void reanudarPartida(string scene)
    {
        SceneManager.LoadScene(scene);

        PlayerControls.instance.SpawnAt(CheckpointGeneral.instance.lastCheckpoint.transform);
    }

    public Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }
}
