using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float runSpeed;
	public float rotSmoothing;
	public float speedEpsilon;

	private bool isDodging = false;
	private float dodgeDurationTimer = 0.0f;
	private float dodgeCooldownTimer = 0.0f;
	public float dodgeDuration;
	public float dodgeSpeed;
	public float dodgeCooldown;
	
	private Transform camTransform;

	private Vector3 heading;

	Animator animator;

	private Color matColor;


	void Start()
	{
		camTransform = PlayerManager.GetCameraTransform ();
		isDodging = false;
		animator = GetComponentInChildren<Animator> ();
	}

	void Update()
	{
		Vector3 forward = camTransform.forward;
		forward.y = 0.0f;
		forward.Normalize();

		Vector3 right = camTransform.right;
		right.y = 0.0f;
		right.Normalize();

		Vector3 newVel = Vector3.zero;

		dodgeCooldownTimer += Time.deltaTime;

		if(!isDodging)
		{
			// forward
			if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Up)))
			{
				newVel += forward;
				//print ("FORWARD");
			}
			// back
			else if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Down)))
			{
				newVel -= forward;
				//print ("BACK");
			}
			// right
			if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Right)))
			{
				newVel += right;
				//print ("RIGHT");
			}
			// left
			else if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Left)))
			{
				newVel -= right;
				//print ("LEFT");
			}

			newVel.Normalize();
			heading = newVel;
			if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Run)))
			{
				newVel *= runSpeed;
			}
			else
			{
				newVel *= speed;
			}

			rigidbody.velocity = new Vector3(0.0f, rigidbody.velocity.y, 0.0f) + newVel;
		}
		else
		{
			dodgeDurationTimer += Time.deltaTime;
			if(dodgeDurationTimer >= dodgeDuration)
			{
				isDodging = false;
				//
				animator.SetBool("Rolling", isDodging);
				//
				dodgeDurationTimer = 0.0f;
				PlayerManager.GetPlayerScript().SetInvincible(false);
			}
		}

		if(!isDodging && Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.Dodge)) && dodgeCooldownTimer >= dodgeCooldown)
		{
			isDodging = true;
			//
			animator.SetBool("Rolling", isDodging);
			//
			rigidbody.velocity = heading * dodgeSpeed + new Vector3(0.0f, rigidbody.velocity.y, 0.0f);
			dodgeCooldownTimer = 0.0f;
			dodgeDurationTimer = 0.0f;
			PlayerManager.GetPlayerScript().SetInvincible(true);
		}




		GameObject target = PlayerManager.Target();
		if(target)
		{
			Vector3 targetPos = PlayerManager.Target().transform.position;
			transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z), Vector3.up);

			//if(rigidbody.velocity.sqrMagnitude < speed * 0.85f)
			//{
				rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
			//}
		}
		else
		{
			//if(Mathf.Abs(rigidbody.velocity.x) > speedEpsilon || Mathf.Abs(rigidbody.velocity.z) > speedEpsilon)
			if(newVel != Vector3.zero)
			{
				Vector3 towards = new Vector3(newVel.x, 0.0f, newVel.z);
				transform.forward = Vector3.RotateTowards(transform.forward, towards.normalized, rotSmoothing * Time.deltaTime, 0.0f);
			}
		}
	}
	
}



