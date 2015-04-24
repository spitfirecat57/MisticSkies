using UnityEngine;
using System.Collections;

public class AI_Attack : AI_State 
{
	public GameObject enemyAttack;
	public float attackRate = 4.0f;
	public float attackDuration = 2.0f;
	private float attackTimer = 0.0f;
	public bool isRanged = true;

	void Start()
	{
		attackTimer = attackRate + 1.0f;
	}

	override public void OnEnter()
	{
		// stop
		navAgent.ResetPath ();
		navAgent.velocity = Vector3.zero;

		//------MARCO WAS HERE------
		Animator anim = GetComponent<Animator>();
		//anim.SetTrigger("attack");
		//anim.CrossFade ("Attack", 0f);
		//------MARCO WAS HERE------
	}
	
	override public void OnUpdate()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition ();
		float distToTargetSqr = (playerPos - transform.position).sqrMagnitude;

		transform.LookAt (new Vector3(playerPos.x, transform.position.y, playerPos.z));

		attackTimer += Time.deltaTime;
		if(attackTimer > attackRate)
		{
			// Attack the player
			GameObject.Instantiate (enemyAttack, transform.position + transform.forward, transform.rotation);
			attackTimer = -attackDuration;
		}

		if(!isRanged && attackTimer > 0.0f && distToTargetSqr > owner.attackDistance * owner.attackDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Seek);
		}
		else if(isRanged && distToTargetSqr > owner.pursueDistance * owner.pursueDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Idle);
		}
	}
	
	override public void OnExit ()
	{
	}
}
