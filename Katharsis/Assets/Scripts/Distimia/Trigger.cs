using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : State
{
    public Paseando pasear;
    public Transform spawnPosition;

    public override State RunCurrentState()
    {
        if (SceneTriggerController.instance.findTriggerByName("Distimia Trigger").recolectado)
        {
            SceneIAController.instance.instanciarDistimia(spawnPosition);
            return pasear;
        }
        else
        {
            return this;
        }
    }
}
