using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //collider.GetComponent<Health>() ! =null
        if (other.CompareTag("Enemy"))
        {
            //Health health = collider.GetComponent<Health>();
            //health.Damage(damage);
            Destroy(other.gameObject);
        }
    }
}