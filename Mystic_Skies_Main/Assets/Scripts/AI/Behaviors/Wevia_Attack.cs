using UnityEngine;
using System.Collections;

public class Wevia_Attack : AI_State 
{
	public ParticleSystem waterSpray;
	public float attackCooldownTime = 5.0f;
	public float attackAnimationDuration;
	public float attackWarmupTime;
	public float speedWhileAttacking;
	public float turnSpeed;

	private bool isAttacking = false;
	private float attackTimer = 0.0f;

	private Animator anim;

	private float oldSpeed = 0.0f;

	void Start()
	{
		attackTimer = attackCooldownTime;
		anim = GetComponent<Animator>();
	}

	override public void OnEnter()
	{
		// stop
		navAgent.ResetPath ();
		navAgent.velocity = Vector3.zero;
		
		print ("[Enemy] Entered Attack State");
		oldSpeed = navAgent.speed;
		navAgent.speed = speedWhileAttacking;
	}

	void Update()
	{
		attackTimer += Time.deltaTime;
	}

	override public void OnUpdate()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition ();
		float distToTargetSqr = (playerPos - transform.position).sqrMagnitude;
		transform.rotation = Quaternion.LookRotation( Vector3.RotateTowards (waterSpray.transform.forward, new Vector3(playerPos.x, transform.position.y, playerPos.z) - transform.position, turnSpeed * Time.deltaTime, 0.0f));


		if(isAttacking)
		{
			waterSpray.transform.rotation = Quaternion.LookRotation( Vector3.RotateTowards (transform.forward, new Vector3(transform.position.x, playerPos.y, transform.position.z) - transform.position, turnSpeed * Time.deltaTime, 0.0f));
			if(!waterSpray.isPlaying && attackTimer > attackWarmupTime)
			{
				waterSpray.Play ();
			}
			if(attackTimer > attackAnimationDuration)
			{
				attackTimer = 0.0f;
				waterSpray.Stop ();
				isAttacking = false;
				print("[Wevia_Attack] Attacking FINISHED");
			}
		}
		else
		{

			if(distToTargetSqr > owner.attackDistance * owner.attackDistance)
			{
				owner.ChangeState(AI_StateMachine.AIStates.Seek);
				return;
			}
			else if(distToTargetSqr > owner.pursueDistance * owner.pursueDistance)
			{
				owner.ChangeState(AI_StateMachine.AIStates.Idle);
				return;
			}

			if(attackTimer > attackCooldownTime)
			{
				anim.CrossFade ("Attack", 0f);

				print("[Wevia_Attack] Attacking START");
				isAttacking = true;
				attackTimer = 0.0f;
			}
		}

		navAgent.SetDestination (PlayerManager.GetPlayerPosition ());
	}
	
	override public void OnExit ()
	{
		navAgent.speed = oldSpeed;
		navAgent.Stop ();
		navAgent.ResetPath ();
	}
}
