using UnityEngine;
using System.Collections;

public class AI_Seeker_Seek : AI_State 
{


	override public void OnEnter()
	{
		Debug.Log ("AI_SEEK");
		if(target)
		{
			navAgent.SetDestination(target.transform.position);
			//------MARCO WAS HERE------

			Animator anim = GetComponent<Animator>();

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
		if(target)
		{
			//float distToTargetSqr = (target.transform.position - transform.position).sqrMagnitude;
			
			//if(distToTargetSqr <= navAgent.stoppingDistance * navAgent.stoppingDistance)
			if(navAgent.remainingDistance == 0.0f)
			{
				owner.ChangeState(AI_StateMachine.AIStates.Attack);
			}
			else if(navAgent.remainingDistance > owner.pursueDistance * owner.pursueDistance)
			{
				owner.ChangeState(AI_StateMachine.AIStates.Idle);
			}
			else
			{
				navAgent.SetDestination(target.transform.position);
			}
		}
		else
		{
			owner.ChangeState(AI_StateMachine.AIStates.Idle);
		}
	}
	
	override public void OnExit ()
	{
	}
}
