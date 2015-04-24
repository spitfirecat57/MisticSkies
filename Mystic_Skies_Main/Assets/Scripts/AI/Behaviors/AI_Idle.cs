using UnityEngine;
using System.Collections;

public class AI_Idle : AI_State 
{
	override public void OnEnter()
	{
		// return home
		navAgent.SetDestination(spawnPos);
	}
	
	override public void OnUpdate()
	{
		if((PlayerManager.GetPlayerPosition() - transform.position).sqrMagnitude < owner.alertDistance * owner.alertDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Seek);
		}
	}
	
	override public void OnExit ()
	{
	}
}
