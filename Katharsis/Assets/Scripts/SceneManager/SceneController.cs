using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public bool pausa;
    void Start()
    {
        instance = this;
        pausa = false;
    }

    public void cambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void pause()
    {
        if(pausa)
        {
             UIController.instance.desactivarPaneles();
             freeze(false);
             Cursor.visible = false;
             Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIController.instance.pausar();
            freeze(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void freeze(bool congelar)
    {
        pausa = congelar;
        if (congelar)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
