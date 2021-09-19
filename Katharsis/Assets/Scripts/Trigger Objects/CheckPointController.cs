using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public List<CheckpointSingle> checkpoints;
    public static CheckPointController instance;
    public CheckpointPuerta checkPuerta;
    bool cargar = false;

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        if (SceneController.instance.CheckpointPuerta != "")
        {
            cargar = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!cargar)
        {       
             if(checkpoints.Count != 0)
             {
                 SceneController.instance.ultimoCheckPoint = getCheckpoint(SceneController.instance.CheckpointPuerta);
                 SceneController.instance.CheckpointPuerta = "";
                 SceneController.instance.GuardarPartida();
                 cargar = true;
                 SceneController.instance.respawn();
             }
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        
        SceneController.instance.ultimoCheckPoint = checkpointSingle;
        //SceneController.instance.GuardarPartida();
        
    }

    public void PlayerThroughCheckpoint(CheckpointPuerta checkpointPuerta)
    {

        SceneController.instance.CheckpointPuerta = checkpointPuerta.gameObject.name;
        
    }

    public void cargarCheckpoints(int[] checkpoints) // megafonos
    {
        //TODO
    }
    public CheckpointSingle getCheckpoint(string nombre)
    {
       foreach(CheckpointSingle cs in checkpoints)
        {
            if(cs.gameObject.name == nombre)
            {
                return cs;
            }

        }
        return null;
    }
}
