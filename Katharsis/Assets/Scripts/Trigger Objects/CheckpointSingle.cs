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
            checkpointGeneral.PlayerThroughCheckpoint(this);
            gameObject.SetActive(false);

        }
        

    }
    public void SetCheckpointsMapa(CheckpointGeneral checkpointGeneral)
    {
        this.checkpointGeneral = checkpointGeneral;
    }

}
