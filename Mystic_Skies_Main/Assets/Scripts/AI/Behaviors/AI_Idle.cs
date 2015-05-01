using UnityEngine;
using System.Collections;

public class AI_Idle : AI_State 
{
	private bool isSuicidal = false;

	override public void OnEnter()
	{
		// return home
		navAgent.SetDestination(spawnPos);

		print ("[Enemy] Entered Idle State");
	}
	
	override public void OnUpdate()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition ();
		if((playerPos - transform.position).sqrMagnitude < owner.alertDistance * owner.alertDistance)
		{
			owner.ChangeState(AI_StateMachine.AIStates.Seek);
		}

		if(!isSuicidal && enemyScript.loadout.health < enemyScript.loadout.maxHealth / 4)
		{
			owner.attackDistance = 1000.0f;
			owner.pursueDistance = 1000.0f;
			owner.alertDistance = 0.0f;
			enemyScript.explodeOnTouch = true;
			navAgent.speed = enemyScript.suicideSpeed;
			navAgent.SetDestination(playerPos);
			isSuicidal = true;
		}
		else if(isSuicidal)
		{
			navAgent.SetDestination(playerPos);
		}
	}
	
	override public void OnExit ()
	{
	}
}
