using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject spawnObject;

	public bool isSpawning = true;

	public bool autoSpawn = false;
	public float spawnDelay = 1.0f;
	private float mTimer = 0.0f;

	public KeyCode manualSpawnKey = KeyCode.Minus;
	public KeyCode onOffKey = KeyCode.Plus;



	void Start()
	{
		if(spawnObject == null)
		{
			Debug.Log("[Spawner] GameObject 'spawnObject' is not set");
			isSpawning = false;
		}

		mTimer = 0.0f;
	}

	void Update()
	{
		if(Input.GetKeyDown(onOffKey))
		{
			if(spawnObject != null)
			{
				isSpawning = !isSpawning;
			}
		}

		if(Input.GetKeyDown(manualSpawnKey))
		{
			GameObject.Instantiate(spawnObject, gameObject.transform.position, Quaternion.identity);
		}

		mTimer += Time.deltaTime;
		if(isSpawning)
		{
			if(mTimer >= spawnDelay)
			{
				GameObject.Instantiate(spawnObject, gameObject.transform.position, Quaternion.identity);
				mTimer -= spawnDelay;
			}
		}
	}
}
