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
    private void Start()
    {
        instance = this;
    }

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
