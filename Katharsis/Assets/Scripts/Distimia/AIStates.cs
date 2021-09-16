using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStates : MonoBehaviour
{
    enum Estados { activo, inactivo, enfadado, busqueda };
    private Estados actual;
    private void Start()
    {
        actual = Estados.inactivo;
    }

   

}
