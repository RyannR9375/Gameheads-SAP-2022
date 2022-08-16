using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    #region Declaring Variables
    public float speed;

    private Transform player;
    private Vector2 target;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
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
