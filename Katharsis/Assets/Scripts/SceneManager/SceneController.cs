using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public CheckpointSingle lastCheckpoint;
    public List<CheckpointSingle> checkpoints;

    public GameObject jugador;

    private void Awake()
    {
        instance = this;
    }

    public void cambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void resume()
    {
        UIController.instance.desactivarPaneles();
        freeze(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void pause()
    {            
        freeze(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void freeze(bool congelar)
    {
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
        pause();
        UIController.instance.panelMuerte.SetActive(true);
    }
    public void restartGameFromCheckpoint()
    {
        UIController.instance.desactivarPaneles();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        jugador.transform.position = lastCheckpoint.transform.position;
        resume();
        Debug.Log(jugador.transform.position);
        Debug.Log(lastCheckpoint.transform.position);
    }
    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        lastCheckpoint = checkpointSingle;
    }

    public void MenuPausa()
    {
        pause();
        UIController.instance.pauseScreen.SetActive(true);
    }
}
