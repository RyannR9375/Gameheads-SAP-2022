using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileShooter : MonoBehaviour
{
	public GameObject prefabProjectile;
	public float speed;
	public float timeBtwShots;
	public float startTimeBtwShots;
	public float shootDistance;
	public float retreatDistance;
	public float stoppingDistance;
	public float damage;
	public Transform player;

    private void Awake()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}


    public void Update()
    {
		if (Vector2.Distance(transform.position, player.transform.position) < shootDistance)
		{
			ProjectileAttack();
		}

		if(Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
        {
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
		else if(Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
        {
			transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }

    public void ProjectileAttack()
	{
		if (timeBtwShots < 0)
		{
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

	public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			Player playerScript = player.GetComponent<Player>();
			playerScript.playerGetHit(damage);
        }
    }
}
