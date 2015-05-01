using UnityEngine;
using System.Collections;

public class Seeker_SuicideObject : MonoBehaviour
{
	public float radius = 1.0f;
	public float damage = 1.0f;
	public float knockbackPower = 1.0f;

	void Start()
	{
		Destroy (gameObject, 0.5f);
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius);
		foreach(Collider c in colliders)
		{
			if(c.CompareTag("Player"))
			{
				Player p = c.GetComponent<Player>();
				if(p)
				{
					p.TakeDamage(damage);
					p.KnockBack( (c.gameObject.transform.position - transform.position).normalized * knockbackPower);
				}
			}
		}
	}
}
