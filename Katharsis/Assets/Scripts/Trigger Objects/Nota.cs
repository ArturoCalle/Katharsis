using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nota : MonoBehaviour 
{
    public GameObject aviso;
    public GameObject nota;
    public GameObject plane;
    public PanelNotas i;

    public string escena;
    public string nombre;

    bool recolectado = false;
    bool inCollectRange = false;
    bool mostrandoNota = false;
    // Start is called before the first frame update
    public void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            if(plane.activeInHierarchy)
            {
               aviso.SetActive(true);
               inCollectRange = true;
            }
        }
    }
    public void OnTriggerExit(Collider col)
    {
        aviso.SetActive(false);
        inCollectRange = false;
    }

    public void mostrarNota()
    {
        SceneController.instance.freeze(true);
        nota.SetActive(true);
        aviso.SetActive(true);
        aviso.GetComponentInChildren<Text>().text = "(Z) aceptar";
    }

    private void Update()
    {
        if(recolectado)
        {
           plane.SetActive(false);
        }
        if(inCollectRange)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                mostrarNota();

            }
        }
        if (aviso.activeInHierarchy)
        {         
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if(!recolectado)
                {
                    aviso.SetActive(false);
                    nota.SetActive(false);
                    SceneController.instance.freeze(false);
                    recolectado = true;
                    mostrandoNota = false;

                }
                else if(mostrandoNota)
                {
                    aviso.SetActive(false);
                    nota.SetActive(false);
                    mostrandoNota = false;
                    i.setLock(false);
                }
                else
                {
                    mostrandoNota = true;
                }

            }
            
        }
    }
    public bool isCollected()
    {
        return recolectado;
    }
    void Start()
    {
        
    }
}
