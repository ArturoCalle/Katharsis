using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    Color objectColor;
    public Image blackScreen;
    public bool fadeOut()
    {
        float fadeSpeed = 087.45E-2f;

        objectColor = blackScreen.GetComponent<Image>().color;
        float fadeAmount;
        fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        blackScreen.GetComponent<Image>().color = objectColor;
        if (blackScreen.GetComponent<Image>().color.a < 1)
        {

            return false;
        }
        else
        {
            return true;
        }
    }
    
    public IEnumerator fadeOutCorutine()
    {
        float fadeSpeed = 087.45E-2f;

        objectColor = blackScreen.GetComponent<Image>().color;
        float fadeAmount;
        while(blackScreen.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = objectColor;
            yield return null;
        }
    }
    public bool fadeIn()
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
    public float getTransparency()
    {
        return blackScreen.GetComponent<Image>().color.a;
    }
}
