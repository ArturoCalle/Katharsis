using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Esta clase se encarga de detectar cuando el jugador pasa por un checkpoint que est� ubicado dentro de la escena. Al hacer esto
 * la funci�n "PlayerThroughCheckpoint" de la clase CheckPointController es llamada.
 */
public class CheckpointSingle : MonoBehaviour
{
    /**
     * Esta funci�n detecta si lo que toc� el objeto tiene el tag "Player" para saber si es el jugador. Luego de esto, se registra en
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
