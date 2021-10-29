using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotaUI : MonoBehaviour
{
    public GameObject aviso;
    public GameObject nota;
    public Text textoNota;

    public string escena;
    public string nombre;
    public char tipo;
    public int numNota;
    public bool recolectado;
    private bool cargado;

    public Sprite hoja_informativa;
    public Sprite hoja_tutorial;
    public Sprite hoja_historia;
    public Sprite hoja_motivacional;

    SpriteRenderer spriterenderer;


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
        cargado = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!cargado)
        {
            nota.gameObject.transform.GetChild(1).GetComponent<Text>().text = nombre;
            cambiarHoja(tipo);
            textoNota.text = InventarioController.instance.getTextoNota(numNota);
            cargado = true;
        }
        
        if (aviso.activeInHierarchy && nota.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                aviso.SetActive(false);
                nota.SetActive(false);
            }

        }

    }
    public void mostrarNota()
    {
        SceneController.instance.freeze(true);
        nota.SetActive(true);
        aviso.SetActive(true);
        aviso.GetComponentInChildren<Text>().text = "(Z) aceptar";
    }
    public void  mostrarAviso(bool mostrar)
    {
        aviso.SetActive(mostrar);
    }
    public void agregarNotaAInventario(GameObject nota)
    {
        recolectado = true;
        InventarioController.instance.agregarNota(nombre, escena, tipo, recolectado, numNota);
        nota.SetActive(false);
        
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
    void cambiarHoja(char tipo)
    {
        switch(tipo)
        {
            case 'T':
                nota.transform.GetChild(0).GetComponent<Image>().sprite = hoja_tutorial;
                break;
            case 'H':
                nota.transform.GetChild(0).GetComponent<Image>().sprite = hoja_historia;
                break;
            case 'I':
                nota.transform.GetChild(0).GetComponent<Image>().sprite = hoja_informativa;
                break;
            case 'M':
                nota.transform.GetChild(0).GetComponent<Image>().sprite = hoja_motivacional;
                break;
            default:
                nota.transform.GetChild(0).GetComponent<Image>().sprite = hoja_tutorial;
                break;
        }
    }
}
