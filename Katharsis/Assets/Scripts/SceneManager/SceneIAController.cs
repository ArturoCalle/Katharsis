using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIAController : MonoBehaviour
{
    public GameObject prefabDistimia;
    private GameObject distimia = null;
    public static SceneIAController instance;
    public GameObject[] targets;
    public GameObject startPos;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if(SceneTriggerController.instance.findTriggerByName("Distimia Trigger").recolectado && distimia == null)
        {
            instanciarDistimia(startPos.transform);
        }
    }

    public void instanciarDistimia(Transform posicion)
    {
        prefabDistimia.transform.position = posicion.position;
        distimia = Instantiate(prefabDistimia);
    }

    public void destroyDistimia()
    {
        Destroy(distimia);
    }

}
