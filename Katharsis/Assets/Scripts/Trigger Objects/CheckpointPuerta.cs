using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPuerta : MonoBehaviour
{
    public string escenaDestino;
    void Update()
    {
        this.gameObject.transform.Rotate(0, 10, 0 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name != "ChechPointStart")
        {
            if (other.tag == "Player")
            {
                CheckPointController.instance.PlayerThroughCheckpoint(this);
                //gameObject.SetActive(false);
                SceneController.instance.GuardarPartida();
                StartCoroutine(UIController.instance.oscurecerPantallaYCambiarEscena(escenaDestino));
            }
        }
    }
}
