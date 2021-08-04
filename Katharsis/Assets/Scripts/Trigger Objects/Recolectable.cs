using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recolectable : MonoBehaviour 
{
    public GameObject aviso;
    public GameObject nota;
    public string escena;
    public string nombre;

    bool recolectado = false;
    bool inCollectRange = false;
    // Start is called before the first frame update
    public void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
           aviso.SetActive(true);
           inCollectRange = true;
        }
    }
    public void OnTriggerExit(Collider col)
    {
        aviso.SetActive(false);
        inCollectRange = false;
    }

    public void mostrarNota()
    {
        nota.SetActive(true);
        PlayerControls.instance.freeze(true);
        aviso.GetComponentInChildren<Text>().text = "(Z) aceptar";
    }
   
    private void Update()
    {
        if(recolectado)
        {
            gameObject.SetActive(false);
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
                aviso.SetActive(false);
                nota.SetActive(false);
                PlayerControls.instance.freeze(false);
                recolectado = true;

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
