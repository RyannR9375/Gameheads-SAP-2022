using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shine_Ability", menuName = "ScriptableObjects/Abilities/Shine Ability")]
public class ShineAbility : Ability
{
    /// <summary>
    /// MAKE A VARIABLE THAT GETS COMPONENT OF RIGIDBODY,GAMEOBJECT, ETC... FROM 'PlayerMovement' SCRIPT AND USE THAT HERE INSTEAD OF NEEDING TO MAKE AN ABILITYPHYSICS SCRIPT?
    /// </summary>

    //DECLARING VARIABLES
    [SerializeField] private float _knockbackTime;
    //[SerializeField] private float _pullTime; POTENTIALLY MAKING THIS 
    [SerializeField] private float _shinePush;
    [SerializeField] private float _shineRadius;

    public float knockbackTime{ get { return _knockbackTime; }}
    //public float pullTime{ get { return _pullTime; }}
    public float shinePush{ get { return _shinePush; }}
    public float shineRadius { get { return _shineRadius; } }

    //ACTIVATING ABILITIES FUNCTION
    public override void Activate(GameObject parent)
    {
        CircleCollider2D shine = parent.AddComponent<CircleCollider2D>();

        shine.radius = shineRadius;
        shine.isTrigger = true;
        shine.tag = "Shine";
    }

    //COOLDOWN FUNCTION
    public override void BeginCooldown(GameObject parent)
    {
        CircleCollider2D shine = parent.GetComponent<CircleCollider2D>();
        shine.tag = "Untagged";
        Destroy(shine);
    }
}
