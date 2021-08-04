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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        panelOpciones.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (instance.pauseScreen.activeInHierarchy)
        {
            getInputs();
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
    void getInputs()
    {
        menuPausa mp = pauseScreen.GetComponent<menuPausa>();
        PanelOpciones po = panelOpciones.GetComponent<PanelOpciones>();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(-1);
            }
            else if (!po.isLocked())
            {
                po.cambiarSeleccion(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(1);
            }
            else if (!po.isLocked())
            {
                po.cambiarSeleccion(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

            if (!mp.isLocked())
            {
                mp.setLock(true);
                mp.seleccionar();
            }
            else if(!po.isLocked())
            {
                po.setLock(true);
                po.seleccionar(mp);
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
    }

    public void pausar()
    {
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        pauseScreen.SetActive(true);
    }
    
    
    
}
