using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelObjetivo : MonoBehaviour
{
    public Text texto;
    private void Update()
    {
        if(SceneController.instance.pausa)
        {
            texto.enabled = false;
        }
        else
        {
            texto.enabled = true;
        }
    }

    public void cambiarTexto(string textonuevo)
    {
        texto.enabled = true;
        texto.text = textonuevo;
        texto.enabled = false;
    }
}
