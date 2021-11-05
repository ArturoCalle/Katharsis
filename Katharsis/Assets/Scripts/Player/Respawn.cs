using UnityEngine;

/**
 * Esta clase se encarga de controlar lo que pasa cuando el jugador muere por tocar cualquier objeto dentro de la escena con
 * el tag "Mortal".
 * Al morir, la clase llama a la función "EndGame" de la clase SceneController.
 */
public class Respawn : MonoBehaviour
{

    public GameObject jugador; //El jugador es asignado desde el inspector
    private void Start()
    {
        //jugador.transform.position = SceneController.instance.lastCheckpoint.transform.position;
    }
    /**
     * Si el jugador toca un collider de un objeto con el tag "Mortal", se llama a la función EndGame de la clase SceneController.
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mortal")
        {
            SceneController.instance.EndGame();
        }
    }
}
