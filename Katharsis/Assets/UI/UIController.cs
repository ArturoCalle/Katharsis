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

    Recolectable nota;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        desactivarPaneles();

    }

    // Update is called once per frame
    void Update()
    {
        if (instance.pauseScreen.activeInHierarchy)
        {
            getInputsMenu();
        }

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

    void getInputsMenu()
    {
        menuPausa mp = pauseScreen.GetComponent<menuPausa>();
        PanelOpciones po = panelOpciones.GetComponent<PanelOpciones>();
        Inventario i = panelNotas.GetComponent<PanelInventario>().inventario;
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
            Debug.Log(i.isLocked());
            if (!mp.isLocked())
            {
                mp.setLock(true);
                mp.seleccionar();
            }
           if(!po.isLocked())
            {
                po.setLock(true);
                po.seleccionar(mp);
            }
            if (!i.isLocked())
            {
                //i.setLock(true);
                i.seleccionar();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
        }
    }
    public void Reanudar()
    {
        desactivarPaneles();
        PlayerControls.instance.PauseUnpause();
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
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        pauseScreen.SetActive(true);
    }
    public void EndGame()
    {
        Debug.Log("GameOver");
        PlayerControls.instance.freeze(true);
        panelMuerte.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("OPRIMÍ Z");
            desactivarPaneles();
        }


    }
    
    
    
}
