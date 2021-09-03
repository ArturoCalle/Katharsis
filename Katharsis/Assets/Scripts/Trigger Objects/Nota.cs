using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nota : MonoBehaviour 
{
    public NotaUI notaUI;
    public GameObject plane;
    public PanelNotas i;

    bool inCollectRange = false;
    // Start is called before the first frame update
    public void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            if(plane.activeInHierarchy)
            {
               notaUI.mostrarAviso(true);
               inCollectRange = true;
            }
        }
    }
    public void OnTriggerExit(Collider col)
    {
        notaUI.mostrarAviso(false);
        inCollectRange = false;
    }

    void Update()
    {
        if(notaUI.recolectado)
        {
           plane.SetActive(false);
        }
        if(inCollectRange)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                SceneController.instance.freeze(true);
                notaUI.agregarNotaAInventario();
                notaUI.mostrarNota();                
            }
        }
        if (notaUI.isActive())
        {         
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if(!notaUI.recolectado)
                {
                    SceneController.instance.freeze(false);
                    notaUI.recolectado = true;
                }

            }
        }
    }
    public bool isCollected()
    {
        return notaUI.recolectado;
    }
    void Start()
    {
        
    }
}
