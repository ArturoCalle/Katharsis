using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController: MonoBehaviour
{
    public List<Transform> targets;
    private int last;
    public static TargetController instance;

    private void Start()
    {
        instance = this;
        last = 0;
    }
    

    public Transform getNext()
    {
        if(last < targets.Count)
        {
            last++;
            Debug.Log("El ultimo es " + last);
            return targets[last];
        }
        else
        {
            Debug.Log("No hay mas targets " + last);
            return targets[0];
        }
    }

    public Transform getLast()
    {
        return targets[last];
    }
    public int getLastPersistencia()
    {
        return last;
    }
    public void cargarLastTarget(int last)
    {
        this.last = last;
    }
}
