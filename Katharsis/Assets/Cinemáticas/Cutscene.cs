using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    public GameObject cutsceneCam;
    public PlayableDirector timeline;
    public GameObject camarasVirtuales;
    
    void Awake()
    {
        timeline.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
