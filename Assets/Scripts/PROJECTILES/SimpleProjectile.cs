using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    #region Declaring Variables
    Player playerScript;
    public float damage;
    public float speed;
    public float lifeTime;
    private Transform player;
    private Vector2 target;
    private Vector2 thisVector2;
    private float speedOfObject;
    private Vector3 oldPosition;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.gameObject.GetComponent<Player>();

        target = new Vector2(player.position.x, player.position.y);
    }

    public void FixedUpdate()
    {
        speedOfObject = Vector3.Distance(oldPosition, transform.position) * 100f;
        oldPosition = transform.position;
        thisVector2 = new Vector2(transform.position.x, transform.position.y);
        lifeTime -= Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (lifeTime <= 0)
        {
            DestroyProjectile();
        }

        if (speedOfObject == 0)
        {
            DestroyProjectile();
        }
    }
    #endregion

    #region Collision Functions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
            playerScript.TakeDamage(playerScript.knockTime, damage);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            DestroyProjectile();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DestroyProjectile();
            playerScript.TakeDamage(playerScript.knockTime, damage);
        }

        if (thisVector2 == Vector2.zero)
        {
            DestroyProjectile();
        }

        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            DestroyProjectile();
        }
    }

    
    #endregion

    #region Other Functions
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    #endregion

}
