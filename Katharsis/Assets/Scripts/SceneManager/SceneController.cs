using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject prefabJugador;
    public CheckpointSingle ultimoCheckPoint;
    public GameObject jugador;
    public string CheckpointPuerta = "";

    public bool pausa;
    bool cargar = false;
    
    /**
     * Instancia al jugar en la escena y carga la partida
     */
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "Pantalla Principal")
        {
            Partida partida = Persistencia.CargarPartida("partida unica");
            CheckpointPuerta = partida.CheckpointPuerta;
            prefabJugador.transform.position = ultimoCheckPoint.transform.position;
            jugador = Instantiate(prefabJugador);
        }
        instance = this;
        pausa = false;        
    }

    internal void bloquearPlayerControls(bool estado)
    {
        PlayerControls.instance.enabled = estado;
    }
    /**
     * Activa la animación al iniciar una nueva partida
     */
    internal void playArepa()
    {
        PlayerControls.instance.playArepa();
    }
    /*
     * Si hay una partida guardada utliza respawn para destruir el objeto jugador y reinstanciarlo
     */
    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "Pantalla Principal")
        {
            if(cargar == false)
            {
                respawn();
            }
        }
    }
    public void cambiarEscena(string nombre)
    {
        
        if(CheckpointPuerta != "")
        {
            GuardarPartida();
        }
        SceneManager.LoadScene(nombre);

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
    }

    public void MenuPausa()
    {
        pause();
        UIController.instance.pauseScreen.SetActive(true);
    }
    //Inicia el proceso de guardar partida
    public void GuardarPartida()
    {
        Debug.Log("guardando....");
        Persistencia.GuardarPartida("partida unica");
    }
    //Si la partida existe, carga los datos del inventario y la ultima escena guardada
    public void CargarPartida()
    {
        try
        {
            Partida partida = Persistencia.CargarPartida("partida unica");
            CheckpointPuerta = partida.CheckpointPuerta;
            InventarioController.instance.cargarInventario(partida);
            SceneManager.LoadScene(partida.escena);
        }
        catch(System.Exception e)
        {
            Debug.Log("error, la partida no existe");
            Debug.Log(e.Message);
        }    
    }
    //carga el inventario desde la persistencia
    public void cargarInventario()
    {
        Partida partida = Persistencia.CargarPartida("partida unica");
        InventarioController.instance.cargarInventario(partida);
    }

    //reinicia el inventario y lo persiste
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
    //destruye al jugador y si hay un checkpoint disponible lo instancia y guarda la partida
    public void respawn()
    {
        Destroy(jugador);
        if(ultimoCheckPoint != null)
        {
            prefabJugador.transform.position = ultimoCheckPoint.transform.position;
            prefabJugador.transform.rotation = ultimoCheckPoint.transform.rotation;
            jugador = Instantiate(prefabJugador);
            GuardarPartida();
            cargar = true;
        }
    }

}
