using UnityEngine;
using System.Collections;

public class AI_Seeker_Attack : AI_State 
{
	override public void OnEnter()
	{
		Debug.Log ("AI_SEEKER_ATTACK");



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
