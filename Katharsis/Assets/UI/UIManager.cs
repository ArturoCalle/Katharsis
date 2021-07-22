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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            pauseScreen.GetComponent<menuPausa>().cambiarSeleccion(-1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pauseScreen.GetComponent<menuPausa>().cambiarSeleccion(1);
        }
    }
    public void Reanudar()
    {
        PlayerControls.instance.PauseUnpause();
    }
    public void AbrirOpciones()
    {

    }
    public void CerrarOpciones()
    {

    }
    public void VolverAlMenu()
    {

    }
    
    
}
