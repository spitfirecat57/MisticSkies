using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager>
{
	public static List<GameObject> enemies;

	private static bool awakeHasBeenCalled = false;

	void Awake()
	{
		if(!awakeHasBeenCalled)
		{
			print ("[EnemyManager] Start()");
			enemies = new List<GameObject>();
			StartCoroutine ("PurgeEnemyList");
		}
	}

	public static void RegisterEnemy(GameObject enemy)
	{
		if(!enemies.Contains(enemy))
		{
			enemies.Add (enemy);
		}
	}

	public static GameObject GetClosest(Vector3 pos)
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

		return closestEnemy;
	}

	public static GameObject GetNextClosest(GameObject current, Vector3 pos)
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
		
		return closestEnemy;
	}

	IEnumerator PurgeEnemyList()
	{
		while(true)
		{
			yield return new WaitForSeconds(1.0f);
			enemies.RemoveAll(obj => obj == null);
		}
	}
}
