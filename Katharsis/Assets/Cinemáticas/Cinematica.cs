using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/**
 * Esta clase se utiliza para controlar que las cinem�ticas (cutscenes) que se reproducen dentro del juego se reproduzcan.
 * Utiliza el componente de Unity "Cinemachine" el cual permite usar gameobjects de tipo timeline y a�adir c�maras virtuales a la escena.
*/ 
public class Cinematica : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector Timeline; //Timeline asignada desde el inspector
    public GameObject cutscenecam; //C�mara asignada desde el inspector
    public bool reproducido = true;
    public float duracion; //Duraci�n de la cinem�tica
    void Start()
    {
        
        
    }

    /**
     * En esta clase, el update se utiliza para controlar la duraci�n de las cinem�ticas la cual est� guardada en la variable "duracion"
     * Al reproducir una cinem�tica, se desactiva el PlayerControls para evitar que Trompi se pueda mover, al terminar la cinem�tica se vuelve a activar
     */ 
    void Update()
    {
        duracion -= Time.deltaTime;
        if (duracion <= 0)
        {
            Timeline.Stop();
            cutscenecam.SetActive(false); //Desactivar c�mara
            PlayerControls.instance.enabled = true; //Activar PlayerControls
            gameObject.SetActive(false);


        }        
        if(Timeline.state.ToString() != "Played") //
        {
            reproducido = false;
            Timeline.Stop();
            cutscenecam.SetActive(false);
        }
    }
    /**
     * Esta funci�n reproduce la cinem�tica una vez es llamada. De igual manera, desactiva el PlayerControls para evitar que trompi
     * se pueda mover.
     */ 
    public void reproducir()
    {
        cutscenecam.SetActive(true);
        Timeline.Play();
        PlayerControls.instance.enabled = false;
    }
}
