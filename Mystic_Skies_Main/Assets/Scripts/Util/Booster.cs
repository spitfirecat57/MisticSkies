using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour
{
	public Transform landingPos;
	public float timeInAir;
	public float c;

	private Vector3 launchPos;
	private Vector3 launchVel;

	void Start()
	{
		launchPos = transform.position;

	}

	void OnTriggerEnter(Collider other)
	{
		if(landingPos)
		{
			Vector3 launchToLanding = landingPos.position - launchPos;
			launchToLanding.y = 0.0f;
			float magXZ = launchToLanding.magnitude;
			
			float p0x = launchPos.x;
			float p0y = launchPos.y;
			float p1x = landingPos.position.x;
			float p1y = landingPos.position.y;
			float vx = magXZ / timeInAir;
			//float c = ((p0y*p1x*p1x) - (p1y*p0x*p0x)) / ((p0x*p0x) + (p1x*p1x));
			float a = (-p1y - c) / (p1x*p1x);
			float x = Mathf.Sqrt((-p0y / a) - c);
			launchVel.y = vx * (-2.0f * a * x);
			launchToLanding.Normalize();
			launchVel.x = vx * launchToLanding.x * magXZ;
			launchVel.z = vx * launchToLanding.z * magXZ;
		}




		Debug.Log("[Booster] Collided with " + other.tag);
		//if(other.CompareTag("Player"))
		//{
			Debug.Log("[Booster] Touched player");
			//PlayerController pc = PlayerManager.getplayer();
			//if(pc.enabled)
			//{
			//	Debug.Log("[Booster] Launched player");
			//	pc.enabled = false;
			//	PlayerManager.GetPlayerObject().rigidbody.velocity = launchVel;
			//}
		//}
	}
}
