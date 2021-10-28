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

    void Update()
    {
        if (SceneController.instance.getCurrentSceneName() == "Sala" && SceneController.instance.jugador != null)
        {
            if (timeline1.state.ToString() == "Paused")
            {
                cutsceneCam1.SetActive(false);
                SceneController.instance.bloquearPlayerControls(true);
            }
            else if (!InventarioController.instance.getRecolectable(0).getRecolectado())
            {
                SceneController.instance.playArepa();
            }
            if (timeline2.state.ToString() == "Paused")
            {
                cutsceneCam2.SetActive(false);
                SceneController.instance.bloquearPlayerControls(true);
            }
            else
            {
                SceneController.instance.bloquearPlayerControls(true);
            }
        }
    }
}