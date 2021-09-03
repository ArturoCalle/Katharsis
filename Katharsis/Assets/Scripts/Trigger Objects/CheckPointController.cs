using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public List<CheckpointSingle> checkpoints;
    public static CheckPointController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        SceneController.instance.ultimoCheckPoint = checkpointSingle;
        SceneController.instance.GuardarPartida();
    }

    public void cargarCheckpoints(int[] checkpoints) // megafonos
    {
        //TODO
    }
}
