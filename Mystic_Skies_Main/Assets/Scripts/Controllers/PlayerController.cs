using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float runSpeed;
	public float rotSmoothing;
	public float speedEpsilon;
	
	private Transform camTransform;


	void Start()
	{
		camTransform = PlayerManager.GetCameraTransform ();
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

		// forward
		if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Up)))
		{
			newVel += forward;
		}
		// back
		if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Down)))
		{
			newVel -= forward;
		}
		// right
		if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Right)))
		{
			newVel += right;
		}
		// left
		if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Left)))
		{
			newVel -= right;
		}

		newVel.Normalize();
		if(Input.GetKey(InputManager.GetKeyCode(InputKeys.Run)))
		{
			newVel *= runSpeed;
		}
		else
		{
			newVel *= speed;
		}

		rigidbody.velocity = new Vector3(0.0f, rigidbody.velocity.y, 0.0f) + newVel;


		GameObject target = PlayerManager.Target ();
		if(target)
		{
			Vector3 targetPos = PlayerManager.Target().transform.position;
			transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z), Vector3.up);
		}
		else
		{
			//if(Mathf.Abs(rigidbody.velocity.x) > speedEpsilon || Mathf.Abs(rigidbody.velocity.z) > speedEpsilon)
			if(newVel != Vector3.zero)
			{
				Vector3 towards = new Vector3(newVel.x, 0.0f, newVel.z);
				transform.forward = Vector3.RotateTowards(transform.forward, towards.normalized, rotSmoothing * Time.deltaTime, 1.0f);
			}
		}
	}
	
}



