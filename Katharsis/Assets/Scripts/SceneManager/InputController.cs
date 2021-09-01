using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            UIController.instance.getInputsMenu();
        }else if(Time.timeScale == 1f)
        {
            PlayerControls.instance.getInputs();
        }
    }
}
