using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    //DECLARING VARIABLES
    //public new string name;
    public float activeTime;
    public float cooldownTime;

    //SETTING 'ACTIVATE' & 'BEGINCOOLDOWN' FUNCTIONS FOR TYPE 'ABILITY' SCRIPTS.
    public virtual void Activate(GameObject parent)
    {

    }

    public virtual void BeginCooldown(GameObject parent)
    {

    }
}
