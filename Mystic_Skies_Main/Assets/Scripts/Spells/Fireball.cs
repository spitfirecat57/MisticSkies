using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
	[HideInInspector]
	public GameObject target;
	[HideInInspector]
	public SpellFireball.Loadout loadout;
	public float damage;
	public SpellType type;
	
	
	void Start()
	{
		rigidbody.velocity = transform.forward * loadout.speed;
		Destroy (gameObject, loadout.lifetime);
	}
	
	
	void FixedUpdate()
	{
		Vector3 accel = Vector3.zero;
		
		if(target)
		{
			// seek to target
			accel = target.transform.position - transform.position;
			accel.Normalize();
			accel *= loadout.acceleration * Time.fixedDeltaTime;
		}
		else
		{
			target = EnemyManager.GetClosest(transform.position, PlayerManager.GetPlayerScript().maxTargetingRange);
		}
		
		rigidbody.velocity += accel;
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, loadout.speed);
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if(!other.CompareTag("Player") && !other.CompareTag("InteractionBox"))
		{
			if(other.CompareTag("Enemy"))
			{
				Enemy enemy = other.GetComponent<Enemy>();
				if(enemy)
				{
					enemy.Knockback((other.transform.position - transform.position).normalized * loadout.knockBack);
					enemy.TakeDamage(type, damage);
				}
			}
			else if(other.CompareTag("FireBoss"))
			{
				FireBoss enemy = other.GetComponent<FireBoss>();
				if(enemy)
				{
					enemy.TakeDamage(type, damage);
				}
			}

			
			if(loadout.isExplosive)
			{
				Explode();
			}
			
			Debug.Log("[Fireball] Hit gameObject " + other.name);
			Destroy(gameObject);
		}
	}
	
	
	private void Explode()
	{
		Debug.Log("[Fireball] Exploded!!!");
		Collider[] cols = Physics.OverlapSphere (transform.position, loadout.explodeRadius, LayerMask.NameToLayer("Enemy"));
		foreach(Collider col in cols)
		{
			if(col.CompareTag("Enemy"))
			{
				Enemy enemy = col.GetComponent<Enemy>();
				if(enemy)
				{
					//Debug.Log("[Fireball] Explosion hit an enemy");
					enemy.TakeDamage(type, damage);
				}
			}
		}
	}
	
	
	
}
