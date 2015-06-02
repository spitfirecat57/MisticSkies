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
		damage = EnemyManager.GetEnemyLoadout (SpellType.Fire).damage;
		Destroy (gameObject, lifetime);
	}
	
	
	void FixedUpdate()
	{
		Vector3 accel = Vector3.zero;

		// seek to player
		GameObject player = PlayerManager.GetPlayerObject ();
		accel = (player.transform.position - transform.position) + (player.rigidbody.velocity * Time.fixedDeltaTime);
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

		if(!other.CompareTag("FireBoss") && !other.CompareTag("InteractionBox") && !other.CompareTag("Enemy"))
		{
			Destroy(gameObject);
		}
	}
}
