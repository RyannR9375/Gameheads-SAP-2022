using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resilience : Collectable
{
    [SerializeField] int ResilienceValue = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("collected");   
            CollectedResiliance();
        }
    }
    public void CollectedResiliance()
    {
        print("collected");
        GameManager.MyInstance.AddItems(ResilienceValue);
        Destroy(this.gameObject);
    }
}
