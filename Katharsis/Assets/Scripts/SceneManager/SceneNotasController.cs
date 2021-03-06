using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNotasController: MonoBehaviour
{
    public List<GameObject> notas = new List<GameObject>();
    Stack<string> notasPorQuitar = new Stack<string>();
    public static SceneNotasController instance;
    public bool cargar;

    // Start is called before the first frame update
    void Awake()
    {
       
        instance = this;
        cargar = false;

    }
    /**
     * Verifica las notas una vez, no se hace en el metodo start porque debe esperar a que la persistencia cargue la partida de ser necesario
     * Quita las notas ya recolectadas de la escena
     */
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
    /*
     * Revisa que el inventario ya haya sido llenado, de no ser asi, la variable cargar queda en false y debe volver a realizar el proceso
     */
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
            cargar = false;
        }
    }
  
    /*
     * recoge las notas de la escena que deba reocoger
     */
    public void recogerNota(string nombre)
    {
        foreach (GameObject n in notas)
        {
            if(n!= null)
            {
                if(n.GetComponent<NotaUI>().nombre == nombre)
                {
                    n.GetComponent<NotaUI>().recolectado = true;
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
