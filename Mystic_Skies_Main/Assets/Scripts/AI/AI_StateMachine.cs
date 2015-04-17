using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
//public class NavAgentLoadout
//{
//
//}

public class AI_StateMachine : MonoBehaviour
{
	//The important variables for States->
	public float alertDistance = 10.0f;
	public float pursueDistance = 15.0f;
	//public NavAgentLoadout navAgentLoadout;
	// NavMeshAgent variables
	public float acceleration;
	public float speed;
	public float turnSpeed;
	public float attackDistance;
	
	private Vector3 spawnPosition;
	private GameObject target;
	private NavMeshAgent navAgent;
	//===================================|
	
	//AI FSM Variables ------------------>
	public AI_State mStateIdle;
	public AI_State mStateSeek;
	public AI_State mStateAttack;
	
	private AIStates mCurrentState;
	private AIStates mNextState;
	
	private AI_State[] mStates;
	private AI_State mCurrentAI_State;
	//===================================|
	
	//AI States ------------------------->
	public enum AIStates
	{
		Idle,
		Seek,
		Attack,
		COUNT
	}
	//===================================|
	
	// Use this for initialization
	public void Start ()
	{
		// State Variables -------------->
		
		// Set spawn position
		spawnPosition = transform.position;
		
		// get NavMeshAgent component reference
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.acceleration 		= acceleration;
		navAgent.speed 				= speed;
		navAgent.angularSpeed 		= turnSpeed;
		//navAgent.stoppingDistance 	= attackDistance;
		
		// default target is player
		target = PlayerManager.GetPlayerObject();
		
		//AI Initilization -------------->
		mStates = new AI_State[(int)AIStates.COUNT];
		mStates[(int)AIStates.Idle]   = mStateIdle;
		mStates[(int)AIStates.Seek]   = mStateSeek;
		mStates[(int)AIStates.Attack] = mStateAttack;
		
		// Set state info references to this
		foreach(AI_State s in mStates)
		{
			s.SetOwner(this);
			s.SetNavAgent(navAgent);
			s.SetTarget(target);
			//s.SetSpawnPosition(spawnPosition);
		}
		
		// Set current and next states
		// NOTE: (current != next) forces OnEnter() in first update
		mCurrentState = AIStates.Seek;
		mNextState = AIStates.Idle;
		
		mCurrentAI_State = mStates [(int)mCurrentState];
		//===============================|
	}
	
	// Update is called once per frame
	public void Update ()
	{
		//Switch state
		if(mCurrentState != mNextState)
		{
			mCurrentAI_State.OnExit();
			mCurrentAI_State = mStates[(int)mNextState];
			mCurrentAI_State.OnEnter();
			mCurrentState = mNextState;
		}
		mCurrentAI_State.OnUpdate();
	}
	
	public void ChangeState(AIStates state)
	{
		mNextState = state;
	}
	
	public void SetTarget(GameObject target)
	{
		this.target = target;
		foreach(AI_State s in mStates)
		{
			s.SetTarget(target);
		}
	}
	
	public Vector3 SpawnPosition()
	{
		return spawnPosition;
	}
	
	
}






