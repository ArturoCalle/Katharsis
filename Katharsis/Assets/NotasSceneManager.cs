using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasSceneManager : MonoBehaviour
{
    public List<Nota> notas = new List<Nota>();
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (r[i].getRecolectado())
            {
                Debug.Log(r[i].getNombre());
            }
        }
    }
}
