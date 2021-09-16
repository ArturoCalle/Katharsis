using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggerController : MonoBehaviour
{
    public List<SceneTrigger> triggers;
    public static SceneTriggerController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    private void Update()
    {
        
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

}
