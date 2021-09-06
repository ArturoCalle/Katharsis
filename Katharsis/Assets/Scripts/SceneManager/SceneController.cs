using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject prefabJugador;
    public CheckpointSingle ultimoCheckPoint;
    private GameObject jugador;
    public bool pausa;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name!= "Pantalla Principal")
        {

            prefabJugador.transform.position = ultimoCheckPoint.transform.position;
            jugador = Instantiate(prefabJugador);
        }
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

    public void resume()
    {
        UIController.instance.desactivarPaneles();
        freeze(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void pause()
    {
        UIController.instance.pausar();
        freeze(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        freeze(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UIController.instance.panelMuerte.SetActive(true);
    }
    public void restartGameFromCheckpoint()
    {
        UIController.instance.desactivarPaneles();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        jugador.transform.position = ultimoCheckPoint.transform.position;
        resume();
        Debug.Log(jugador.transform.position);
        Debug.Log(ultimoCheckPoint.transform.position);
    }

    public void MenuPausa()
    {
        pause();
        UIController.instance.pauseScreen.SetActive(true);
    }
    public void GuardarPartida()
    {
        Persistencia.GuardarPartida("partida unica");
    }
    public void CargarPartida()
    {
        Partida partida = Persistencia.CargarPartida("partida unica");
        Debug.Log("notas: " + partida.notasRecogidas.Length);
        InventarioController.instance.cargarInventario(partida.notasRecogidas);
        SceneManager.LoadScene(partida.escena);
    }
    public void nuevaPartida()
    {
        cambiarEscena("sala");
    }

    public string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
