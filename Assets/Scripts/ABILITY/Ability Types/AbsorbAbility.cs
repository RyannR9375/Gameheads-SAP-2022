using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Absorb_Ability", menuName = "ScriptableObjects/Abilities/Absorb Ability")]
public class AbsorbAbility : Ability
{
    //DECLARING VARIABLES
    //public PlayerMovement player;
    public float absorbRadius;
    public int absorbValue;
    //public Slider slider;

    private ParticleSystem playerParticles;

    //ACTIVATING ABILITIES FUNCTION
    public override void Activate(GameObject parent)
    {
        CircleCollider2D absorb = parent.AddComponent<CircleCollider2D>();
        //GameObject player = player.GetComponent<GameObject>();

        absorb.radius = absorbRadius;
        absorb.tag = "Absorb";
        absorb.isTrigger = true;
        
    }

    //COOLDOWN FUNCTION
    public override void BeginCooldown(GameObject parent)
    {
        CircleCollider2D absorb = parent.GetComponent<CircleCollider2D>();
        absorb.tag = "Untagged";

        Destroy(absorb);
    }

}
