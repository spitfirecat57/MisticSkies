using UnityEngine;
using System.Collections;

public class Lapis_Attack : AI_State 
{
	public float attackCooldownTime = 5.0f;
	private float attackIntervalTimer = 0.0f;
	public float damage = 5.0f;

	public float attackDist = 3.0f;

	//private bool isAttacking = false;





	override public void OnEnter()
	{
		// stop
		navAgent.ResetPath ();
		navAgent.velocity = Vector3.zero;

		//------MARCO WAS HERE------
		//Animator anim = GetComponent<Animator>();
		//anim.SetTrigger("attack");
		//anim.CrossFade ("Attack", 0f);
		//------MARCO WAS HERE------

		//print ("[Enemy] Entered Attack State");
	}

	void Update()
	{
		// time bertween burstFire attacks
		attackIntervalTimer += Time.deltaTime;
	}

	override public void OnUpdate()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition ();
		float distToTargetSqr = (playerPos - transform.position).sqrMagnitude;
		transform.LookAt (new Vector3(playerPos.x, transform.position.y, playerPos.z));


		if(attackIntervalTimer > attackCooldownTime)
		{
			navAgent.SetDestination(playerPos);
			//isAttacking = true;
			if(distToTargetSqr < attackDist * attackDist)
			{
				PlayerManager.GetPlayerScript().TakeDamage(damage);
				PlayerManager.GetPlayerScript().KnockBack((playerPos - transform.position).normalized * 3.0f);
				attackIntervalTimer = 0.0f;
				navAgent.Stop();
			}
		}
		
		if(distToTargetSqr > owner.attackDistance * owner.attackDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Seek);
		}
		else if(distToTargetSqr > owner.pursueDistance * owner.pursueDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Idle);
		}


	}
	
	override public void OnExit ()
	{
	}
}
