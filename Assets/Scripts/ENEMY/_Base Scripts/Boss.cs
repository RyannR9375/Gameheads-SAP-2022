using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	#region Declaring Variables
	Player playerScript;

	private bool isFlipped = false;
	private bool attackDone = true;
	private int index;
	#endregion

	#region Components
	private Rigidbody2D playerRB;

	public Transform player;
	public GameObject prefabSimpleAttack;

	public List<IEnumerator> Attacks = new List<IEnumerator>();
	public IEnumerator chosenAttack;
	#endregion

	#region Boss Stats
	[Header("Boss Stats")]
	public int bossDamage;
	public float attackRange;
	public float speed;
	public float health;
	public float stoppingDistance;

	[Header("Projectile Ability Stats")]
	public float timeBtwShots;
	public float startTimeBtwShots;

	[Header("Pull Ability Stats")]
	public float pullAttackDistance;
	public float pullForce;
	public float pullActiveTime;
	public float pullCooldown;

	private float pullActive;
	#endregion

	#region Rotate Functions
	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}
    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
		FillAttacksList();
	}

	void Start()
	{
		playerRB = player.GetComponent<Rigidbody2D>();
		playerScript = player.GetComponent<Player>();

		timeBtwShots = startTimeBtwShots;
		pullActive = pullActiveTime;

		index = Random.Range(0, Attacks.Count);
	}

	private void Update()
    {
		#region Attack Cycle (Randomized)
		//SET UP A LIST OF ALL THE COROUTINES,
		//RANDOMIZE THEM, AND THEN GO THROUGH THE LIST,
		//DELETE THAT COROUTINE FROM THE LIST ONCE EXECUTED,
		//IF THE LIST IS DONE,
		//RESET THE LIST AND RANDOMIZE IT AGAIN
		//UNTIL BOSS DEAD.

		if (Attacks.Count != 0)
		{
			chosenAttack = Attacks[index];
            StartCoroutine(chosenAttack);
            //DoAttacks();
            Debug.Log(index + "index");
			Debug.Log(Attacks.Count + "count");
			//Attacks.RemoveAt(index);
			Attacks.RemoveRange(0, Attacks.Count);
		}
		else if (Attacks.Count == 0)
        {
			FillAttacksList();
		}
		//StartCoroutine(SimpleAttack());
		//StartCoroutine(PullAttack());
		#endregion
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

    #region Coroutines
    public IEnumerator SimpleAttack()
	{
		attackDone = true;
		if (timeBtwShots < 0)
		{
			Debug.Log("Simple Attack Called");
			index = Random.Range(0, Attacks.Count);

			Instantiate(prefabSimpleAttack, transform.position, Quaternion.identity);
			prefabSimpleAttack.SetActive(true);
			timeBtwShots = startTimeBtwShots;
		}
		else
		{
			timeBtwShots -= Time.deltaTime;
		}
		yield return new WaitForSeconds(timeBtwShots);
		attackDone = false;
	}

	public IEnumerator PullAttack()
    {
		attackDone = true;
		if (player != null && pullActiveTime > 0 && Vector2.Distance(transform.position, player.transform.position) < pullAttackDistance)
		{
			Debug.Log("Pull Attack Called");
			index = Random.Range(0, Attacks.Count);

			Vector2 difference = (transform.position - player.transform.position);
			difference = (difference.normalized * pullForce);
			playerRB.AddForce(difference, ForceMode2D.Force);
			yield return new WaitForSeconds(pullCooldown / 2);
		}

		if(pullActiveTime > 0)
        {
			pullActiveTime -= Time.deltaTime;
		}
		else if (pullActiveTime <= 0)
		{
			yield return new WaitForSeconds(pullCooldown/2);
			pullActiveTime = pullActive;
		}

		if(pullActiveTime == pullActive)
        {
			attackDone = false;
        }
	}

	public IEnumerator AbsorbAttack()
    {
		Debug.Log("Absorb Ability Called");
		attackDone = false;

		yield return new WaitForSeconds(1f);
    }
    #endregion

    #region Other Functions

	void FillAttacksList()
    {
		Debug.Log("filling atk list");
		IEnumerator atk1 = SimpleAttack();
		IEnumerator atk2 = PullAttack();
		Attacks.Add(atk1);
		Attacks.Add(atk2);
	}

	void DoAttacks()
    {
		StartCoroutine(chosenAttack);
		Debug.Log(chosenAttack + "cA");
    }

	void TakeDamage()
    {

    }

	void DoDamage()
    {
		if(playerScript != null)
        {
			playerScript.TakeDamage(playerScript.knockTime, bossDamage);
		}
	}
	#endregion

	#region Testing Enum



	#endregion
}