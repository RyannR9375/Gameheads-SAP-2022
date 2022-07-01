using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Release_Ability", menuName = "ScriptableObjects/Abilities/Release Ability")]
public class ReleaseAbility : Ability
{
    //DECLARING VARIABLES
    [SerializeField] private float _knockbackTime;
    [SerializeField] private float _releasePush;
    [SerializeField] private float _releaseRadius;

    public float knockbackTime { get { return _knockbackTime; }}
    public float releasePush { get { return _releasePush; } }
    public float releaseRadius { get { return _releaseRadius; } }

    public override void Activate(GameObject parent)
    {
        CircleCollider2D release = parent.AddComponent<CircleCollider2D>();

        release.radius = releaseRadius;
        release.isTrigger = true;
        release.tag = "Release";
        Debug.Log("RELEASE");
    }

    //COOLDOWN FUNCTION
    public override void BeginCooldown(GameObject parent)
    {
        CircleCollider2D release = parent.GetComponent<CircleCollider2D>();
        release.tag = "Untagged";
        Destroy(release);
    }
}
