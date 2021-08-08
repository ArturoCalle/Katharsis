using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalar : MonoBehaviour
{
    bool colision;
    bool corner;

    private void OnTriggerEnter(Collider col)
    {
       
    }
    //si  la colision de un objeto es una esquina se puede escalar
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Escalable")
        {
            colision = true;
        }else if (col.gameObject.tag == "corner")
        {
            corner = true;
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
        return corner;
    }


}
