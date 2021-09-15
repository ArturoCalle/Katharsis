using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggerController : MonoBehaviour
{
    public List<GameObject> notas = new List<GameObject>();
    Stack<string> notasPorQuitar = new Stack<string>();
    List<Recolectable> quitarDeEscena;
    public static SceneTriggerController instance;
    public bool cargar;

    // Start is called before the first frame update
    void Awake()
    {
        quitarDeEscena = new List<Recolectable>();
        instance = this;
        cargar = false;

    }

    // Update is called once per frame
    void Update()
    {
       if(cargar == false)
        {
            verificarNotas();
            cargar = true;
        }
       if(notasPorQuitar.Count != 0)
       {
            string nombre = notasPorQuitar.Pop();
            recogerNota(nombre);
       }
    }
    public void verificarNotas()
    {
        if(InventarioController.instance.getRecolectables() != null)
        {

            List<Recolectable> r = InventarioController.instance.getRecolectables();
            for (int i = 0; i < r.Count; i++)
            {
               if(r[i].getRecolectado()&&(SceneController.instance.getCurrentSceneName() == r[i].getEscena()))
               {
                
                    notasPorQuitar.Push(r[i].getNombre());
               }
            }
        }
        else
        {
            Debug.Log("holi");
            cargar = false;
        }
    }
  
    public void recogerNota(string nombre)
    {
        foreach (GameObject n in notas)
        {
            if(n!= null)
            {
                if(n.GetComponent<NotaUI>().nombre == nombre)
                {
                    
                    n.transform.GetChild(0).gameObject.SetActive(false);
                    cargar = false;

                }
                
            }
            else
            {
                
                notasPorQuitar.Push(nombre);
                break;
            }
        }
    }
}
