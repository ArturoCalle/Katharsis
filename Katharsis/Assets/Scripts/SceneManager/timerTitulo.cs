using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timerTitulo : MonoBehaviour
{
    public float tiempo;
    Color objectColor;
    public Image blackScreen;
    bool fadedIn;
    // Start is called before the first frame update
    void Start()
    {
        fadedIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!fadedIn)
        {
            fadedIn=fadeIn();   
        }
        else
        {
            tiempo -= Time.deltaTime;
            if (tiempo <= 0)
            {
                fadeOut();
                if(blackScreen.GetComponent<Image>().color.a >= 1)
                {
                    SceneManager.LoadScene("Pantalla Principal");
                }
            }
        }
        
    }
    private void fadeOut()
    {
        float fadeSpeed = 087.45E-2f;
    
        objectColor = blackScreen.GetComponent<Image>().color;
        float fadeAmount;
        fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        blackScreen.GetComponent<Image>().color = objectColor;
    }
    private bool fadeIn()
    {
        float fadeSpeed = 087.45E-2f;

        objectColor = blackScreen.GetComponent<Image>().color;
        float fadeAmount;
        fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        blackScreen.GetComponent<Image>().color = objectColor;
        if (blackScreen.GetComponent<Image>().color.a > 0)
        {
            
            return false;
        }
        else
        {
            return true;
        }
    }
}
