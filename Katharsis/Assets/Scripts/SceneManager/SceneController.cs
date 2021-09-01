using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public CheckpointSingle lastCheckpoint;
    public List<CheckpointSingle> checkpoints;
    public bool pausa;
    void Start()
    {
        instance = this;
        pausa = false;
    }

    public void cambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void pause(string causa)
    {
        if(pausa)
        {
             UIController.instance.desactivarPaneles();
             freeze(false);
             Cursor.visible = false;
             Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            if(causa != "EndGame")
            {
                UIController.instance.pausar();
            }
            else if(causa == "EndGame")
            {
                pausa = true;
            }
            freeze(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void freeze(bool congelar)
    {
        pausa = congelar;
        if (congelar)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void EndGame()
    {
        pause("EndGame");
        UIController.instance.panelMuerte.SetActive(true);
    }
    public void restartGameFromCheckpoint()
    {
        UIController.instance.desactivarPaneles();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        SceneController.instance.lastCheckpoint = checkpointSingle;
    }
}
