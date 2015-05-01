﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent), typeof(AI_StateMachine), typeof(Enemy))]

public abstract class AI_State: MonoBehaviour 
{
	protected AI_StateMachine owner;
	protected NavMeshAgent navAgent;
	protected Enemy enemyScript;
	protected Vector3 spawnPos;
	
	abstract public void OnEnter();
	abstract public void OnUpdate();	
	abstract public void OnExit();

	void Awake()
	{
		spawnPos = gameObject.transform.position;
		navAgent = gameObject.GetComponent<NavMeshAgent> ();
		enemyScript = gameObject.GetComponent<Enemy> ();
	}

	public void SetOwner(AI_StateMachine owner)
	{
		this.owner = owner;
	}
	
	public struct TimeAndState
	{
		public float time;
		public AI_StateMachine.AIStates state;
		public TimeAndState(float t, AI_StateMachine.AIStates s)
		{
			time = t;
			state = s;
		}
	}
	IEnumerator WaitThenSwitch(TimeAndState tas)
	{
		print ("[AI_State::WaitThenSwitch] " + tas.time + ", " + tas.state.ToString());
		yield return new WaitForSeconds(tas.time);
		owner.ChangeState(tas.state);
	}
}