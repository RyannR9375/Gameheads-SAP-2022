using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Projectiles/Bullet")]
public class Bullet : Projectile
{
    //DECLARING VARIABLES
    private Projectile projectileScript;
    public GameObject bulletProjectile;
    public float bulletSpeed;

    public override void Activate(GameObject shooter, GameObject target)
    {
        Rigidbody2D enemy = shooter.GetComponent<Rigidbody2D>();
        //CircleCollider2D enemyCircle = shooter.AddComponent<CircleCollider2D>();
        Rigidbody2D player = target.GetComponent<Rigidbody2D>();

        Debug.Log("activate");
        Debug.Log(player.transform.position);
        Instantiate(bulletProjectile, shooter.transform.position, Quaternion.identity);
        bulletProjectile.transform.position = Vector2.MoveTowards(shooter.transform.position, player.transform.position, bulletSpeed * Time.fixedDeltaTime);

    }

    public override void BeginCooldown(GameObject parent)
    {
        Debug.Log("cooldown");
        Destroy(bulletProjectile);
    }
}
