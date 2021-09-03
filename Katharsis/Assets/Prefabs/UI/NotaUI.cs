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
    
    Recolectable recolectable;
    public NotaUI(Recolectable r)
    {
        recolectable = r;
        escena = recolectable.getEscena();
        nombre = recolectable.getNombre();
        tipo = recolectable.getTipo();
        numNota = recolectable.getNumNota();
        recolectado = recolectable.getRecolectado();
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
}
