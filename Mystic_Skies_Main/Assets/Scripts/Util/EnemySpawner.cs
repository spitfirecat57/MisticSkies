using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public SpellType enemyType;
	private GameObject spawnedEnemy;
	public float activationDistance = 20.0f;
	public bool willRespawn = true;
	public float spawnDelayAfterDeath = 60.0f;
	private float mSpawnDelayTimer = 0.0f;
	public ParticleSystem onSpawnParticles;


	void Start()
	{
		mSpawnDelayTimer = spawnDelayAfterDeath + 1.0f;
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		if(mr)
		{
			mr.enabled = false;
		}
	}

	void Update()
	{
		if(spawnedEnemy == null)
		{
			mSpawnDelayTimer += Time.deltaTime;

			if( mSpawnDelayTimer > spawnDelayAfterDeath && 
			   	(PlayerManager.GetPlayerPosition() - transform.position).sqrMagnitude < activationDistance * activationDistance)
			{
				onSpawnParticles.Play();
				EnemyLoadout el = EnemyManager.GetEnemyLoadout(enemyType);
				print ("[EnemySpawner] damage = " + el.damage + ", " + el.prefab == null);

				spawnedEnemy = GameObject.Instantiate(el.prefab, transform.position, Quaternion.identity) as GameObject;
				mSpawnDelayTimer = 0.0f;	
			}
		}
	}
}
