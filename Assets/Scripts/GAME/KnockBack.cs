using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    //DECLARING VARIABLES
    public float thrust;
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D player = other.GetComponent<Rigidbody2D>();
            if(player != null)
            {
                Vector2 difference = player.transform.position - transform.position;
                difference = difference.normalized * thrust;
                player.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(player));
            }
        }
    }

    
    private IEnumerator KnockCo(Rigidbody2D player)
    {
        if(player != null && player.velocity != Vector2.zero)
        {
            yield return new WaitForSeconds(knockTime);
            player.velocity = Vector2.zero;
        }

    }
}
