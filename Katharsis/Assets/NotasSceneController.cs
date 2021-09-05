using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasSceneController : MonoBehaviour
{
    public List<Nota> notas = new List<Nota>();
    public static NotasSceneController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void verificarNotas()
    {
        List<Recolectable> r = InventarioController.instance.getRecolectables();
        for (int i = 0; i < r.Count; i++)
        {
           
           Debug.Log(r[i].getNombre() +" "+ r[i].getRecolectado());   
        }
    }
}
