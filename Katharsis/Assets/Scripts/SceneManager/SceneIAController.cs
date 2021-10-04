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
    public bool InsDistimia;

    private void Start()
    {
        InsDistimia = true;
        instance = this;
    }

    private void Update()
    {
        if (InsDistimia && distimia == null)
        {
            if (SceneTriggerController.instance.findTriggerByName("Distimia Trigger").recolectado)
            {
                SceneTriggerController.instance.findTriggerByName("Distimia Trigger").transform.parent.gameObject.SetActive(false);
                instanciarDistimia(startPos.transform);
            }
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
        InsDistimia = false;
    }

}
