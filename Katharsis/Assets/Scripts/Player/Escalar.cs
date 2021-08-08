using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalar : MonoBehaviour
{
    bool colision;
    public Corner corn;
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
    }
    private void OnTriggerExit(Collider col)
    {
        colision = false;
    }
    
    public bool isActive()
    {
        return colision;
    }
    public bool isCorner()
    {
        return corn.corner;
    }

}
