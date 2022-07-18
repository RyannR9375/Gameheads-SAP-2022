using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    //CALLING SCRIPTS
    public Enemy enemy;

    //DECLARING VARIABLES;
    float maxHealth;
    float currentHealth;
    float moveSpeed;
    private Rigidbody2D rb;

    //AWAKE FUNCTION TO GET RIGIDBODY OF THE ENEMY TO APPLY 'BOUNCINESS'
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// MINOR BUG - IF PLAYER COLLIDES WITH THE BOUNCING ENEMY OFF THE WALL, THE PLAYER WILL SLOWLY BE MOVED BACK UNTIL THEY HIT A WALL THEMSELVES.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ENEMY BOUNCING OFF WALLS (QUALITY OF LIFE)
        float bounciness = 0f;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map")) // replace check with whatever
        {
            bounciness = 0.1f;
        }

        rb.velocity += collision.relativeVelocity * bounciness;
    }

    private void Update()
    {
        //movement
    }

}
