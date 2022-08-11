using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resilience : Collectable
{
    [SerializeField] int ResilienceValue = 1;

    protected override void Collected()
    {
        GameManager.MyInstance.AddItems(ResilienceValue);
        Destroy(this.gameObject);
    }
}
