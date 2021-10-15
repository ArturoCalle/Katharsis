using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneTrigger : MonoBehaviour
{
    public List<GameObject> objetosBloqueados;//los objetos trigger desbloquean otros objetos, el objeto a desbloquear PUEDEN tener un componente en cuya funcion start tenga la rutina necesaria para iniciar una accion en su funcion start(), sea una animacion, instanciar un objeto en escena, etc...
    public Text aviso;
    public bool automatico;
    public string nombre;
    public int numero;
    public bool recolectado;
    public int recolectableBloqueante;//Requiere el numero de recolectable que es necesario recoger para activarse
    public UnityEvent onTriggerCollected;

    private Recolectable recolectable;
    public void OnTriggerStay(Collider col)
    {
        Recolectable r = InventarioController.instance.getRecolectable(recolectableBloqueante);
        if(r.getRecolectado() && !recolectado)
        {
            if (col.tag == "Player")
            {
                if(!automatico)
                {

                    aviso.enabled = true;
                }
                else
                {
                    recolectar();
                }
            }
        }            
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            aviso.enabled = false;
        }
    }
    private void Start()
    {
        
        recolectable = new Recolectable(nombre, SceneController.instance.getCurrentSceneName(), recolectado, numero);
        aviso.enabled = false;
        foreach (GameObject go in objetosBloqueados)
        {
            go.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        recolectable.setRecolectado(recolectado);
        if(aviso.enabled && !recolectado)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                recolectar();
                gameObject.SetActive(false);
            }
        }
    }

    public void recolectar()
    {
        foreach (GameObject go in objetosBloqueados)
        {
            go.SetActive(true);
        }
        aviso.enabled = false;
        recolectado = true;
        recolectable.setRecolectado(true);
        InventarioController.instance.agregarTrigger(recolectable);
        onTriggerCollected?.Invoke();
    }
    public void recolectar(bool recolectar)
    {
        foreach (GameObject go in objetosBloqueados)
        {
            go.SetActive(true);
        }
        aviso.enabled = false;
        recolectado = recolectar;
        recolectable.setRecolectado(true);
    }

}
