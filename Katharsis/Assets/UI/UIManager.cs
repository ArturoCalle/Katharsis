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
    public Button BotonReanudar;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        BotonReanudar.enabled = true;
        BotonReanudar.onClick.AddListener(clickeando);
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
    public void Reanudar()
    {
        PlayerControls.instance.PauseUnpause();
        Debug.Log(Cursor.visible);
        Debug.Log("Entra a función reanudar");
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
    public void clickeando()
    {
        Debug.Log("Se está clickeando");
    }
    
}
