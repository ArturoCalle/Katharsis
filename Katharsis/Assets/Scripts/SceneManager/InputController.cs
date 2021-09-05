using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (SceneController.instance.pausa)
        {

            UIController.instance.getInputsMenu();
        }
        else if(SceneController.instance.pausa)
        {
            PlayerControls.instance.getInputs();
        }
    }
}
