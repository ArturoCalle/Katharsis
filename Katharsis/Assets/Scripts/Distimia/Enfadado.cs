using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enfadado : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
