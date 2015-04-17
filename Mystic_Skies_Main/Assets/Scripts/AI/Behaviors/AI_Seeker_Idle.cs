using UnityEngine;
using System.Collections;

public class AI_Seeker_Idle : AI_State 
{
	override public void OnEnter()
	{
		// return home
		navAgent.SetDestination(spawnPos);

		//------MARCO WAS HERE------
		
		//Animator anim = GetComponent<Animator>();

		//anim.SetTrigger("idle");
		//animmm.CrossFade ("Idle", 0f);
		
		//------MARCO WAS HERE------


	}
	
	override public void OnUpdate()
	{
		if(target)
		{
			if((target.transform.position - transform.position).sqrMagnitude < owner.alertDistance * owner.alertDistance)
			{
				print ("[AI_IDLE] Noticed the player, changing to seek state");
				owner.ChangeState(AI_StateMachine.AIStates.Seek);
			}
		}
	}
	
	override public void OnExit ()
	{
	}
}
