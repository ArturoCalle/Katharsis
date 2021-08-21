using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    public bool corner;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Escalable")
        {
            corner = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        corner = false;
    }
}
