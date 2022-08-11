using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBossTest : MonoBehaviour
{
	#region Components
	Player playerScript;
	private Rigidbody2D playerRB;
	private Transform player;

	public enum Stage
	{
		WaitingToStart,
		stage1,
		stage2,
		stage3
	}

	private Stage stage;
	#endregion

	#region Boss Stats
	[Header("Boss Stats")]
	public float bossDamage;
	public float attackRange;
	public float speed;
	public float health;
	public float stoppingDistance;

	[Header("Projectile Ability Stats")]
	public GameObject prefabProjectile;
	public float timeBtwShots;
	public float startTimeBtwShots;

	[Header("Pull Ability Stats")]
	public float pullAttackDistance;
	public float pullForce;
	public float pullActiveTime;
	public float pullCooldown;

	private float pullActive;

	[Header("Absorb Ability Stats")]
	public float resizeAmt;
	public float maxResize;
	public float absorbDistance;
	public float absorbStrength;
	public float absorbActiveTime;
	public float absorbCooldown;

	private float maxScaleMag;
	private float absorbActive;

	private Vector3 scaleChange;
	private Vector3 maxScale;
	#endregion

	#region Unity Callback Functions

	private void Awake()
	{
		stage = Stage.WaitingToStart;
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerRB = player.GetComponent<Rigidbody2D>();
		playerScript = player.GetComponent<Player>();

		timeBtwShots = startTimeBtwShots;
		pullActive = pullActiveTime;
		absorbActive = absorbActiveTime;

		//Starts Stage 1
		StartNextStage();
	}

	private void Update()
	{
		DoWhatInThisPhase();

		if (health <= 0)
        {
			SceneManager.LoadScene("WinningEndScene");
        }
	}

	#endregion

	#region Collision Functions

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			DoDamage();
		}
	}

	#endregion

	#region Boss Abilities
	public void ProjectileAttack()
	{
		if (timeBtwShots < 0)
		{
			Debug.Log("Simple Attack Called");

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

	public void PullAttack()
	{
		//CHECKS IF PLAYER IS IN DISTANCE, IF pullActiveTime IS GREATER THAN ZERO
		if (player != null && pullActiveTime > 0 && Vector2.Distance(transform.position, player.transform.position) < pullAttackDistance)
		{
			Debug.Log("Pull Attack Called");

			//Gets difference of distance, and pulls the player in.
			Vector2 difference = (transform.position - player.transform.position);
			difference = (difference.normalized * pullForce);
			playerRB.AddForce(difference, ForceMode2D.Force);
		}

		//Simple and undone possible cooldown mechanic for boss ability?
		if (pullActiveTime > 0)
		{
			pullActiveTime -= Time.deltaTime;
		}
		else if (pullActiveTime <= 0)
		{
			pullActiveTime = pullActive;
		}
	}

	public void AbsorbAttack()
	{
		if (player != null && Vector2.Distance(transform.position, player.transform.position) < absorbDistance)
		{
			Debug.Log("Absorb Ability Called");
			scaleChange = new Vector3(resizeAmt * 0.0001f, resizeAmt * 0.0001f, 0f);
			maxScale = transform.localScale * maxResize;
			maxScaleMag = maxScale.magnitude;

			//Checks if the boss has hit the max scale, if not, keep increasing their size
			if(transform.localScale.magnitude < maxScaleMag)
            {
				transform.localScale += scaleChange;
            }

			//damage script
			playerScript.TakeDamage(0, absorbStrength);
			
		}

		
	}
	#endregion

	#region Functions
	void DoDamage()
	{
		if (playerScript != null)
		{
			playerScript.TakeDamage(playerScript.knockTime, bossDamage);
		}
	}

	void TakeDMGDebug()
	{
		//Used for debugging for now.
		if (Input.GetKeyDown(KeyCode.E))
		{
			health -= 10;
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
	}

	void DoWhatInThisPhase()
	{
		BossDamaged();

		if (stage == Stage.stage1)
		{
			ProjectileAttack();
			AbsorbAttack();
		}

		if (stage == Stage.stage2)
		{
			PullAttack();
			ProjectileAttack();
		}
	}
	#endregion

	#region Enums

	private void StartNextStage()
    {
        switch (stage) 
		{ 
		default: 

			case Stage.WaitingToStart:
				stage = Stage.stage1;
				break;
			case Stage.stage1:
				stage = Stage.stage2;
				break;
			case Stage.stage2:
				stage = Stage.stage3;
				break;
		}
    }

	public void BossDamaged()
	{
		switch (stage)
		{
		default:
			case Stage.stage1:
				if (health <= 70)
				{
					gameObject.SetActive(true);
					StartNextStage();
				}
				break;

			case Stage.stage2:
				if (health <= 45)
				{
					StartNextStage();
				}
				break;

			case Stage.stage3:
				if (health <= 0)
				{
					gameObject.SetActive(false);
				}
				break;
		}
	}
	#endregion
}