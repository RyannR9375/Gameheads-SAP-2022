using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ScriptableObject
{
    //DECLARING VARIABLES
    public Sprite projectileSprite;
    public float activeTime;
    public float cooldownTime;



    //SETTING 'ACTIVATE' & 'BEGINCOOLDOWN' FUNCTIONS FOR TYPE 'PROJECTILE' SCRIPTS.
    public virtual void Activate(GameObject shooter, GameObject player)
    {

    }

    public virtual void BeginCooldown(GameObject parent)
    {

    }
}
