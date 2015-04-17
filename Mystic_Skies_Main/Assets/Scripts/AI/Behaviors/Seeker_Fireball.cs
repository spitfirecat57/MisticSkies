using UnityEngine;
using System.Collections;

public class Seeker_Fireball : MonoBehaviour
{
	public float damage;
	public float speed;
	public float acceleration;
	public float lifetime;
	
	
	void Start()
	{
		rigidbody.velocity = transform.forward * speed;
		Destroy (gameObject, lifetime);
	}
	
	
	void FixedUpdate()
	{
		Vector3 accel = Vector3.zero;

		// seek to player
		accel = PlayerManager.GetPlayerPosition() - transform.position;
		accel.Normalize();
		accel *= acceleration * Time.fixedDeltaTime;

		
		rigidbody.velocity += accel;
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, speed);
	}
	
	
	void OnTriggerEnter(Collider other)
	{

		if(other.CompareTag("Player"))
		{
			Player player = other.GetComponent<Player>();
			if(player)
			{
				player.KnockBack(other.transform.position - transform.position);
				player.TakeDamage(damage);
			}
		}
		

		if(!other.CompareTag("InteractionBox") && !other.CompareTag("Enemy"))
		{
			Debug.Log("[Fireball] Hit gameObject " + other.name);
			Destroy(gameObject);
		}

	}
	
	
//	private void Explode()
//	{
//		Debug.Log("[Fireball] Exploded!!!");
//		Collider[] cols = Physics.OverlapSphere (transform.position, loadout.explodeRadius, LayerMask.NameToLayer("Enemy"));
//		foreach(Collider col in cols)
//		{
//			if(col.CompareTag("Enemy"))
//			{
//				Enemy enemy = col.GetComponent<Enemy>();
//				if(enemy)
//				{
//					//Debug.Log("[Fireball] Explosion hit an enemy");
//					enemy.TakeDamage(loadout.type, loadout.explodeDamage);
//				}
//			}
//		}
//	}
}
