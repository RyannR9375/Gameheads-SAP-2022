using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    #region Declaring Variables
    public float attackRange = 5f;
	public float speed = 3f;

	public bool isFlipped = false;

	private int attacksListLength;
    #endregion

    #region Components
    public Transform player;
	public GameObject prefabSimpleAttack;
	public List<IEnumerator> Attacks;
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
    private void Update()
    {
		//SET UP A LIST OF ALL THE COROUTINES, RANDOMIZE THEM, AND THEN GO THROUGH THE LIST, DELETE THAT COROUTINE FROM THE LIST ONCE EXECUTED, IF THE LIST IS DONE, RESET THE LIST AND RANDOMIZE IT AGAIN
		//UNTIL BOSS DEAD.
		Attacks.Add(SimpleAttack());
		attacksListLength = Attacks.Count;

    }
    #endregion


    #region Coroutines
    private IEnumerator SimpleAttack()
	{
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		yield return new WaitForSeconds(3f);
		//boss.anim
		GameObject attack = (GameObject)Instantiate(prefabSimpleAttack, transform);
		attack.SetActive(true);
		yield return new WaitForSeconds(0f);
		Destroy(attack);
	}
    #endregion
}