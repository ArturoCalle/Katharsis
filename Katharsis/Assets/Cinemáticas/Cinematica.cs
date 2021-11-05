using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/**
 * Esta clase se utiliza para controlar que las cinemáticas (cutscenes) que se reproducen dentro del juego se reproduzcan.
 * Utiliza el componente de Unity "Cinemachine" el cual permite usar gameobjects de tipo timeline y añadir cámaras virtuales a la escena.
*/ 
public class Cinematica : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector Timeline; //Timeline asignada desde el inspector
    public GameObject cutscenecam; //Cámara asignada desde el inspector
    public bool reproducido = true;
    public float duracion; //Duración de la cinemática
    void Start()
    {
        
        
    }

    /**
     * En esta clase, el update se utiliza para controlar la duración de las cinemáticas la cual está guardada en la variable "duracion"
     * Al reproducir una cinemática, se desactiva el PlayerControls para evitar que Trompi se pueda mover, al terminar la cinemática se vuelve a activar
     */ 
    void Update()
    {
        duracion -= Time.deltaTime;
        if (duracion <= 0)
        {
            Timeline.Stop();
            cutscenecam.SetActive(false); //Desactivar cámara
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
     * Esta función reproduce la cinemática una vez es llamada. De igual manera, desactiva el PlayerControls para evitar que trompi
     * se pueda mover.
     */ 
    public void reproducir()
    {
        cutscenecam.SetActive(true);
        Timeline.Play();
        PlayerControls.instance.enabled = false;
    }
}
