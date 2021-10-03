using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaCutscene : MonoBehaviour
{    
    public GameObject cutsceneCam;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        SceneController.instance.jugador.SetActive(false);
        StartCoroutine(FinishCut());
    }
    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(10);
        SceneController.instance.prefabJugador.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}


