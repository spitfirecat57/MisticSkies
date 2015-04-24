using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
	public SpellType type;
	[HideInInspector]
	public EnemyLoadout loadout;

	
	public ItemManager.ConsumableDropChance dropChance;
	
	private NavMeshAgent navAgent;
	
	
	void Start()
	{
		EnemyManager.RegisterEnemy(gameObject);
		EnemyManager.InitEnemy(this);
		
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
	
	public void TakeDamage(SpellType attackType, float damage)
	{
		float totalDamage = damage * Spell.GetDamageMultiplier (attackType, loadout.mType);
		loadout.health -= totalDamage;
		print ("Enemy took " + damage + " damage");
		
		if(loadout.health < 1)
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
	
	public void Knockback(Vector3 direction)
	{
		StartCoroutine ("CoKnockBack", direction);
	}

	IEnumerator CoKnockBack (Vector3 direction)
	{
		navAgent.velocity = direction;
		yield return new WaitForSeconds (0.5f);
	}

	private void OnDeath()
	{
		//TODO: Enemy Death animations here
		
		Destroy (gameObject);
	}
}
