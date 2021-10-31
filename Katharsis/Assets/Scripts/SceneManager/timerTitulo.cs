using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Esta clase se utiliza en la pantalla del titulo, regula el fade in y el fadeout del titulo y la advertencia.
 * Utiliza el componente de unity scenemanager para cambiar de escena
 */
public class timerTitulo : MonoBehaviour
{
    public float tiempo;// tiempo de espera para hacer el fadeout
    public GameObject titulo;
    public GameObject advertencia;
    public ScreenFader fader;// contiene una pantalla en negro y las funciones que regulan la opacidad.
    bool fadedIn;
    
    void Start()
    {
        tiempo = 1;
        fadedIn = false;
        titulo.SetActive(true);
        advertencia.SetActive(false);
    }

    /**
     * En el update se revisa el estado de los cmponentes activos y se capturan las entradas del usuario de manera que 
     * cambie de pantalla si se presiona alguna tecla
     */
    void Update()
    {
        if(Input.anyKey)
        {
            next();
        }
        if (!fadedIn)
        {
            fadedIn = fader.fadeIn();
        }
        else
        {
            tiempo -= Time.deltaTime;
            if (tiempo <= 0)
            {
                fader.fadeOut();
                if (fader.getTransparency() >= 1)
                {
                    next();
                }
            }
        }
    }
    /**
     * Funcion que regulan los cambios en la sucesion de eventos segun se esten presentado en pantalla.
     */
    public void next()
    {
        if (titulo.activeInHierarchy)
        {
            tiempo = 13.0f;
            titulo.SetActive(false);
            fadedIn = false;
            advertencia.SetActive(true);
        }
        else if (advertencia.activeInHierarchy)
        {
            SceneManager.LoadScene("Pantalla Principal");
        }
           
    }
}
