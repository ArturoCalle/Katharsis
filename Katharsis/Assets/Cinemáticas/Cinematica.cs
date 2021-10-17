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
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Timeline.state.ToString() != "Played")
        {
            reproducido = false;
            Timeline.Stop();
            cutscenecam.SetActive(false;
        }
    }
    public void reproducir()
    {
        cutscenecam.SetActive(true);
        Timeline.Play();
    }
}
