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
        Debug.Log(Timeline.duration.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Timeline.time.ToString());
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
    }
}
