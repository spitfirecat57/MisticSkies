using UnityEngine;
using System.Collections;

public class AI_Seeker_Seek : AI_State 
{


	override public void OnEnter()
	{
		Debug.Log ("AI_SEEKER_SEEK");
		if(target)
		{
			navAgent.SetDestination(target.transform.position);
			//------MARCO WAS HERE------

			//Animator anim = GetComponent<Animator>();

		//	anim.SetTrigger("seek");
		//	animm.CrossFade ("Seek", 0f);

			//------MARCO WAS HERE------
		}
		else
		{
			owner.ChangeState(AI_StateMachine.AIStates.Idle);
		}
	}
	
	override public void OnUpdate()
	{

		float distToTargetSqr = (PlayerManager.GetPlayerPosition() - transform.position).sqrMagnitude;

		transform.LookAt (target.transform.position);
		
		if(distToTargetSqr <= owner.attackDistance * owner.attackDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Attack);
		}
		else if(distToTargetSqr > owner.pursueDistance * owner.pursueDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Idle);
		}
		else
		{
			navAgent.SetDestination(target.transform.position);
		}

	}
	
	override public void OnExit ()
	{
	}
}
