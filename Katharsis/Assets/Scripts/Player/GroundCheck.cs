using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool grounded;

    private void OnTriggerEnter(Collider col)
    {

    }
    //si  la colision de un objeto es una esquina se puede escalar
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name != "trompi" && col.tag != "Checkpoint" && col.gameObject.name != "Jugador" && col.tag != "Trigger")
        {
            grounded = true;
        }
        
    }
    private void OnTriggerExit(Collider col)
    {
        grounded = false;
    }

    public bool isGrounded()
    {
        return grounded;
    }
}
