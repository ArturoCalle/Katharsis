using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggerController : MonoBehaviour
{
    public List<GameObject> notas = new List<GameObject>();
    List<Recolectable> quitarDeEscena;
    public static SceneTriggerController instance;
    public bool cargar;

    // Start is called before the first frame update
    void Start()
    {
        quitarDeEscena = new List<Recolectable>();
        cargar = true;
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        /*
       if(cargar)
       {
            verificarNotas();
            for (int i = 0; i < quitarDeEscena.Count; i++)
            {
                
                Debug.Log(quitarDeEscena[i].getNombre());
                recogerNota(quitarDeEscena[i].getNombre());
                  
            }
       }*/
            
    }
    public void verificarNotas()
    {
        List<Recolectable> r = InventarioController.instance.getRecolectables();
        for (int i = 0; i < r.Count; i++)
        {
           if(r[i].getRecolectado()&&(SceneController.instance.getCurrentSceneName() == r[i].getEscena()))
           {    
                quitarDeEscena.Add(r[i]);
           }
        }
    }
  
    public void recogerNota(string nombre)
    {
        foreach(GameObject n in notas)
        {
            if(n!= null)
            {
                n.GetComponent<Nota>().plane.SetActive(false);
                cargar = false;
            }
        }
    }
}
