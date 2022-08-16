using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowForBoss : MonoBehaviour
{
    #region Declaring Variables
    public float speed;
    public float followDistance;
    public float lifeTime;
    public Transform player;
    private Vector2 target;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    public void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            DestroyProjectile();
        }

        if (Vector2.Distance(transform.position, transform.position) < followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
