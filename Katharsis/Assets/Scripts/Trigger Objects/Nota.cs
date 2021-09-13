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
        if(!notaUI.recolectado)
        {
            notaUI.mostrarAviso(false);
            inCollectRange = false;
        }
    }

    void Update()
    {
        
        if(inCollectRange)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                
                notaUI.agregarNotaAInventario(plane);
                notaUI.mostrarNota();
                inCollectRange = false;
            }
        }
        

    }
    
    void Start()
    {
        
    }
}
