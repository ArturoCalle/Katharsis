using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggerController : MonoBehaviour
{
    public List<SceneTrigger> triggers;
    public static SceneTriggerController instance;
    Stack<string> triggersPorQuitar = new Stack<string>();
    bool cargar;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cargar = false;
    }

    public SceneTrigger findTriggerByName(string name)
    {
        foreach (SceneTrigger go in triggers)
        {
            if (go.gameObject.name == name)
            {
                return go;
            }
        }
        return null;
    }

    public List<SceneTrigger> getTriggers()
    {
        return triggers;
    }

}
