using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Assets.ThirdPerson;

public class SceneIAController : MonoBehaviour
{
    public GameObject prefabDistimia;
    private GameObject distimia = null; // controla si distimia ha sido instanciado
    public static SceneIAController instance;
    public GameObject[] targets;
    public GameObject startPos;
    public bool InsDistimia; // controla si distimia esta en la escena

    private void Start()
    {
        InsDistimia = true;
        instance = this;
    }

    /**
     * Valida las condiciones de instanciacón de Distimia. Si el megafono ha sido recolectado activa la llave de salida de la escena
     */
    private void Update()
    {
        if (InsDistimia && distimia == null && !SceneTriggerController.instance.findTriggerByName("megafono").recolectado)
        {
            if(SceneController.instance.getCurrentSceneName() == "Sala")
            {
                if (SceneTriggerController.instance.findTriggerByName("Distimia Trigger").recolectado)
                {
                    SceneTriggerController.instance.findTriggerByName("Distimia Trigger").transform.parent.gameObject.SetActive(false);
                    instanciarDistimia(startPos.transform);
                }
            }
            else if (SceneController.instance.getCurrentSceneName() == "Comedor" || SceneController.instance.getCurrentSceneName() == "Cocina")
            {
                instanciarDistimia(startPos.transform);
            }
        }
        else if(SceneTriggerController.instance.findTriggerByName("megafono").recolectado)
        {
            CheckPointController.instance.transform.GetChild(1).gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    //Cambia la posicion del prefabricado, lo instancia y guarda la instancia en la variable distimia
    public void instanciarDistimia(Transform posicion)
    {
        prefabDistimia.transform.position = posicion.position;
        distimia = Instantiate(prefabDistimia);
    }

    //destruye el objeto
    public void destroyDistimia()
    {
        Destroy(distimia);
        InsDistimia = false;
    }

}
