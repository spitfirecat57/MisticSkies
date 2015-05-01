using UnityEngine;
using System.Collections;

public class AI_Attack : AI_State 
{
	public GameObject enemyAttack;
	public int numFireballsInBurst = 3;
	public float burstFireRate = 0.8f;
	public float attackCooldownTime = 5.0f;

	private float burstFireTimer = 0.0f;
	private int burstFireCount = 0;
	private float attackIntervalTimer = 0.0f;
	private bool isAttacking = false;
	private bool isSuicidal = false;


	void Start()
	{
		attackIntervalTimer = attackCooldownTime;
	}

	override public void OnEnter()
	{
		// stop
		navAgent.ResetPath ();
		navAgent.velocity = Vector3.zero;

		//------MARCO WAS HERE------
		Animator anim = GetComponent<Animator>();
		//anim.SetTrigger("attack");
		//anim.CrossFade ("Attack", 0f);
		//------MARCO WAS HERE------

		print ("[Enemy] Entered Attack State");
	}

	void Update()
	{
		// time bertween burstFire attacks
		attackIntervalTimer += Time.deltaTime;
	}

	override public void OnUpdate()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition ();
		float distToTargetSqr = (playerPos - transform.position).sqrMagnitude;
		transform.LookAt (new Vector3(playerPos.x, transform.position.y, playerPos.z));


		if(isAttacking)
		{
			// increment burstfire timer
			burstFireTimer += Time.deltaTime;

			// if firing is available
			if(burstFireTimer >= burstFireRate)
			{
				GameObject.Instantiate (enemyAttack, transform.position + transform.forward, transform.rotation);				
				++burstFireCount;
				burstFireTimer = 0.0f;

				print ("[Enemy] Fired number " + burstFireCount);

				if(burstFireCount == numFireballsInBurst)
				{
					attackIntervalTimer = 0.0f;
					burstFireCount = 0;
					isAttacking = false;
				}
			}
		}
		else
		//if(!isAttacking)
		{
			// set attacking flag
			if(attackIntervalTimer > attackCooldownTime)
			{
				isAttacking = true;
				print ("[Enemy] Starting attack cycle");
			}

			if(distToTargetSqr > owner.attackDistance * owner.attackDistance)
			{
				owner.ChangeState(AI_StateMachine.AIStates.Seek);
			}
			else if(distToTargetSqr > owner.pursueDistance * owner.pursueDistance)
			{
				owner.ChangeState(AI_StateMachine.AIStates.Idle);
			}
		}

		if(!isSuicidal && enemyScript.loadout.health < enemyScript.loadout.maxHealth / 4)
		{
			owner.attackDistance = 1000.0f;
			owner.pursueDistance = 1000.0f;
			owner.alertDistance = 0.0f;
			enemyScript.explodeOnTouch = true;
			navAgent.SetDestination(playerPos);
			navAgent.speed = enemyScript.suicideSpeed;
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
