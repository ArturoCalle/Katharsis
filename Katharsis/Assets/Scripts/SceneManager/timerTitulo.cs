using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timerTitulo : MonoBehaviour
{
    public float tiempo;
    public GameObject titulo;
    public GameObject advertencia;
    public ScreenFader fader;
    bool fadedIn;
    // Start is called before the first frame update
    void Start()
    {
        tiempo = 1;
        fadedIn = false;
        titulo.SetActive(true);
        advertencia.SetActive(false);
    }

    // Update is called once per frame
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
            /*
            tiempo = 5.0f;
            advertencia.SetActive(false);
            fadedIn = false;
            controles.SetActive(true);*/
        }
           
    }
}
