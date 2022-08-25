using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collected(); 
            Player player = collision.GetComponent<Player>();
            player.currentHealth += 10f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collected();
        }
    }

    protected virtual void Collected()
    {
        //override
        Destroy(this.gameObject);
    }
}
