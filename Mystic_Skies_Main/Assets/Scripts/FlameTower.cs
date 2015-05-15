using UnityEngine;
using System.Collections;

public class FlameTower : MonoBehaviour
{
	public GameObject FlamePrefab;
	public int numFlames;

	public void Init(float damage, float rotSpeed, float expansionSpeed)
	{
		Vector3 startPos = transform.position;
		float angle = 0.0f;
		float deltaAngle = (Mathf.PI * 2.0f) / numFlames;

		for(int i = 0; i < numFlames; ++i)
		{
			Vector3 flamePos = transform.position + new Vector3(Mathf.Cos(angle) , transform.position.y, Mathf.Sin(angle));
			GameObject flame = GameObject.Instantiate(FlamePrefab, flamePos, Quaternion.identity) as GameObject;
			FlameTowerFlame ftf = flame.GetComponent<FlameTowerFlame>();

			ftf.damage = damage;
			ftf.rotSpeed = rotSpeed;
			ftf.originPos = startPos;
			ftf.expansionSpeed = expansionSpeed;

			angle += deltaAngle;
		}

	}
}
