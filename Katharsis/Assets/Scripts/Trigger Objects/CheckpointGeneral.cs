using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGeneral : MonoBehaviour
{
    public static CheckpointGeneral instance;
    public List<CheckpointSingle> checkpoints;
    public CheckpointSingle lastCheckpoint;
    private void Awake()
    {
        instance = this;
        foreach (CheckpointSingle checkpointSingle in checkpoints)
        {            
            Debug.Log(checkpointSingle);

            checkpointSingle.SetCheckpointsMapa(this);
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        Debug.Log(checkpointSingle.transform.name);
        
        lastCheckpoint = checkpointSingle;
        Debug.Log(lastCheckpoint);
        
    }
}
