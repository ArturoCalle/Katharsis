using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGeneral : MonoBehaviour
{
    public List<CheckpointSingle> checkpoints;
    private void Awake()
    {          
        foreach (CheckpointSingle checkpointSingle in checkpoints)
        {            
            Debug.Log(checkpointSingle);
            checkpointSingle.SetCheckpointsMapa(this);
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        Debug.Log(checkpointSingle.transform.name);
    }
}
