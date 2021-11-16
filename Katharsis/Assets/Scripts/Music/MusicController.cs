using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    //public AudioSource audiointro;
    public AudioClip audioIntro;
    public AudioClip audioAmbiente;
    public AudioSource audioSource;
    private string escena;
   //public AudioSource audioAmbiente;

   private void Awake()
   {
        GameObject[] musicController = GameObject.FindGameObjectsWithTag("MusicSource");
        if (musicController.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
   }
    /**
     * Según la escena que esté activa, cambia la musica si es que la pista no está corriendo ya
     */
    private void Update()
    {
        if(escena != SceneManager.GetActiveScene().name)
        {
            escena = SceneManager.GetActiveScene().name;
            switch(escena)
            {

                 case "Pantalla Principal":
                    if (audioSource.clip == audioIntro)
                        break;
                    audioSource.clip = audioIntro;
                    audioSource.Play();
                    break;
                    case "Creditos":
                    if (audioSource.clip == audioIntro)
                        break;
                    audioSource.clip = audioIntro;
                    audioSource.Play();
                    break;

                    case "Titulo":
                    if (audioSource.clip == audioIntro)
                        break;
                    audioSource.clip = audioIntro;
                    audioSource.Play();
                    break;

                    case "Sala":
                    if (audioSource.clip == audioAmbiente)
                        break;
                    audioSource.clip = audioAmbiente;
                    audioSource.Play();
                    break;
                    case "Comedor":
                    if (audioSource.clip == audioAmbiente)
                        break;
                    audioSource.clip = audioAmbiente;
                    audioSource.Play();
                    break;
                    case "Cocina":
                    if (audioSource.clip == audioAmbiente)
                        break;
                    audioSource.clip = audioAmbiente;
                    audioSource.Play();
                    break;
                    default:
                        break;
            }
        }    
    }
}
