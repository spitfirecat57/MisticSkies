using UnityEngine;
using System.Collections;

public class FlameThrowerPE : MonoBehaviour
{
	public float damagePerSecond;

	void OnParticleCollision(GameObject other)
	{
		print("OnParticleCollision");
		if(other.CompareTag("Player"))
		{
			PlayerManager.GetPlayerScript().TakeDamage(damagePerSecond * Time.deltaTime);
			print("OnParticleCollision:Hit Player");
		}
	}
}
