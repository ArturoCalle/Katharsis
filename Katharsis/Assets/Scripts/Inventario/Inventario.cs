using System.Collections;
using System.Collections.Generic;


public class Inventario
{
    private List<Recolectable> recolectables;
    public Inventario()
    {
        recolectables = new List<Recolectable>();
    }
    public void agregarRecolectable(Recolectable nuevo)
    {
        recolectables.Add(nuevo);
    }
}
