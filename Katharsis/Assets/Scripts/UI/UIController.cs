using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image blackScreen;
    Color objectColor;
    //public float fadeSpeed;
    public bool fadeToBlack, fadeFromBlack; //oscureder la pantalla y aclarecer la pantalla
    public GameObject pauseScreen;
    public GameObject panelOpciones;
    public GameObject panelNotas;
    public GameObject panelMuerte; //Pantalla de muerte o Game Over
    public GameObject NotaUI;
    public GameObject panelControles;
    public GameObject avisoEsc;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aclararPantalla());
        //StartCoroutine(oscurecerPantallaYCambiarEscena());
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        instance = this;
        desactivarPaneles();

    }

    // Update is called once per frame
    void Update()
    {
        
        //oscurecerPantallaYCambiarEscena();
        
        if(pauseScreen.activeInHierarchy)
        {
            avisoEsc.SetActive(false);
        }
        else
        {
            avisoEsc.SetActive(true);
        }
    }

    public void getInputsMenu()
    {
        if (SceneController.instance.pausa)
        {
            menuPausa mp = pauseScreen.GetComponent<menuPausa>();
            PanelOpciones po = panelOpciones.GetComponent<PanelOpciones>();
            PanelNotas i = panelNotas.GetComponent<PanelInventario>().panelNotas;

            if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!mp.isLocked())
                {
                    mp.cambiarSeleccion(-1);
                }
                if (!po.isLocked())
                {
                    po.cambiarSeleccion(-1);
                }
                if (!i.isLocked())
                {

                    i.cambiarSeleccion(-1);
                }
            }
            if (Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
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
            if (Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown(KeyCode.Return))
            {
                if (panelMuerte.activeInHierarchy)
                {
                    SceneController.instance.restartGameFromCheckpoint();
                }
                else if (!mp.isLocked())
                {
                    mp.setLock(true);
                    mp.seleccionar();
                }
                else if (panelControles.activeInHierarchy)
                {
                    panelControles.SetActive(false);
                    po.setLock(false);
                }
                else if (!po.isLocked())
                {
                    po.setLock(true);
                    po.seleccionar(mp);
                }
                else if (!i.isLocked())
                {
                    //i.setLock(true);
                    i.seleccionar();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.X))
            {
                
                if (panelOpciones.activeInHierarchy)
                {
                    if (panelControles.activeInHierarchy)
                    {
                        panelControles.SetActive(false);
                        po.setLock(false);
                    }
                    else
                    {

                        po.setLock(true);
                        panelOpciones.SetActive(false);
                        mp.setLock(false);
                    }
                }
                else if (panelNotas.activeInHierarchy)
                {
                    i.setLock(true);
                    panelNotas.SetActive(false);
                    mp.setLock(false);
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
    }
    /** 
     * Esta funci?n permite reanudar el juego y desactiva los paneles de pausa
     */
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
       
        panelOpciones.GetComponent<PanelOpciones>().reiniciarBotones();
        instance.pauseScreen.SetActive(true);
    }
    public void activarPanelControles()
    {
        menuPausa mp = pauseScreen.GetComponent<menuPausa>();
        PanelOpciones po = panelOpciones.GetComponent<PanelOpciones>();
        pausar();
        mp.setLock(true);
        panelOpciones.SetActive(true);
        po.setLock(true);
        panelControles.SetActive(true);
    }
    
    /**
     * Esta funci?n permite oscurecer la pantalla antes de cambiar de escena para realizar una transici?n. Es llamada desde la
     * funci?n update como una corutina para que cada frame del juego, la pantalla se vaya volviendo gradualmente negra.
     * Recibe el nombre de una escena como un string y utiliza la funci?n de SceneController "cambiarEscena".
     */
    public IEnumerator oscurecerPantallaYCambiarEscena(string escena)
    {
        float fadeSpeed = 087.45E-2f;
        Debug.Log("oscurecer");
        if(SceneController.instance.CheckpointPuerta != "")
        {         
            objectColor = blackScreen.GetComponent<Image>().color;
            float fadeAmount;        
            while (blackScreen.GetComponent<Image>().color.a <1)
            {            
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = objectColor;
                yield return null;              
            }
            SceneController.instance.cambiarEscena(escena);
        }             
    }
    /**
     * Esta funci?n oscurece la pantalla por medio de una corutina. Es llamada desde la funci?n update como una corutina para que cada frame, la
     * pantalla se vaya volviendo gradualmente negra.
     */ 
    public IEnumerator oscurecerPantalla()
    {
        float fadeSpeed = 087.45E-2f;
        objectColor = blackScreen.GetComponent<Image>().color;
        float fadeAmount;
        while (blackScreen.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = objectColor;
            yield return null;
        }

    }
    /**
     * Esta funci?n aclarece la pantalla para terminar una transici?n. Es llamada desde la funci?n update como una corutina
     * para que cada frame la pantalla se vaya aclarando gradualmente hasta que el jugador recupere visibilidad.
     */
    public IEnumerator aclararPantalla(float fadeSpeed = 087.45E-2f)
    {
        Debug.Log("aclarar");
        objectColor = blackScreen.GetComponent<Image>().color;
        float fadeAmount;        
        while (blackScreen.GetComponent<Image>().color.a > 0)
        {            
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = objectColor;
            yield return null;
            
        }
    }
}