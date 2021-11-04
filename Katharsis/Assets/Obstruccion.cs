using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruccion : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (InventarioController.instance.getRecolectable(0).getRecolectado())
        {
            Destroy(gameObject);
        }
    }
}
