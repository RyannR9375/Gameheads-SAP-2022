using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    GameObject boss;
    EnemyBossTest bossScript;
    GameObject player;
    Player playerScript;

    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossScript = boss.GetComponent<EnemyBossTest>();
        player = this.transform.parent.gameObject;
        playerScript = player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //collider.GetComponent<Health>() ! =null
        if (other.CompareTag("Enemy"))
        {
            //Health health = collider.GetComponent<Health>();
            //health.Damage(damage);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Boss"))
        {
            bossScript.TakeDamage(playerScript.playerDamage);
        }
    }
}