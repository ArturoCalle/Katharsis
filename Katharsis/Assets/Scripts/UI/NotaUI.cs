using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotaUI : MonoBehaviour
{
    public GameObject aviso;
    public GameObject nota;

    public string escena;
    public string nombre;
    public char tipo;
    public int numNota;
    public bool recolectado;
    
    
    public NotaUI(Recolectable r)
    {
        escena = r.getEscena();
        nombre = r.getNombre();
        tipo = r.getTipo();
        numNota = r.getNumNota();
        recolectado = r.getRecolectado();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aviso.activeInHierarchy)
        {
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("desactivar nota");
                aviso.SetActive(false);
                nota.SetActive(false);
            }

        }
    }
    public void mostrarNota()
    {  
        nota.SetActive(true);
        aviso.SetActive(true);
        aviso.GetComponentInChildren<Text>().text = "(Z) aceptar";
    }
    public void  mostrarAviso(bool mostrar)
    {
        aviso.SetActive(mostrar);
    }
    public void agregarNotaAInventario()
    {
        InventarioController.instance.agregarNota(nombre, escena, tipo, recolectado, numNota);
    }
    public bool isActive()
    {
        return (aviso.activeInHierarchy);
    }
    public void setDatos(Recolectable r)
    {
        escena = r.getEscena();
        nombre = r.getNombre();
        tipo = r.getTipo();
        numNota = r.getNumNota();
        recolectado = r.getRecolectado();
    }
}
