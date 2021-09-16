using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIAController : MonoBehaviour
{
    public GameObject prefabDistimiaSala;
    public GameObject prefabDistimiaComedor;
    public GameObject prefabDistimiaCocina;

    private GameObject distimia = null;
    public List<GameObject> trigger;

    public static SceneIAController instance;

    enum Estados { activo, inactivo, enfadado, busqueda };
    private Estados actual;

    public List<GameObject> targets;

    private void Start()
    {
        actual = Estados.inactivo;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneTriggerController.instance.findTriggerByName("Distimia Trigger").recolectado && actual == Estados.inactivo)
        {
            instanciarDistimia();
        }
    }

    private void instanciarDistimia()
    {
        if (SceneController.instance.getActiveScene().name == "Sala")
        { 
            distimia = Instantiate(prefabDistimiaSala);
            actual = Estados.activo;
        }
    }

    public void destroyDistimia()
    {
        Destroy(distimia);
    }

    public void setEnum(string estado)
    {
        switch (estado)
        {
            case "activo":
                actual = Estados.activo;
                break;
            case "inactivo":
                actual = Estados.inactivo;
                break;
            case "enfadado":
                actual = Estados.enfadado;
                break;
            case "busqueda":
                actual = Estados.busqueda;
                break;
            default:
                break;
        }
    }

    public string getEstado()
    {
        switch (actual)
        {
            case Estados.activo:
                return "activo";
            case Estados.inactivo:
                return "inactivo";
            case Estados.busqueda:
                return "busqueda";
            case Estados.enfadado:
                return "enfadado";
            default:
                return "";
        }
    }

}
