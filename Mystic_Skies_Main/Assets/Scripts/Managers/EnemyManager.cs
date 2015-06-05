using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct EnemyLoadout
{
	public string name;
	public SpellType mType;
	public GameObject prefab;
	public float maxHealth;
	[HideInInspector]
	public float health;
	public float damage;
	public float knockbackPower;
	public float speed;
}

public class EnemyManager : Singleton<EnemyManager>
{
	public static List<GameObject> enemies;

	public EnemyLoadout waterEnemyLoadout;
	public EnemyLoadout fireEnemyLoadout;
	public EnemyLoadout rockEnemyLoadout;
	private static EnemyLoadout[] enemyLoadouts;


	private static bool awakeHasBeenCalled = false;
	void Awake()
	{
		if(!awakeHasBeenCalled)
		{
			print ("[EnemyManager] Start()");
			enemies = new List<GameObject>();
			StartCoroutine ("PurgeEnemyList");

			enemyLoadouts = new EnemyLoadout[3];
			enemyLoadouts[(int)SpellType.Fire]  = fireEnemyLoadout;
			enemyLoadouts[(int)SpellType.Water] = waterEnemyLoadout;
			enemyLoadouts[(int)SpellType.Rock]  = rockEnemyLoadout;
		}
	}

	public static void RegisterEnemy(GameObject enemy)
	{
		if(!enemies.Contains(enemy))
		{
			enemies.Add (enemy);
		}
	}

	public static GameObject GetClosest(Vector3 pos, float maxDist)
	{
		enemies.RemoveAll(obj => obj == null);

		float closestDist = float.MaxValue;
		GameObject closestEnemy = null;

		foreach (GameObject e in enemies)
		{
			float dist = (e.transform.position - pos).sqrMagnitude;
			if(dist < closestDist)
			{
				closestDist = dist;
				closestEnemy = e;
			}
		}
		if(closestDist > maxDist * maxDist)
		{
			return null;
		}
		return closestEnemy;
	}

	public static GameObject GetNextClosest(GameObject current, Vector3 pos, float maxDist)
	{
		enemies.RemoveAll(obj => obj == null);

		float closestDist = float.MaxValue;
		GameObject closestEnemy = current;
		
		foreach (GameObject e in enemies)
		{
			float dist = (e.transform.position - pos).sqrMagnitude;
			if(dist < closestDist && e != current)
			{
				closestDist = dist;
				closestEnemy = e;
			}
		}
		if(closestDist > maxDist * maxDist)
		{
			return null;
		}
		return closestEnemy;
	}

	IEnumerator PurgeEnemyList()
	{
		while(true)
		{
			yield return new WaitForSeconds(5.0f);
			enemies.RemoveAll(obj => obj == null);
		}
	}


	public static void InitEnemy(Enemy e)
	{
		EnemyLoadout el = enemyLoadouts [(int)e.loadout.mType];
		e.loadout.name 			 = el.name;
		e.loadout.health		 = el.maxHealth;
		e.loadout.maxHealth 	 = el.maxHealth;
		e.loadout.damage		 = el.damage;
		e.loadout.knockbackPower = el.knockbackPower;
		e.loadout.speed 		 = el.speed;
	}

	public static EnemyLoadout GetEnemyLoadout(SpellType type)
	{
		return enemyLoadouts[(int)type];
	}





}
