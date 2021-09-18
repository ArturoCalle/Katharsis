using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPuerta : MonoBehaviour
{
    public string escenaDestino;
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name != "ChechPointStart")
        {
            if (other.tag == "Player")
            {
                CheckPointController.instance.PlayerThroughCheckpoint(this);
                //gameObject.SetActive(false);
                SceneController.instance.GuardarPartida();
                SceneController.instance.cambiarEscena(escenaDestino);
            }
        }
    }
}
