using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    // Start is called before the first frame update
    private CheckpointGeneral checkpointGeneral;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("objeto jugador");
            checkpointGeneral.PlayerThroughCheckpoint(this);
            Destroy(gameObject);
        }
        

    }
    public void SetCheckpointsMapa(CheckpointGeneral checkpointGeneral)
    {
        this.checkpointGeneral = checkpointGeneral;
    }

}
