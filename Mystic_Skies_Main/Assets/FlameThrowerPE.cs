using UnityEngine;
using System.Collections;

public class FlameThrowerPE : MonoBehaviour
{
	public float damagePerSecond;

	void OnParticleCollision(GameObject other)
	{
		if(other.CompareTag("Player"))
		{
			PlayerManager.GetPlayerScript().TakeDamage(damagePerSecond * Time.deltaTime);
		}
		else if(other.CompareTag("Fireball"))
		{
			Destroy(other.gameObject);
		}
	}
}
