using UnityEngine;
using System.Collections;


public class LightTrigger2 : MonoBehaviour
{
	public GameObject Flame;


	// Use this for initialization
	void Start () 
	{
		Flame.SetActive(false);
		EnemyManager.RegisterEnemy(gameObject);
	}

	
	// Update is called once per frame

		void OnTriggerEnter (Collider col)
		{
			print ("Collided with " + col.name);
			if(col.gameObject.name == "Fireball(Clone)")
			{
				Flame.SetActive(true);
			}

		}
}
