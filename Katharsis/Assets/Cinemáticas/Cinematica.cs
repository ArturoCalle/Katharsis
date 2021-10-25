using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cinematica : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector Timeline;
    public GameObject cutscenecam;
    public bool reproducido = true;
    public float duracion;
    void Start()
    {
        Debug.Log(Timeline.duration.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {
        duracion -= Time.deltaTime;
        if (duracion <= 0)
        {
            Timeline.Stop();
            cutscenecam.SetActive(false);
            PlayerControls.instance.enabled = true;
            gameObject.SetActive(false);


        }        
        if(Timeline.state.ToString() != "Played")
        {
            reproducido = false;
            Timeline.Stop();
            cutscenecam.SetActive(false);
        }
    }
    public void reproducir()
    {
        cutscenecam.SetActive(true);
        Timeline.Play();
        PlayerControls.instance.enabled = false;
    }
}
