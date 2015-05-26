using UnityEngine;
using System.Collections;

public class WaterWhip : MonoBehaviour
{
	[HideInInspector]
	public SpellWaterWhip.Loadout loadout;
	public float damage;
	public SpellType type;

	private BoxCollider hitbox;
	
	private Vector3 positionOffset;


	void Start()
	{
		if(loadout.radius < 60.0f)
		{
			loadout.radius = 60.0f;
		}

		float offset = (loadout.minWidth * 0.5f * Mathf.Tan(loadout.radius * Mathf.Deg2Rad * 0.5f)) + (loadout.range * 0.5f);
		positionOffset = transform.position - (transform.forward * offset);

		hitbox = gameObject.GetComponent<BoxCollider>();

		if(hitbox)
		{
			hitbox.size = new Vector3(loadout.range, loadout.verticalSize, loadout.range);
		}
		else
		{
			Debug.Log("[WaterWhip] Please add a BoxCollider to WaterWhip Object prefab");
		}


		Vector3 forward = transform.forward;
		
		for(float i = (loadout.radius * -0.5f); i < (loadout.radius * 0.5f); i += 5.0f)
		{
			Debug.DrawLine(positionOffset, positionOffset + (Quaternion.AngleAxis(i, Vector3.up) * forward * loadout.range), Color.blue, 0.15f, false);
		}
	}


	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Enemy"))
		{
			Vector3 toOtherOffset = collider.transform.position - positionOffset;
			float theta = Mathf.Acos(Vector3.Dot(toOtherOffset.normalized, transform.forward));

			if(toOtherOffset.sqrMagnitude < (loadout.range * loadout.range) && theta < (loadout.radius * 0.5f * Mathf.Deg2Rad))
			{
				Enemy enemy = collider.GetComponent<Enemy>();
				if(enemy)
				{
					enemy.TakeDamage(type, damage);
				}
				else
				{
					Debug.Log("[WaterWhip] Object with Enemy tag does not have Enemy script");
				}
			}
		}
		else if(collider.CompareTag("FireBoss"))
		{
			Vector3 toOtherOffset = collider.transform.position - positionOffset;
			float theta = Mathf.Acos(Vector3.Dot(toOtherOffset.normalized, transform.forward));
			
			if(toOtherOffset.sqrMagnitude < (loadout.range * loadout.range) && theta < (loadout.radius * 0.5f * Mathf.Deg2Rad))
			{
				FireBoss enemy = collider.GetComponent<FireBoss>();
				if(enemy)
				{
					enemy.TakeDamage(type, damage);
				}
			}
		}
		else if(collider.CompareTag("FlamePillar"))
		{
			Destroy(collider.gameObject);
		}
	}
}
