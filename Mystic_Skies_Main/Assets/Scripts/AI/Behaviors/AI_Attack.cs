using UnityEngine;
using System.Collections;

public class AI_Attack : AI_State 
{
	private bool isAttacking = false;

	override public void OnEnter()
	{
		Debug.Log ("AI_ATTACK");

		navAgent.ResetPath ();
		navAgent.velocity = Vector3.zero;

		// attack in the direction of the enemy

		//------MARCO WAS HERE------

		Animator anim = GetComponent<Animator>();

		//anim.SetTrigger("attack");
		anim.CrossFade ("Attack", 0f);

		//------MARCO WAS HERE------

	}
	
	override public void OnUpdate()
	{
		transform.LookAt (new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

		float distToTargetSqr = (PlayerManager.GetPlayerPosition() - transform.position).sqrMagnitude;

		if(distToTargetSqr > owner.attackDistance * owner.attackDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Seek);
		}
	}
	
	override public void OnExit ()
	{
	}
}
