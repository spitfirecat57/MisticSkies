using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
	public SpellType mType;
	
	public float health;
	public float maxHealth;
	public float knockbackDist;
	
	private Vector3 vel = Vector3.zero;
	
	private NavMeshAgent navAgent;
	
	public ItemManager.ConsumableDropChance dropChance;
	
	
	void Start()
	{
		EnemyManager.RegisterEnemy(gameObject);
		
		navAgent = GetComponent<NavMeshAgent>();
		
		if(dropChance.percentChanceToDrop > 0.0f)
		{
			if(dropChance.healthShardRatio 	== 0.0f && 
			   dropChance.potionHealthRatio	== 0.0f && 
			   dropChance.potionManaRatio 	== 0.0f && 
			   dropChance.potionRejuvRatio 	== 0.0f )
			{
				dropChance.percentChanceToDrop = 0.0f;
			}
			else
			{
				// normalize drop chances out of 100.0f
				float dropSum = dropChance.healthShardRatio + dropChance.potionHealthRatio + dropChance.potionManaRatio + dropChance.potionRejuvRatio;
				dropChance.healthShardRatio = (dropChance.healthShardRatio / dropSum) * 100.0f;
				dropChance.potionHealthRatio = ((dropChance.potionHealthRatio / dropSum) * 100.0f) + dropChance.healthShardRatio;
				dropChance.potionManaRatio = ((dropChance.potionManaRatio / dropSum) * 100.0f) + dropChance.potionHealthRatio;
				dropChance.potionRejuvRatio = ((dropChance.potionRejuvRatio / dropSum) * 100.0f) + dropChance.potionManaRatio;
			}
		}
	}
	
	void Update()
	{
		transform.position += vel * Time.deltaTime;
	}
	
	public void TakeDamage(SpellType attackType, float damage)
	{
		float totalDamage = damage * Spell.GetDamageMultiplier (attackType, mType);
		health -= totalDamage;
		print ("Enemy took " + damage + " damage");
		
		if(health < 1)
		{
			// play animation?
			
			// drop something maybe
			float drop = Random.Range(0.0f, 99.0f);
			if(drop < dropChance.percentChanceToDrop)
			{
				float objChance = Random.Range(0.0f, 99.0f);
				GameObject objType = null;
				Vector3 launchPos = transform.position + (Vector3.up * 2.0f);
				if(objChance < dropChance.healthShardRatio)
				{
					objType = GameObject.Instantiate(ItemManager.GetObject(ItemManager.ConsumableType.HealthShard), launchPos, Quaternion.identity) as GameObject;
				}
				else if(objChance < dropChance.potionHealthRatio)
				{
					objType = GameObject.Instantiate(ItemManager.GetObject(ItemManager.ConsumableType.PotionHealth), launchPos, Quaternion.identity) as GameObject;
				}
				else if(objChance < dropChance.potionManaRatio)
				{
					objType = GameObject.Instantiate(ItemManager.GetObject(ItemManager.ConsumableType.PotionMana), launchPos, Quaternion.identity) as GameObject;
				}
				else if(objChance < dropChance.potionRejuvRatio)
				{
					objType = GameObject.Instantiate(ItemManager.GetObject(ItemManager.ConsumableType.PotionRejuv), launchPos, Quaternion.identity) as GameObject;
				}
				
				if(objType)
				{
					Destroy(gameObject);
					Vector3 dropLaunch = new Vector3();
					dropLaunch.y = 5.0f;
					dropLaunch.x = Random.Range(-2.5f, 2.5f);
					dropLaunch.z = Random.Range(-2.5f, 2.5f);
					objType.rigidbody.velocity = dropLaunch;
					return;
				}
			}
			
			OnDeath();
		}
		
		
	}
	
	public void ApplyKnockback(Vector3 direction)
	{
		navAgent.Move(direction * knockbackDist);
	}
	
	private void OnDeath()
	{
		//TODO: Enemy Death animations here
		
		Destroy (gameObject);
	}
}
