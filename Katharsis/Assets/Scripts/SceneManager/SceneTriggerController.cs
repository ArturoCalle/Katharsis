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

    private void Update()
    {
        if(cargar == false)
        {
            recogerTriggers();
        }
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

    public void recogerTriggers()
    {
        if(InventarioController.instance.getTriggers() != null)
        {
            List<Recolectable> rs = InventarioController.instance.getTriggers();
            foreach(Recolectable r in rs)
            {
                if (r.getEscena() == SceneController.instance.getCurrentSceneName() && r.getRecolectado())
                {
                    Debug.Log(triggers.Count);
                    for (int i = 0; i < triggers.Count; i++)
                    {
                        if (triggers[i].numero == r.getNumNota())
                        {
                            triggers[i].recolectar(true);
                        }
                   }
                }
            }
            cargar = true;
        }
        else
        {
            cargar = false;
        }
    }

}
