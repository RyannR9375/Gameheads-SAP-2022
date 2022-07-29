using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
	Transform player;
	Rigidbody2D rb;
	Boss boss;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
		boss = animator.GetComponent<Boss>();
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		boss.LookAtPlayer();

		//GETS THE PLAYERS POSITION,
		//STORES A VECTOR 2 WITH A 'MoveTowards' TO OUR PLAYER,
		//MOVES THE BOSS TO TARGET LOCATION
		Vector2 target = new Vector2(player.position.x, player.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, boss.speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);

		//IF THE PLAYER IS IN RANGE, BOSS ATTACKS
		if (Vector2.Distance(player.position, rb.position) <= boss.attackRange)
		{
			animator.SetTrigger("Attack");
			rb.MovePosition(rb.position);
        }
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("Attack");
	}
}