using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AI_StateMachine : MonoBehaviour
{
	//The important variables for States->
	public float alertDistance = 20.0f;
	public float pursueDistance = 25.0f;
	public float attackDistance = 2.0f;
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
		//AI Initilization -------------->
		mStates = new AI_State[(int)AIStates.COUNT];
		mStates[(int)AIStates.Idle]   = mStateIdle;
		mStates[(int)AIStates.Seek]   = mStateSeek;
		mStates[(int)AIStates.Attack] = mStateAttack;
		
		// Set state info references to this
		foreach(AI_State s in mStates)
		{
			s.SetOwner(this);
		}
		
		// Set current and next states
		mCurrentState = AIStates.Idle;
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
	
	
}






