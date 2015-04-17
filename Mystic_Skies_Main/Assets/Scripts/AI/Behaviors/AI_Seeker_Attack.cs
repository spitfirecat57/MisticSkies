using UnityEngine;
using System.Collections;

public class AI_Seeker_Attack : AI_State 
{
	public GameObject fireballObject;
	public float attackTimeInterval = 6.0f;
	private float attackTimer = 6.0f;
	public int numFireballsInBurst = 3;
	private bool isFiring = false;
	public Transform fireballSpawnPos;


	override public void OnEnter()
	{
		Debug.Log ("AI_SEEKER_ATTACK");

		navAgent.stoppingDistance = 0.0f;

		//------MARCO WAS HERE------
		//Animator anim = GetComponent<Animator>();

		//anim.SetTrigger("attack");
		//anim.CrossFade ("Attack", 0f);
		//------MARCO WAS HERE------
	}
	
	override public void OnUpdate()
	{
		if(!isFiring)
		{
			attackTimer += Time.deltaTime;

			if(attackTimer >= attackTimeInterval)
			{
				StartCoroutine("Fire");
				attackTimer = 0.0f;
			}
			else
			{
				Vector3 fromPlayer = transform.position - target.transform.position;
				Vector3 toStoppingDistance = (fromPlayer.normalized * owner.attackDistance) - transform.position;

				if(Vector3.Dot(toStoppingDistance.normalized, transform.forward) < 0.0f)
				{
					navAgent.SetDestination(transform.position + toStoppingDistance);
				}
				else if(fromPlayer.sqrMagnitude < owner.alertDistance * owner.alertDistance)
				{
					owner.ChangeState(AI_StateMachine.AIStates.Seek);
				}
			}
		}


		transform.LookAt (PlayerManager.GetPlayerPosition());
	}
	
	override public void OnExit ()
	{
		isFiring = false;
		navAgent.stoppingDistance = owner.attackDistance;
	}

	IEnumerator Fire()
	{
		isFiring = true;
		for(int i = 0; i < numFireballsInBurst; ++i)
		{
			GameObject.Instantiate (fireballObject, fireballSpawnPos.position, fireballSpawnPos.rotation);
			print ("[AI_SEEKER_ATTACK] Fired a fireball");
			yield return new WaitForSeconds(0.5f);
		}
		isFiring = false;
	}

}
