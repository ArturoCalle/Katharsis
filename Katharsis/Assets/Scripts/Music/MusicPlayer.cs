using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private string escena;
    private GameObject[] musicSource;
    //patron singleton para el controlador de la musica
    private void Awake()
    {
        musicSource = GameObject.FindGameObjectsWithTag("MusicSource");
        if (musicSource.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void cambiarPista()
    {
        musicSource = GameObject.FindGameObjectsWithTag("MusicSource");
    }
}
