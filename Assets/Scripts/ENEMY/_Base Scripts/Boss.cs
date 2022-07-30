using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	#region Calling Scripts

	//Player playerScript;

	#endregion
	#region Declaring Variables
	private bool isFlipped = false;
	private bool attackDone = true;
	private int index;
	#endregion

	#region Components
	public Transform player;
	public GameObject prefabSimpleAttack;
	GameObject attack;

	public List<IEnumerator> Attacks = new List<IEnumerator>();
	public List<IEnumerator> chosenAttack = new List<IEnumerator>();
	#endregion

	#region Boss Stats
	[Header("Boss Stats")]
	public float stoppingDistance;
	public float attackRange;
	public float speed;
	public float health;

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
		//Add all coroutines here
		Attacks.Add(SimpleAttack());
		Attacks.Add(PullAttack());
	}

	void Start()
	{
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

		if (chosenAttack.Count != 0 && attackDone == true)
		{
			//random when it shudnt be
			index = Random.Range(0, Attacks.Count);
			chosenAttack.Add(Attacks[index]);

			StartCoroutine(chosenAttack[index]);

			//chosenAttack.RemoveAt(index);
			chosenAttack.RemoveRange(index, 1);
			Debug.Log(chosenAttack.Count);
			Debug.Log(Attacks.Count);
			//StartCoroutine(SimpleAttack());
		}
        else if (chosenAttack.Count == 0)
        {	
			for ( int i = 0; i < Attacks.Count; i++)
            {
				chosenAttack.Add(Attacks[i]);
				Debug.Log(chosenAttack.Count + "being added");
			}
			//Attacks.Add(chosenAttack);
		}
		#endregion
		//StartCoroutine(PullAttack());
		#region Simple Attack 
		//StartCoroutine(SimpleAttack());

        #endregion

    }
    #endregion

    #region Coroutines
    public IEnumerator SimpleAttack()
	{
		attackDone = false;
		Debug.Log("Called");
		if (timeBtwShots <= 0)
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
		attackDone = false;
		Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

		yield return new WaitForSeconds(0f);
		if (player != null && pullCooldown >= 0 && Vector2.Distance(transform.position, transform.position) < pullAttackDistance)
		{
			Vector2 difference = (transform.position - player.transform.position);
			difference = (difference.normalized * pullForce);
			playerRB.AddForce(difference, ForceMode2D.Impulse);
			pullCooldown -= Time.deltaTime;

		}

		attackDone = true;
		yield return new WaitForSeconds(pullCooldown + startPullCooldown);
		pullCooldown = startPullCooldown;
	}
    #endregion
}