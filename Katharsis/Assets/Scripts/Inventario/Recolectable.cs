using System.Collections;
using System.Collections.Generic;


public class Recolectable
{
    private string nombre;
    private string escena;
    private char tipo;
    private bool recolectado;
    private int numNota;
    public Recolectable(string nombre, string escena, char tipo, bool recolectado, int numNota)
    {
        this.nombre = nombre;
        this.escena = escena;
        this.tipo = tipo;
        this.recolectado = recolectado;
        this.numNota = numNota;
    }
    public Recolectable()
    {
        this.nombre = "???";
        this.escena = "???";
        recolectado = false;
    }

    public string getNombre()
    {
        return this.nombre;
    }
    public string getEscena()
    {
        return this.escena;
    }
    public char getTipo()
    {
        return this.tipo;
    }
    public bool getRecolectado()
    {
        return this.recolectado;
    }


}
