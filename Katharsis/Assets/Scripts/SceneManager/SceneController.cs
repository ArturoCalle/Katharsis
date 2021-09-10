using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject prefabJugador;
    public GameObject prefabDistimia;
    public CheckpointSingle ultimoCheckPoint;
    private GameObject jugador;
    private GameObject distimia;
    public bool pausa;
    Vector3 DistimiaPos;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name!= "Pantalla Principal")
        {
            prefabJugador.transform.position = ultimoCheckPoint.transform.position;
            jugador = Instantiate(prefabJugador);
            prefabDistimia.transform.position = DistimiaPos;
            distimia = Instantiate(prefabDistimia);
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
        Debug.Log("guardando....");
        Persistencia.GuardarPartida("partida unica");
    }
    public void CargarPartida()
    {
        Partida partida = Persistencia.CargarPartida("partida unica");
        InventarioController.instance.cargarInventario(partida);
        TargetController.instance.cargarLastTarget(partida.targetAI);
        DistimiaPos.x = partida.distimia[0];
        DistimiaPos.y = partida.distimia[1];
        DistimiaPos.z = partida.distimia[2];
        SceneManager.LoadScene(partida.escena);

    }
    public void cargarInventario()
    {
        Partida partida = Persistencia.CargarPartida("partida unica");
        InventarioController.instance.cargarInventario(partida);
    }
 
    public void nuevaPartida()
    {
        InventarioController.instance.vaciarInventario();
        GuardarPartida();
        cambiarEscena("sala");
    }

    public string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
