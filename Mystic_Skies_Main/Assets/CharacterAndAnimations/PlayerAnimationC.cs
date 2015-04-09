using UnityEngine;
using System.Collections;

public class PlayerAnimationC : MonoBehaviour {

	private float castPosDuration = 2.0f;
	private bool inCastPos = false;
	private float mTimer = 0.0f;

	internal Animator animator;

		 float v;
		 float h;
		 float walk;
		// float run;
	//float sprint;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{


		v = Input.GetAxis ("Vertical");
		h = Input.GetAxis ("Horizontal");
		Walking();
		Running();

		animator.SetBool ("AimPose", PlayerManager.Target ());

		if (Input.GetKeyDown (InputManager.GetKeyCode(InputKeys.FireSpell)))
		{
			animator.SetBool("CastPos", true);
			mTimer = 0.0f;
			inCastPos = true;
		}

		if(inCastPos)
		{
			mTimer += Time.deltaTime;
			
			if(mTimer >= castPosDuration)
			{
				animator.SetBool("CastPos", false);
				mTimer = 0.0f;
				inCastPos = false;
			}
		}

	}
	

	void FixedUpdate()
	{
		animator.SetFloat("WalkP", v);
		animator.SetFloat("TurnP", h);
		animator.SetFloat("JustWalk", walk);
		//animator.SetFloat ("RunP", run);
	}
	void Walking()
	{
		if( Input.GetKey(InputManager.GetKeyCode(InputKeys.Up)) ||
		    Input.GetKey(InputManager.GetKeyCode(InputKeys.Down)) ||
		    Input.GetKey(InputManager.GetKeyCode(InputKeys.Left)) ||
		    Input.GetKey(InputManager.GetKeyCode(InputKeys.Right)))
		{
			walk = 0.2f;
		}
		else
		{
			walk = 0.0f;
		}

	}


	void Running()
	{
		if(Input.GetKeyDown (InputManager.GetKeyCode(InputKeys.Run)))
		{
			animator.SetBool("Running", true);
		}
		else if (Input.GetKeyUp (InputManager.GetKeyCode(InputKeys.Run)))
		{
			animator.SetBool("Running", false);
		}
	}




}		