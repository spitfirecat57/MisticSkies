using UnityEngine;
using System.Collections;

public class Tsunami : MonoBehaviour
{
	[HideInInspector]
	public SpellTsunami.Loadout loadout;

	private float distanceTravelled = 0.0f;

	void Start()
	{
		rigidbody.velocity = transform.forward * loadout.speed;
	}

	void Update()
	{
		distanceTravelled += loadout.speed * Time.deltaTime;
		if(distanceTravelled > loadout.range)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy"))
		{
			Enemy e = other.GetComponent<Enemy>();
			if(e)
			{
				e.TakeDamage(loadout.type, loadout.damage);
			}
		}
		else if(other.CompareTag("FireBoss"))
		{
			FireBoss e = other.GetComponent<FireBoss>();
			if(e)
			{
				e.TakeDamage(loadout.type, loadout.damage);
			}
		}
		else if(other.CompareTag("FlamePillar"))
		{
			Destroy(other.gameObject);
		}
	}
}
