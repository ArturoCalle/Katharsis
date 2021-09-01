using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject jugador;
    private void Start()
    {
        //jugador.transform.position = SceneController.instance.lastCheckpoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mortal")
        {
            SceneController.instance.EndGame();
        }
    }
}
