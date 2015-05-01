﻿using UnityEngine;
using System.Collections;

public class AI_Seek : AI_State 
{
	override public void OnEnter()
	{
		print ("[Enemy] Entered Seek State");
	}
	
	override public void OnUpdate()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition ();
		float distToTargetSqr = (playerPos - transform.position).sqrMagnitude;
		transform.LookAt (new Vector3(playerPos.x, transform.position.y, playerPos.z));
		
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
			navAgent.SetDestination(playerPos);
		}


	}
	
	override public void OnExit ()
	{
	}
}
