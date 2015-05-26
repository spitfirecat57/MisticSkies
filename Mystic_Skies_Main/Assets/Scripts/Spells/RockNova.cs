using UnityEngine;
using System.Collections;

public class RockNova : MonoBehaviour
{
	[HideInInspector]
	public SpellRockNova.Loadout loadout;

	public float damage;
	public SpellType type;
	
	
	void Start()
	{
		SphereCollider hitbox = GetComponent<SphereCollider> ();
		if(hitbox)
		{
			hitbox.radius = loadout.radius;
		}
		else
		{
			Debug.Log("[RockNova] Please add a SphereCollider to RockNova object prefab");
		}
		
		Destroy (gameObject, 1.1f);
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Enemy"))
		{
			Vector3 toOther = collider.transform.position - transform.position;
			
			if(toOther.y < loadout.maxHeight)
			{
				Enemy enemy = collider.GetComponent<Enemy>();
				if(enemy)
				{
					enemy.TakeDamage(type, damage);
				}
				else
				{
					Debug.Log("[RockNova] Object with Enemy tag does not have Enemy script");
				}
			}
		}
		else if(collider.CompareTag("FireBoss"))
		{
			Vector3 toOther = collider.transform.position - transform.position;
			
			if(toOther.y < loadout.maxHeight)
			{
				FireBoss enemy = collider.GetComponent<FireBoss>();
				if(enemy)
				{
					enemy.TakeDamage(type, damage);
				}
			}
		}

	}
}
