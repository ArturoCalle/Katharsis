using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EntradaCutscene : MonoBehaviour
{
    public GameObject cutsceneCam;
    public PlayableDirector timeline;

    private void OnTriggerExit(Collider other)
    {
        timeline.Stop();
        
        //SceneController.instance.jugador.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        SceneController.instance.jugador.SetActive(false);
        StartCoroutine(FinishCut());
        */
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        
        //SceneController.instance.jugador.SetActive(false);
        timeline.Play();

    }
    /*
    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(8);
        SceneController.instance.prefabJugador.SetActive(true);
        cutsceneCam.SetActive(false);
        SceneController.instance.jugador.GetComponent<>
    }*/
}


