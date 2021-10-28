using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public List<CheckpointSingle> checkpoints;
    public static CheckPointController instance;
    public CheckpointPuerta checkPuerta;
    bool cargar = false;
    public string ultimoCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (SceneController.instance.getCurrentSceneName() != "Pantalla Principal")
        {
            //TO DO Cargar un checkpoitn a la partyida
            //ultimoCheckpoint = SceneController.instance.cargarUltimoCheckpoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!cargar)
        {
            
            //Debug.Log(SceneController.instance.CheckpointPuerta);
            if(SceneController.instance.CheckpointPuerta != "")
            {
                if(checkpoints.Count != 0)
                {
                    SceneController.instance.ultimoCheckPoint = getCheckpoint(SceneController.instance.CheckpointPuerta);
                    SceneController.instance.CheckpointPuerta = "";
                    
                    cargar = true;                    
                    SceneController.instance.respawn();
                }
            }
            else
            {
                SceneController.instance.ultimoCheckPoint = getCheckpoint(ultimoCheckpoint);
            }
            
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        SceneController.instance.ultimoCheckPoint = checkpointSingle;
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
        if(checkpoints.Count == 0)
        {
            
        }
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
