using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public GameObject cutsceneCam;
    public PlayableDirector timeline;
    


    private void OnTriggerExit(Collider other)
    {
        timeline.Stop();
        cutsceneCam.SetActive(false);
        //camarasVirtuales.SetActive(false);
        
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
        //camarasVirtuales.SetActive(true);
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


