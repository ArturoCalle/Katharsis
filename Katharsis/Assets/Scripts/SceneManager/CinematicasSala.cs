using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicasSala : MonoBehaviour
{
    //cinematics
    public PlayableDirector timeline2;
    public GameObject cutsceneCam2;
    public PlayableDirector timeline1;
    public GameObject cutsceneCam1;
    public Cinematica cinematica1;
    public bool controlesVistos = false;
    public static CinematicasSala instance; 
    /**
     * Esta clase se encarga de controlar que el jugador no pueda mover a trompi mientras una cinemática se esté reproduciendo.
     * Dependiende del estado de la cinemática (Paused, Played, Stopped) activa y desactiva las cámaras y el PlayerControls.
     */
    private void Start()
    {
        instance = this;
    }
    /**
     * La función update se encarga principalmente de revisar el estado de la cinemática cada frame para prender y apagar las cámaras.
     * Esto lo realiza dependiendo de la duración de la cinemática. Los gameobjects de las cinemáticas y de las cámaras son asignadas desde el inspector.
     */
    void Update()
    {  
        if (SceneController.instance.getCurrentSceneName() == "Sala" && SceneController.instance.jugador != null)
        {
            if (timeline1.state.ToString() == "Paused")
            {
                if(!controlesVistos && (cinematica1.duracion <= 0))
                {
                    UIController.instance.activarPanelControles();
                    controlesVistos = true;
                }
                cutsceneCam1.SetActive(false);
                
            }
            else if (!InventarioController.instance.getRecolectable(0).getRecolectado())
            {
                SceneController.instance.playArepa();
                SceneController.instance.bloquearPlayerControls(true);
               
            }
            if (timeline2.state.ToString() == "Paused")
            {
                cutsceneCam2.SetActive(false);
                
            }
            else
            {
                SceneController.instance.bloquearPlayerControls(true);
            }
        }
    }
}
