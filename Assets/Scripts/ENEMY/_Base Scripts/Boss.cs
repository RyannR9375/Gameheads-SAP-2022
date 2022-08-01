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
	//public List<IEnumerator> chosenAttack = new List<IEnumerator>();
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
	public float pullCooldown;
	public float startPullCooldown;
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
		pullCooldown = startPullCooldown;
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

		if (Attacks.Count != 0 && attackDone == true)
		{
			index = Random.Range(0, Attacks.Count-1);
			chosenAttack = Attacks[index];
			StartCoroutine(chosenAttack);
			Attacks.RemoveAt(index);
			//Attacks.RemoveAt(index);
			//chosenAttack.RemoveRange(index, 1);
		}
		else if (Attacks.Count == 0)
        {
			FillAttacksList();
			//Attacks.Add(chosenAttack);
		}
		#endregion
		//StartCoroutine(PullAttack());
		#region Simple Attack 
		//StartCoroutine(SimpleAttack());

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
		Debug.Log("Simple Attack Called");
		attackDone = false;

		if (timeBtwShots < 0)
		{
			Instantiate(prefabSimpleAttack, transform.position, Quaternion.identity);
			prefabSimpleAttack.SetActive(true);
			timeBtwShots = startTimeBtwShots;
		}
		else
		{
			timeBtwShots -= Time.deltaTime;
		}

		attackDone = true;
		yield return new WaitForSeconds(1f);
	}

	public IEnumerator PullAttack()
    {
		Debug.Log("Pull Attack Called");
		attackDone = false;

		if (player != null && pullCooldown > 0 && Vector2.Distance(transform.position, transform.position) < pullAttackDistance)
		{
			Vector2 difference = (transform.position - player.transform.position);
			difference = (difference.normalized * pullForce);
			playerRB.AddForce(difference, ForceMode2D.Force);
			pullCooldown -= Time.deltaTime;
		}

		attackDone = true;
		yield return new WaitForSeconds(1f);
	}
    #endregion

    #region Other Functions

	void FillAttacksList()
    {
		Attacks.Add(SimpleAttack());
		Attacks.Add(PullAttack());
	}

	void TakeDamage()
    {

    }

	void DoDamage()
    {
		playerScript.TakeDamage(playerScript.knockTime, bossDamage);
	}
    #endregion
}