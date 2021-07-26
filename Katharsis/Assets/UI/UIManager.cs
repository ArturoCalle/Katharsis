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
        panelLateral.GetComponent<Panel>().Reset();
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
        Panel pl = panelLateral.GetComponent<Panel>();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!mp.isLocked())
            {
                mp.cambiarSeleccion(1);
            }
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {

            if(!mp.isLocked())
            {
                int seleccion = mp.getSeleccion();
                mp.setLock(true);
                switch(seleccion)
                {
                    case 0:
                        Reanudar();
                        break;
                    case 1:
                        Debug.Log("ver notas");
                        break;
                    case 2:
                        Debug.Log("ver opciones");
                        Opciones(pl);

                        break;
                    case 3:
                        Debug.Log("volver al menú principal");
                        break;
                    default:
                        break;
                }
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
    void Reanudar()
    {
        panelLateral.GetComponent<Panel>().Reset();
        PlayerControls.instance.PauseUnpause();
    }
    void Opciones(Panel pl)
    {
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
    void Atras(Panel pl)
    {
        pl.Reset();
        pauseScreen.GetComponent<menuPausa>().setLock(false);
    }
    
    
    
}
