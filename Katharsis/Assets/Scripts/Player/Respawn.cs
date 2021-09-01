using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void Start()
    {
        transform.position = SceneController.instance.lastCheckpoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mortal")
        {
            SceneController.instance.EndGame();
            
        }
    }
}
