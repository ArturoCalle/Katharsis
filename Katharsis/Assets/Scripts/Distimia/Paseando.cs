using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paseando : State
{
    public Enfadado buscarTrompi;
    public bool seAltera;
    public override State RunCurrentState()
    {
        if (seAltera)
        {
            return buscarTrompi;
        }
        else
        {
            return this;
        }
    }
}
