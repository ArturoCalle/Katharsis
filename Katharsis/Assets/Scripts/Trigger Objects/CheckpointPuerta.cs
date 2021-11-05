using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Esta clase se encarga de detectar cuando el jugador toca un objeto dentro de la escena con este script, la pantalla hace una
 * transición y se teletransporta al jugador a una escena determinada.
 */
public class CheckpointPuerta : MonoBehaviour
{
    public string escenaDestino; //Escena a la cual se va a teletransportar 
    /**
     * El update se utiliza para permitir que el objeto dentro de la escena con este script gire horizontalmente. (Llave giratoria delante de algunas puertas)
     */
    void Update()
    {
        this.gameObject.transform.Rotate(0, 100* Time.deltaTime, 0);
    }
    /**
     * Esta función detectar cuando el jugador toca el trigger, al hacer esto, se llama a la función "PlayerThroughCheckpoint" de 
     * la clase CheckPointController, luego de esto el juego es guardado al llamar la función "GuardarPartida" de la clase SceneController.
     * Finalmente, la función oscurecerPantallaYCambiarEscena de la clase UIController es llamada.
     */
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name != "ChechPointStart")
        {
            if (other.tag == "Player")
            {
                CheckPointController.instance.PlayerThroughCheckpoint(this);                
                SceneController.instance.GuardarPartida();
                StartCoroutine(UIController.instance.oscurecerPantallaYCambiarEscena(escenaDestino));
            }
        }
    }
}
