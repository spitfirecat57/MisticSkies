using UnityEngine;
using System.Collections;

public class AI_Attack : AI_State 
{
	override public void OnEnter()
	{
		Debug.Log ("AI_ATTACK");


		// attack in the direction of the enemy
		
		// TODO: 
		// simulate attack animation duration
		StartCoroutine ("WaitThenSwitch", new AI_State.TimeAndState(1.0f, AI_StateMachine.AIStates.Idle));

		//------MARCO WAS HERE------

		Animator anim = GetComponent<Animator>();

		//anim.SetTrigger("attack");
		anim.CrossFade ("Attack", 0f);

		//------MARCO WAS HERE------


	}
	
	override public void OnUpdate()
	{
		transform.LookAt (PlayerManager.GetPlayerPosition());
	}
	
	override public void OnExit ()
	{
	}
}
