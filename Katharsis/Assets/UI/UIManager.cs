using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image blackScreen;
    public float fadeSpeed;
    public bool fadeToBlack, fadeFromBlack;
    public GameObject pauseScreen;
    public GameObject panelLateral;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        panelLateral.GetComponent<PanelOpciones>().Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(instance.pauseScreen.activeInHierarchy)
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
        PanelOpciones pl = panelLateral.GetComponent<PanelOpciones>();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(-1);
            }
            else if(!pl.isLocked())
            {
                pl.cambiarSeleccion(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(1);
            }
            else if (!pl.isLocked())
            {
                pl.cambiarSeleccion(1);
            }
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {

            if(!mp.isLocked())
            {
                mp.setLock(true);
                mp.seleccionar();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!mp.isLocked())
            {
                Reanudar();
            }
            else
            {
                Atras(pl);
            }
        }
    }
    public void Reanudar()
    {
        panelLateral.GetComponent<PanelOpciones>().Reset();
        PlayerControls.instance.PauseUnpause();
    }
    public void Opciones()
    {

        PanelOpciones pl = panelLateral.GetComponent<PanelOpciones>();
        pl.setLock(false);
        pl.Reset();
        pl.cambiarTitulo("Opciones");
        pl.agregarBoton("Video");
        pl.agregarBoton("Audio");
        pl.agregarBoton("Controles");
        pl.agregarBoton("Atras");
    }
    void VolverAlMenu()
    {

    }
    void Atras(PanelOpciones pl)
    {
        pl.Reset();
        pl.setLock(true);
        pauseScreen.GetComponent<menuPausa>().setLock(false);
    }
    
    
    
}
