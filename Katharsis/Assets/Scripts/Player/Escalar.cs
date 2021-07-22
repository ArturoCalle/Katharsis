using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalar : MonoBehaviour
{
    bool colision;

    private void OnTriggerEnter(Collider col)
    {
       
    }
    //si  la colision de un objeto es una esquina se puede escalar
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Escalable")
        {
            colision = true;
        }
        if(col.gameObject.name != "Jugador")
        {
           
        }
        else
        {
            colision = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        colision = false;
    }
    
    public bool isActive()
    {
        return colision;
    }
    
   
}
