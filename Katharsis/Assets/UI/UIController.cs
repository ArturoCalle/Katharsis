using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image blackScreen;
    public float fadeSpeed;
    public bool fadeToBlack, fadeFromBlack;
    public GameObject pauseScreen;
    public GameObject panelOpciones;
    public GameObject panelNotas;
    public GameObject panelMuerte;

    Nota nota;

    // Start is called before the first frame update
    void Start()
    {
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        instance = this;
        desactivarPaneles();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
        */

    }

    public void getInputsMenu()
    {

        menuPausa mp = pauseScreen.GetComponent<menuPausa>();
        PanelOpciones po = panelOpciones.GetComponent<PanelOpciones>();
        PanelNotas i = panelNotas.GetComponent<PanelInventario>().inventario;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(-1);
            }
            if (!po.isLocked())
            {
                po.cambiarSeleccion(-1);
            }
            if(!i.isLocked())
            {
                
                i.cambiarSeleccion(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(1);
            }
            if (!po.isLocked())
            {
                po.cambiarSeleccion(1);
            }
            if (!i.isLocked())
            {
                i.cambiarSeleccion(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (panelMuerte.activeInHierarchy)
            {
                SceneController.instance.restartGameFromCheckpoint();
            }else if (!mp.isLocked())
            {
                mp.setLock(true);
                mp.seleccionar();
            }else if(!po.isLocked())
            {
                po.setLock(true);
                po.seleccionar(mp);
            }else if (!i.isLocked())
            {
                //i.setLock(true);
                i.seleccionar();
            }  
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(panelOpciones.activeInHierarchy)
            {
                po.setLock(true);
                panelOpciones.SetActive(false);
            }
            else if(panelNotas.activeInHierarchy)
            {
                i.setLock(true);
                panelNotas.SetActive(false);
            }
            else if (panelMuerte.activeInHierarchy)
            {
                panelMuerte.SetActive(false);
                SceneController.instance.cambiarEscena("Pantalla Principal");
            }
            else
            {
                Reanudar();
            }
        }
    }
    public void Reanudar()
    {
        desactivarPaneles();
        SceneController.instance.resume();
    }
    public void hidePanel(string name)
    {
        switch(name)
        {
            case "opciones":
                panelOpciones.SetActive(false);
                break;
        }
    }
    public void desactivarPaneles()
    {
        panelOpciones.SetActive(false);
        pauseScreen.GetComponent<menuPausa>().setLock(false);
        pauseScreen.SetActive(false);
        panelNotas.SetActive(false);
        panelMuerte.SetActive(false);
    }

    public void pausar()
    {
        Debug.Log("entro a menu pausa");
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        instance.pauseScreen.SetActive(true);
    }
    
    
    
    
}
