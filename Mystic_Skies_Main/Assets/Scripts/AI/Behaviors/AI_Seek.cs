using UnityEngine;
using System.Collections;

public class AI_Seek : AI_State 
{
	override public void OnEnter()
	{
		Debug.Log ("AI_SEEK");

			navAgent.SetDestination(target.transform.position);
			//------MARCO WAS HERE------

			Animator anim = GetComponent<Animator>();

		//	anim.SetTrigger("seek");
		//	animm.CrossFade ("Seek", 0f);

			//------MARCO WAS HERE------
	}
	
	override public void OnUpdate()
	{
		float distToTargetSqr = (PlayerManager.GetPlayerPosition() - transform.position).sqrMagnitude;
		
		if(distToTargetSqr < owner.attackDistance * owner.attackDistance)
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


		transform.LookAt (new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
	}
	
	override public void OnExit ()
	{
	}
}
