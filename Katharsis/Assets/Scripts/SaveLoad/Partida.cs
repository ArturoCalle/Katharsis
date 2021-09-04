using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Partida
{
    public bool[] notasRecogidas; //se guardan todas las notas, la posicion de la nota es el identificador y se guarda si esta o no recogida
    //TODO int [] bocinas; //Estos son los checkpont especiales entre puertas. Ej bocina
    public float [] LastcheckpointPos; // posicion del ultimo checkpoint
    public string escena; //tener cuidados de que la escena sea la misma del checkpoint

    public Partida(List<Recolectable> r, CheckpointSingle lc, string escena)
    {
        this.escena = escena;
        notasRecogidas = new bool[r.Count];
        int i = 0;
        foreach (Recolectable h in r)
        {
            notasRecogidas[i] = h.getRecolectado();
            i++;
        }
        LastcheckpointPos = new float[3];
        LastcheckpointPos[0] = lc.gameObject.transform.position.x;
        LastcheckpointPos[1] = lc.gameObject.transform.position.y;
        LastcheckpointPos[2] = lc.gameObject.transform.position.z;
    }

}
