using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private float damage;

    GameObject boss;
    EnemyBossTest bossScript;
    GameObject player;
    Player playerScript;

    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");
        if (boss)
        {
            bossScript = boss.GetComponent<EnemyBossTest>();
        }

        //player = this.transform.parent.gameObject;
        playerScript = player.GetComponent<Player>();
        damage = playerScript.attackDamage;
    }

    private void Update()
    {
        this.transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //collider.GetComponent<Health>() ! =null
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Hello");
            }
        }

        if (other.CompareTag("Boss"))
        {
            bossScript.TakeDamage(this.damage);
        }

        if (other.CompareTag("Rock"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //collider.GetComponent<Health>() ! =null
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Hello");
            }
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            bossScript.TakeDamage(this.damage);
        }

        if (other.gameObject.CompareTag("Rock"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}

