using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Partida
{
    public bool[] notasRecogidas; //se guardan todas las notas, la posicion de la nota es el identificador y se guarda si esta o no recogida
    public string[] nombreNotas;
    public char[] tipoNotas;
    public string[] escenaNotas;

    public bool[] sceneTriggerRecolectados;
    public int[] sceneTriggerNum;
    public string[] sceneTriggerEscena;
    public string[] sceneTriggerNombre;

    //AI
    public float[] distimia; //ultima posicion de distimia x, y, z
    public int targetAI;


    //TODO int [] bocinas; //Estos son los checkpont especiales entre puertas. Ej bocina
    public float [] LastcheckpointPos; // posicion del ultimo checkpoint
    public string LastCheckpoint;
    public string CheckpointPuerta;
    public string escena; //tener cuidados de que la escena sea la misma del checkpoint

    public Partida(List<Recolectable> r, CheckpointSingle lc, string escena, string CheckpointPuerta, List<Recolectable> t)
    {
        sceneTriggerNum = new int[t.Count];
        sceneTriggerRecolectados = new bool[t.Count];
        sceneTriggerEscena = new string[t.Count];
        sceneTriggerNombre = new string[t.Count];

        this.escena = escena;
        //TODO cargar checkpoint
        this.CheckpointPuerta = CheckpointPuerta;
        notasRecogidas = new bool[r.Count];
        nombreNotas = new string[r.Count];
        tipoNotas = new char[r.Count];
        escenaNotas = new string[r.Count];
        int i = 0;

        foreach (Recolectable h in r)
        {
            notasRecogidas[i] = h.getRecolectado();
            nombreNotas[i] = h.getNombre();
            tipoNotas[i] = h.getTipo();
            escenaNotas[i] = h.getEscena();
            i++;
        }

        for (int j = 0; j < t.Count; j++)
        {
            sceneTriggerRecolectados[j] = t[j].getRecolectado();
            sceneTriggerNum[j] = t[j].getNumNota();
            sceneTriggerEscena[j] = t[j].getEscena();
            sceneTriggerNombre[j] = t[j].getNombre();

        }
        //TODO guardar checkpoint
        //personajes
        /*
        this.distimia = new float[3];
        this.distimia[0] = distimia.position.x;
        this.distimia[1] = distimia.position.y;
        this.distimia[2] = distimia.position.z;*/

    }

}
