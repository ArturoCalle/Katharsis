using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Esta clase permite controlar los checkpoints que están puestos en la escena, es por esto que tiene una lista de checkpoints la cual puede
 * ser modificada desde el inspector en cada escena.
 */
public class CheckPointController : MonoBehaviour
{
    public List<CheckpointSingle> checkpoints; //Lista de checkpoint singles
    public static CheckPointController instance;
    public CheckpointPuerta checkPuerta;
    bool cargar = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    
    /**
     * La función update se encarga de verificar si la escena fue cargada correctamente, en caso de que no, administra los checkpoints
     * de las puertas y llama a la función "respawn" de la clase SceneController
     */
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
    /**
     * Esta función encuentra un Checkpoint por nombre, recorre la lista de checkpoints y retorna el checkpoint deseado
     */
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
