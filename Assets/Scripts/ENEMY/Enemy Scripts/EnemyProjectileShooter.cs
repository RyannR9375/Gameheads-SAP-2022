using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileShooter : MonoBehaviour
{
	public GameObject prefabProjectile;
	public float timeBtwShots;
	public float startTimeBtwShots;
	public float shootDistance;


    public void Update()
    {
		if (Vector2.Distance(transform.position, transform.position) < shootDistance)
		{
			ProjectileAttack();
		}
    }

    public void ProjectileAttack()
	{
		if (timeBtwShots < 0)
		{
			Debug.Log("Projectile Attack Called");

			//Creates Prefab every timeBtwShots
			Instantiate(prefabProjectile, transform.position, Quaternion.identity);
			prefabProjectile.SetActive(true);
			timeBtwShots = startTimeBtwShots;
		}
		else
		{
			timeBtwShots -= Time.deltaTime;
		}
	}
}
