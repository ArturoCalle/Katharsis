using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Esta clase se encarga de detectar cuando el jugador pasa por un checkpoint que está ubicado dentro de la escena. Al hacer esto
 * la función "PlayerThroughCheckpoint" de la clase CheckPointController es llamada.
 */
public class CheckpointSingle : MonoBehaviour
{
    /**
     * Esta función detecta si lo que tocó el objeto tiene el tag "Player" para saber si es el jugador. Luego de esto, se registra en
     * el CheckPointController
     */
    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.name != "ChechPointStart")
        {
            if (other.tag == "Player")
            {   
                CheckPointController.instance.PlayerThroughCheckpoint(this);
            }
        }
    }
}
